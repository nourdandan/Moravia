using Moravia.Homework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moravia
{
    public static class Mappers
    {
        public static Document DocumentMapper(XDocument xdoc)
        {
            return new Document
            {
                Title = xdoc.Root.Element("title").Value,
                Text = xdoc.Root.Element("text").Value
            };
        }
    }
}
