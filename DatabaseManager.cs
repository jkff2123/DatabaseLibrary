using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    public class DatabaseManager : IDisposable
    {
        IDatabaseController databaseController;
        private bool disposed = false;

        public DatabaseManager(IDatabaseController controller)
        {
            databaseController = controller;
        }

        /// <summary>
        /// Connect to Oracle server.
        /// </summary>
        /// <param name="dbAddress">Database address [ip:port] ex)"localhost:1507"</param>
        /// <param name="dbIdentifier">Database name</param>
        /// <param name="uID">User ID</param>
        /// <param name="pWD">Password</param>
        /// <returns></returns>
        public bool Connect(string dbAddress, string dbIdentifier, string uID, string pWD)
        {
            return databaseController.Connect(dbAddress, dbIdentifier, uID, pWD);
        }

        public bool Connect(string connectionString)
        {
            return databaseController.Connect(connectionString);
        }

        /// <summary>
        /// Get data set.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string query)
        {
            return databaseController.GetDataSet(query);
        }

        /// <summary>
        /// Get data table.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTable(string query)
        {
            return databaseController.GetDataTable(query);
        }

        /// <summary>
        /// Send query without result.
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <returns>Row number affected by query. If nothing is affected return -1</returns>
        public int SendQuery(string query)
        {
            return databaseController.SendQuery(query);
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
                ((IDisposable)databaseController).Dispose(); 
            }

            disposed = true;
        }
    }
}
