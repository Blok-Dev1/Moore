using DAQ.CSV;
using DAQ.Sql;
using System;
using System.Collections.Generic;

namespace Test.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var dps = CSVMethods.SplitCSVFile(@".\Files\TestFile.csv");

            var sd1 = dps.StandardDeviation("TAG1");

            var roc7 = dps.RateOfChange("TAG7");

            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slaubscher\source\repos\Moore\DAQ.Sql\DataAcquisition.mdf;Integrated Security=True
            //Data.Test(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\slaubscher\source\repos\Moore\DAQ.Sql\DataAcquisition.mdf;uid=DBAdmin;password=Password1234")

        }
    }
}
