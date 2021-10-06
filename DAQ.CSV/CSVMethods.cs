using DAQ.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DAQ.CSV
{
    public class CSVMethods
    {
        public static string[] SplitLines(string csv)
        {
            return csv.TrimEnd('\n').TrimEnd('\r').Split('\n');
        }

        public static string ReadCSVFile(string path)
        {
            return File.ReadAllText(path);
        }

        public static DataPoints SplitCSVFile(string path, char delimiter = ',', bool trim = false)
        {
            var csv = ReadCSVFile(@".\Files\TestFile.csv");

            DataPoints dps = new DataPoints();

            var rows = SplitLines(csv);

            var header = SplitRow(rows[0], delimiter, trim);

            var tagnames = header.Skip(1).ToArray();

            foreach(var row in rows.ToList().Skip(1))
            {
                var vals = SplitRow(row, delimiter, trim);

                var ts = vals[0];

                var dp = new DataPoint();

                for (int i = 0; i < tagnames.Length; i++)
                {
                    double val = Double.NaN;

                    var name = tagnames[i];

                    if(i + 1 < vals.Length)
                        double.TryParse(vals[i + 1], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out val);

                    if (dp.ContainsKey(name))
                        dp[name] = val;
                    else
                        dp.Add(name, val);
                }

                dps.Add(ts, dp);
            }

            return dps;
        }

        public static string[] SplitRow(string record, char delimiter, bool trimData)
        {
            var results = new List<string>();

            var result = new StringBuilder();

            var row = $"{record}{delimiter}";

            for (var idx = 0; idx < row.Length; idx++)
            {
                if (row[idx] == delimiter)
                {
                    var res = trimData ? result.ToString().Trim() : result.ToString();

                    res = res.Replace("\r", "");

                    results.Add(res);

                    result.Clear();
                }
                else
                {
                    result.Append(row[idx]);
                }
            }

            return results.ToArray();
        }

    }
}
