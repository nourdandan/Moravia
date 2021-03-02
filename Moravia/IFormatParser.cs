using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moravia
{
    interface IFormatParser<T>
    {
        T Read(Stream sourceStream);
        void Write(T data, Stream targetStream);
    }
}
