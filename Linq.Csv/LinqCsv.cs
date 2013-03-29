﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Csv
{
    public static class LinqCsv
    {
        public static string Csv<T>(this IEnumerable<T> source, object header, params Func<T,object>[] columns)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.WriteCsv(source, header, columns);

                return Encoding.ASCII.GetString(stream.ToArray());
            }
        }

        public static string Csv<T>(this IEnumerable<T> source, params Func<T, object>[] columns)
        {
            return source.Csv(null, columns);
        }

        public static void WriteCsv<T>(this Stream stream, IEnumerable<T> data, object header, params Func<T, object>[] columns)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if (header != null)
                {
                    writer.WriteLine(CsvHeader.Generate(header));
                }

                foreach (T line in data)
                {
                    var parts = columns.Select(p => p(line).ToCsvString());
                    writer.WriteLine(string.Join(", ", parts));
                }
            }
        }
    }
}