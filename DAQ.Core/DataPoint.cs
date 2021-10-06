using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DAQ.Core
{
    public class DataPoint: Dictionary<string, double>
    {
        public double Mean()
        {
            var sequence = this.Values;

            if (sequence.Any())
            {
                return sequence.Average();
            }

            return 0;
        }

        public double StandardDeviation()
        {
            double result = 0;

            var sequence = this.Values;

            if (sequence.Any())
            {
                double average = sequence.Average();

                double sum = sequence.Sum(d => Math.Pow(d - average, 2));

                result = Math.Sqrt((sum) / (sequence.Count() - 1));
            }
            return result;
        }

        // Root Mean Square  
        public double RMS(int window)
        {
            int square = 0;

            double mean, root = 0;

            var arr = this.Values.ToArray();

            if (arr.Any())
            {
                // Calculate square
                for (int i = 0; i < window; i++)
                {
                    square += (int)Math.Pow(arr[i], 2);
                }

                // Calculate Mean
                mean = (square / (window));

                // Calculate Root
                root = Math.Sqrt(mean);
            }

            return root;
        }

        public class TimeValue
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        public DataPoint RateOfChange()
        {
            var values = new List<TimeValue>();

            var roc = new DataPoint();

            foreach(var kvp in this)
            {
                var ts = kvp.Key;

                if(DateTime.TryParse(ts, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
                {
                    values.Add(new TimeValue { DateTime = dt, Value = kvp.Value });
                }
            }

            for(int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    var c = values[i - 1]; // n - 1

                    var n = values[i]; // n

                    var step = (n.DateTime - c.DateTime).TotalSeconds;

                    var dvdt = (n.Value - c.Value) / step;

                    roc.Add(n.DateTime.ToString(), dvdt);
                }
                else
                    roc.Add(values[i].DateTime.ToString(), 0);
            }

            return roc;
        }
    }
}
