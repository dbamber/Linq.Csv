using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Csv
{
    class CsvHeader
    {
        public static string Generate(object o)
        {
            var properties = o.GetType().GetProperties().OrderBy(p => p.Name);
            return string.Join(", ", properties.Select(p => p.GetValue(o, null).ToCsvString()));
        }
    }
}
