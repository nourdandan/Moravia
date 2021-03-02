using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Moravia.Parsers;
using Newtonsoft.Json;

namespace Moravia.Homework
{
    public class Document
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //source/target could be either file path or any other based on resource
            var sourceFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Source Files\\Document1.xml");
            var targetFileName = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Target Files\\Document1.json");
            var fileHandler = new FileStreamHandler();
            var httpHandler = new HttpStreamHandler();

            var xmlParser = new XmlParser<Document>(Mappers.DocumentMapper);
            var jsonParser = new JsonParser<Document>();

            try
            {
                var doc = xmlParser.Read(fileHandler.GetReadStream(sourceFileName));
                jsonParser.Write(doc, fileHandler.GetWriteStream(targetFileName));
                //xmlParser.Write(doc, httpHandler.GetWriteStream(targetFileName));
                //jsonParser.Write(doc, httpHandler.GetWriteStream(targetFileName));
                //jsonParser.Write(doc, fileHandler.GetWriteStream(targetFileName));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}