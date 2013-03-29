using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Csv
{
    static class StringUtil
    {
        public static string ToCsvString(this object value)
        {
            string content = value.ToString();

            if (content.Contains(','))
            {

                content = string.Format("\"{0}\"", content.Replace("\"", "\"\""));
            }

            return content;
        }
    }
}
