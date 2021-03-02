using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Moravia
{
    //Implementation of IStreamManager allows us to have dedicated streams 
    //for read and write , whilst allowing us to customize the settings


    public class FileStreamHandler : IStreamManager
    {
        //get stream dedicated for read using the source . i.e. file path
        public Stream GetReadStream(string sourceFileName)
        {
            return File.Open(sourceFileName, FileMode.Open);
        }

        //get stream dedicated for write using the target . i.e. file path
        public Stream GetWriteStream(string targetFileName)
        {
            return File.Open(targetFileName, FileMode.Create, FileAccess.Write);
        }
    }

    public class HttpStreamHandler : IStreamManager
    {
        //get stream dedicated for read the target . i.e. uri path
        public Stream GetReadStream(string sourceFileName)
        {
            WebRequest request = WebRequest.Create(sourceFileName);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }

        //get stream dedicated for write using the target . i.e. uri path
        public Stream GetWriteStream(string targetFileName)
        {
            //create request using the uri string
            var request = WebRequest.Create(targetFileName);
            request.Credentials = CredentialCache.DefaultCredentials;
            return request.GetRequestStream();
        }
    }
}
