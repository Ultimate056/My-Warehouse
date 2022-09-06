using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad
{
    public class CoreMethods
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SkladConnection"].ConnectionString;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        static SqlConnection connection = new SqlConnection(connectionString);


        public Object[] GetAllNamesField()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable tables = connection.GetSchema("Tables");
                List<String> TableNames = new List<String>();
                foreach (DataRow row in tables.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }

                Object[] obj = new object[TableNames.Count];
                for (int i = 0; i < TableNames.Count; i++)
                {
                    obj[i] = TableNames[i];
                }
                return obj;
            }
        }



    }

}
