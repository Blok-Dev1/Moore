using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DAQ.Core.DataPoint;

namespace DAQ.Core
{
    public class DataPoints: Dictionary<string, DataPoint>
    {
        public double Mean(string name)
        {
            var dps = GetValues(name);

            return dps.Mean();
        }

        public double StandardDeviation(string name)
        {
            var dps = GetValues(name);

            return dps.Mean();
        }

        public DataPoint RateOfChange(string name)
        {
            var dps = GetValues(name);

            return dps.RateOfChange();
        }

        public double RootMeanSquare(string name, int window)
        {
            var dps = GetValues(name);

            return dps.RMS(window);
        }

        public DataPoint GetValues(string name)
        {
            var datapoint = new DataPoint();

            foreach (var kvp in this)
            {
                var ts = kvp.Key;

                if (kvp.Value.TryGetValue(name, out double val))
                    datapoint.Add(ts, val);
            }

            return datapoint;
        }
    }
}
