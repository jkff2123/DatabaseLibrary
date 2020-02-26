using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.Common;

namespace DatabaseLibrary
{
    public class OracleController : IDatabaseController, IDisposable
    {
        OracleConnection connection = null;
        bool disposed = false;

        public OracleController()
        {
            connection = null;
        }

        public bool Connect(string dbAddress, string dbIdentifier, string UID, string PWD)
        {
            if (connection != null)
            {
                return false;
            }

            var ipandport = dbAddress.Split(':');
            if (ipandport.Length != 2)
            {
                return false;
            }

            var connectionString = @"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = " + ipandport[0] + ")(PORT = " + ipandport[1] + ")))(CONNECT_DATA = (SID = " + dbIdentifier + ")(SERVER = DEDICATED))); User Id = " + UID + "; Password = " + PWD + ";";
            connection = new OracleConnection(connectionString);

            return CheckConnect();
        }

        public bool Connect(string connectionString)
        {
            if (connection != null)
            {
                return false;
            }

            connection = new OracleConnection(connectionString);

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
                return false;
            }
        }

        private DbDataAdapter ReceiveDBAdapter(string query)
        {
            if (connection == null)
                return null;

            return new OracleDataAdapter(query, connection);
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

                var command = new OracleCommand(query, connection);
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
