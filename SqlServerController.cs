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
            connection = null;
        }

        public bool Connect(string dbAddress, string dbIdentifier, string UID, string PWD)
        {
            if(connection != null)
            {
                return false;
            }
            var connection_string = "Server=" + dbAddress + ";Database=" + dbIdentifier + ";UID=" + UID + ";PWD=" + PWD + ";";
            connection = new SqlConnection(connection_string);
            connection.Open();

            return true;
        }

        private DbDataAdapter ReceiveDBAdapter(string query)
        {
            if (connection == null)
                return null;

            return new SqlDataAdapter(query, connection);
        }

        public DataSet GetDataSet(string query)
        {
            try
            {
                var result = new DataSet();
                var dataAdapter = ReceiveDBAdapter(query);
                dataAdapter.Fill(result);

                return result;
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                var result = new DataTable();
                var dataAdapter = ReceiveDBAdapter(query);
                dataAdapter.Fill(result);

                return result;
            }
            catch
            {
                return null;
            }
        }

        public int SendQuery(string query)
        {
            try
            {
                if (connection == null)
                    return 0;

                var command = new SqlCommand(query, connection);
                var result = command.ExecuteNonQuery();

                return result;
            }
            catch
            {
                return 0;
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
                }
            }

            disposed = true;
        }
    }
}
