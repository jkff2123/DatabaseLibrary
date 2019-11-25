using System.Data;

namespace DatabaseLibrary
{
    public interface IDatabaseController
    {
        bool Connect(string dbAddress, string dbIdentifier, string UID, string PWD);
        int SendQuery(string query);
        DataSet GetDataSet(string query);
        DataTable GetDataTable(string query);
    }
}
