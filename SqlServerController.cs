using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace DatabaseLibrary
{
    public class SqlServerController : IDisposable, IDatabaseController
    {
        SqlConnection connection = null;
        bool disposed = false;

        public SqlServerController()
        {

        }

        public bool Connect(string dbAddress, string dbIdentifier, string UID, string PWD)
        {
            if(connection != null)
            {
                return false;
            }

            var connectionString = "Server=" + dbAddress + ";Database=" + dbIdentifier + ";UID=" + UID + ";PWD=" + PWD + ";";
            connection = new SqlConnection(connectionString);

            return CheckConnect();
        }

        public bool Connect(string connectionString)
        {
            if (connection != null)
            {
                return false;
            }

            connection = new SqlConnection(connectionString);

            return CheckConnect();
        }

        private bool CheckConnect()
        {
            try
            {
                connection.Open();
                connection.Close();

                return true;
            }
            catch
            {
                connection = null;
                return false;
            }
        }

        private DbDataAdapter ReceiveDBAdapter(string query)
        {
            if (connection == null)
                return null;

            return new SqlDataAdapter(query, connection);
        }

        private SqlCommand ReceiveSqlCommand(string query)
        {
            if (connection == null)
                return null;

            return new SqlCommand(query, connection);
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                connection.Open();
                var result = new DataSet();
                var dataAdapter = ReceiveDBAdapter(query);
                dataAdapter.Fill(result);

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                connection.Open();
                var result = new DataTable();
                var dataAdapter = ReceiveDBAdapter(query);
                dataAdapter.Fill(result);

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        public int SendQuery(string query)
        {
            try
            {
                connection.Open();
                var sqlCommand = ReceiveSqlCommand(query);
                var result = sqlCommand.ExecuteNonQuery();

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        // Dispose pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (disposed)
            {
                return;
            }

            if (IsDisposing)
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            disposed = true;
        }
    }
}
