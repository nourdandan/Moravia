using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moravia.Parsers
{
    //Implementation of IFormatParser<T> allows us to read and write whilst
    // using Stream which can be of any type

    public class XmlParser<T> : IFormatParser<T>
    {
        //use a function for value mappung for more customized xml nodes reading
        public readonly Func<XDocument, T> _mapper;
        public XmlParser(Func<XDocument,T> mapper)
        {
            _mapper = mapper;
        }
        public T Read(Stream sourceStream)
        {
            string input;
            //umanaged resource resource stream - reading stream 
            using (sourceStream)
            {
                //use stream reader 
                using (var reader = new StreamReader(sourceStream))
                {
                    input = reader.ReadToEnd();
                }
            }
            //load string into xdocument
            var xdoc = XDocument.Parse(input);
            //map values to convert to object
            var elem = _mapper(xdoc);
            return elem;
        }

        public void Write(T data, Stream targetStream)
        {
            System.Xml.Serialization.XmlSerializer writer =
                      new System.Xml.Serialization.XmlSerializer(typeof(T));
            //umanaged resource resource stream - reading stream 
            using (targetStream)
            {
                //write to stream
                writer.Serialize(targetStream, data);
            }
        }
    }
}
