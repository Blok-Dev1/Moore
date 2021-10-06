using System;
using System.Data.SqlClient;

namespace DAQ.Sql
{
    public class Data
    {
        public static void Test(string conn)
        {
            
            using (SqlConnection connection = new SqlConnection(conn))
            {
                var query = "select 1";
                
                var command = new SqlCommand(query, connection);

                connection.Open();
               
                command.ExecuteScalar();
            }
        }
    }
}
