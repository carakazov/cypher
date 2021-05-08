using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingApp.logic
{
    interface IEncoder
    {
        string Encode(string plainText);
        string Decode(string encodedString);
    }
}
