using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtility
{
    interface ISettings
    {
        public void WriteValue(string section, string key, string value);
        public string ReadValue(string section, string key, string defaultValue = "");
    }
}
