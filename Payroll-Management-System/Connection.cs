using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    static class Connection
    {
        public static SqlConnection con;
        public static SqlConnection getConnection()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-HMGV26E\SQLEXPRESS01;Initial Catalog=PayRollSystem;Integrated Security=True");
            if(con == null || con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public static void closeConnection()
        {
            if(con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}
