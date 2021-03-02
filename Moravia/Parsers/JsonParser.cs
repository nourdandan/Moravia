using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia.Parsers
{
    //Implementation of IFormatParser<T> allows us to read and write whilst
    // using Stream which can be of any type

    public class JsonParser<T> : IFormatParser<T>
    {
        public T Read(Stream sourceStream)
        {
            string input;
            //umanaged resource resource stream - reading stream 
            using (sourceStream)
            {
                //use stream reader 
                //can be changed to jsonreadfile within newtonsoft lib
                using (var sr = new StreamReader(sourceStream))
                {
                    input = sr.ReadToEnd();
                }
            }
            //deserialize to  whatever type specified
            return JsonConvert.DeserializeObject<T>(input);
        }

        public void Write(T data, Stream targetStream)
        {
            //serialize from whatever type specified
            var serialized = JsonConvert.SerializeObject(data);
            //umanaged resource resource stream - writing stream 
            using (targetStream)
            {
                //use stream writer but can also use jsonwriter within newtonsoft
                using (var sw = new StreamWriter(targetStream))
                {
                    sw.Write(serialized);
                }
            }
        }
    }
}
