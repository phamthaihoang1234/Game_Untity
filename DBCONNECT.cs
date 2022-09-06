using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DBCONNECT
    {
        public SqlConnection SqlConn = null;
        public SqlConnection getConnect()
        {
            string strConnect = GetConnectionStringByName("connectionString");
            try
            {
                if (SqlConn == null)
                {
                    SqlConn = new SqlConnection(strConnect);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            //SqlConn = new SqlConnection(strConect);
            return SqlConn;
        }

        static string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }

        public DBCONNECT()
        {
            string strConnect = GetConnectionStringByName("connectionString");
            try
            {
                if (SqlConn == null)
                {
                    SqlConn = new SqlConnection(strConnect);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public SqlDataReader getData(String sql)
        {
            SqlDataReader reader = null;
            if (SqlConn.State == System.Data.ConnectionState.Closed)
            {
                SqlConn.Open();
            }
            using (var command = SqlConn.CreateCommand())
            {
                command.CommandText = sql;
                reader = command.ExecuteReader();

            }
            return reader;
        }
    }
}
