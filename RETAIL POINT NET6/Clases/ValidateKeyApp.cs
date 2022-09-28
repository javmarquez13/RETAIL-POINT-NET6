using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RETAIL_POINT_NET6.Clases
{
    internal class ValidateKeyApp
    {
        public static bool ValidateKeyFile()
        {
            if (Directory.Exists(@"C:\Windows\System32\RetailPointKey"))
                return true;

            else
                return false;
        }
    }
}
