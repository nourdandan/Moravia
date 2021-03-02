using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia
{
    public interface IStreamManager
    {
        Stream GetReadStream(string sourceFileName);
        Stream GetWriteStream(string targetFileName);
    }
}
