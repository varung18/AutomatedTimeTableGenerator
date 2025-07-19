using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TImeTableGenerator
{
    public class DatabaseLayer
    {
        public static SqlConnection conn;

        public static SqlConnection ConOpen()
        {
            if (conn == null)
            {
                conn = new SqlConnection(@"Data Source=VARUN_GAUR;Initial Catalog=AutoTimeTableDB;Integrated Security=True");
            }

            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            return conn;
        }

        public static bool Insert (string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, ConOpen());
                int roweffected = cmd.ExecuteNonQuery();
                if (roweffected > 0)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
        }

        public static bool Update(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, ConOpen());
                int roweffected = cmd.ExecuteNonQuery();
                if (roweffected > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool Delete(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, ConOpen());
                int roweffected = cmd.ExecuteNonQuery();
                if (roweffected > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public static DataTable Retrieve(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(query, ConOpen());
                da.Fill(dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                        return dt;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
