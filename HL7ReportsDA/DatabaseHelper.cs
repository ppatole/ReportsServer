using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HL7ReportsDA
{
    public static class DatabaseHelper
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = Properties.Settings.Default.ConnectionString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }

        public static SqlDataReader ExecuteSQLReader(String SPName, List<SqlParameter> prms = null, System.Data.CommandType cmdType = System.Data.CommandType.StoredProcedure)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection conn = GetConnection();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, conn);
                cmd.CommandType = cmdType;
                if (prms != null && prms.Count > 0)
                {
                    cmd.Parameters.AddRange(prms.ToArray());
                }
                return cmd.ExecuteReader();
            }
            catch (Exception exx)
            {
                throw exx;
            }
            finally
            {
            }
        }

        public static int ExecuteNonQuery(String SPName, List<SqlParameter> prms = null, System.Data.CommandType cmdType = System.Data.CommandType.StoredProcedure)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlConnection conn = GetConnection();
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            try
            {
                SqlCommand cmd = new SqlCommand(SPName, conn);
                if (prms != null && prms.Count > 0)
                {
                    cmd.Parameters.AddRange(prms.ToArray());
                }

                cmd.CommandType = cmdType;

                return cmd.ExecuteNonQuery();
            }
            catch (Exception exx)
            {
                throw exx;
            }
            finally
            {
            }
        }
    }
}
