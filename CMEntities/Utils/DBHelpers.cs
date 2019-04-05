using EZRunner.Logger;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CMEntities.Utils
{
    public static class DBHelpers
    {
        private static readonly string connString = ConfigurationManager.ConnectionStrings["CampaignManagerDB"].ToString();
        private static IEzLog _logger;
        public static DataTable PullDataAsDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(connString);

            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            using (cmd.Connection)
            {
                try
                {
                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    cmd.Connection.Close();
                    da.Dispose();
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    throw;
                }
                finally
                {
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

            return dataTable;
        }

        public static T GetValue<T>(string query) where T : IComparable, IConvertible, IEquatable<T>
        {
            Object value;
            using (SqlCommand command = new SqlCommand(query))
                value = GetValue(command);
            if (Convert.IsDBNull(value))
                return GetDefaultValue<T>();
            return (T)Convert.ChangeType(value, typeof(T));
        }

        private static Object GetValue(SqlCommand cmd)
        {
            try
            {
                if (cmd.Connection == null)
                    using (SqlConnection connection = new SqlConnection(connString))
                    {
                        cmd.Connection = connection;
                        connection.Open();
                        return cmd.ExecuteScalar();
                    }
                else
                    return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                return DBNull.Value;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static T GetDefaultValue<T>()
        where T : IComparable, IConvertible, IEquatable<T>
        {
            return typeof(T) == typeof(string) ? (T)(object)string.Empty : default(T);
        }

        public static void ExecuteSingleQuery(string query)
        {
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
        }

        public static string ConvDateToSQL(DateTime InDate)
        {
            return InDate.ToString("yyyy-MM-dd") + " " + InDate.ToString("HH:mm.ss");
        }

        public static string ToSqlDate(this DateTime InDate)
        {
            return InDate.ToString("yyyy-MM-dd") + " " + InDate.ToString("HH:mm.ss");
        }

        public static void ExecuteSingleQueryLocalDB(string query, string connString)
        {
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception e)
            {
                _logger.LogInfo("Error in ExecuteSingleQueryLocalDB: " + e.Message);
            }
        }

        public static T QueryLocalDBWithReturnType<T>(string query, string connString)
        {
            T theValueWeWant = default(T);
            try
            {
                var conn = new SqlConnection(connString);
                conn.Open();
                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                theValueWeWant = (T)cmd.ExecuteScalar();
                conn.Close();
            }
            catch (Exception e)
            {
                var message = e.Message;
                _logger.LogInfo("Error in QueryLocalDBWithReturnType: " + e.Message);
            }

            return theValueWeWant;
        }
    }
}
