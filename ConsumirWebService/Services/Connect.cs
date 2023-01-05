﻿using ConsumirWebService.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsumirWebService.Services
{
    public class Connect
    {
        string url = "http://www.praticteste.praticsistemas2.com.br/PraticSite/PraticAppRHServlet";
        string action = "http://www.praticteste.praticsistemas2.com.br/PraticSite/PraticAppRHServlet";

        string xmlEstados = @"<?xml version=""1.0"" encoding=""ISO-8859-1"" ?><praticsistemas><praticServiceValidarEntrada>
            <tokenPraticAppRHAutenticacao>ksklsd9034nmsd4jf9023nmmgf034vxa,mbnvsd73bf9lsgwb0ldhweqktrlhbgmxçshynh06</tokenPraticAppRHAutenticacao>
            </praticServiceValidarEntrada><selecionar_estados><selecionar>true</selecionar></selecionar_estados></praticsistemas>";

        //string xmlMunicipios = @"<?xml version=""1.0"" encoding=""ISO-8859-1"" ?><praticsistemas><praticServiceValidarEntrada>
        //    <tokenPraticAppRHAutenticacao>ksklsd9034nmsd4jf9023nmmgf034vxa,mbnvsd73bf9lsgwb0ldhweqktrlhbgmxçshynh06</tokenPraticAppRHAutenticacao>
        //    </praticServiceValidarEntrada><selecionar_municipios><selecionar>true</selecionar></selecionar_municipios></praticsistemas>";

        public string LerXmlArquivo()
        {
            return File.ReadAllText(@"C:\C#\XML.xml");
        }

        public  HttpWebRequest CreateWebRequest()
        {

            HttpWebRequest web = (HttpWebRequest)WebRequest.Create(url);
            web.Headers.Add("SOAPAction", action);
            web.ContentType = "text/xml;charset=ISO-8859-1";
            web.Accept = "application/xml";
            web.Method = "POST";
            return web;
        }

        public XmlDocument CreateSoapEnvelope()
        {
            XmlDocument xml = new XmlDocument();
            
            xml.LoadXml(xmlEstados);


            return xml;
        }

        public  List<Estados> ListarEstados()
        {
            List<Estados> estados = new List<Estados>();
            XElement xml = XElement.Load(@"C:\C#\XML.xml");
            foreach (XElement x in xml.Elements())
            {
                Estados p = new Estados()
                {
                    codigo = x.Element("estcod").Value,
                    nome = x.Element("estnom").Value,
                };
                estados.Add(p);

            }

            foreach (var list in estados)
            {
                Console.WriteLine(list);
            }
            return estados;
        }


        public List<Estados> ConverterXmlParaObjeto(String xml)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);
            MemoryStream stream = new MemoryStream(byteArray);

            XmlTextReader xtr = new XmlTextReader(stream);

            List<Estados> estados = new List<Estados>();
            Estados e;

            e = new Estados();
            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "estcod")
                {
                    e.Cod = xtr.ReadElementContentAsString();
                }

                if (xtr.NodeType == XmlNodeType.Element && xtr.Name == "estnom")
                {
                    e.Nome = xtr.ReadElementContentAsString();
                    estados.Add(e);
                    e = new Estados();
                }
            }
            return estados;
        }

    }
}
