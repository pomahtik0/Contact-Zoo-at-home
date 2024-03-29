using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Contact_zoo_at_home.Application
{
    public static class DBConnections
    {
        public static string ConnectionString { get; set; }

        public static DbConnection GetNewDbConnection() => new SqlConnection(ConnectionString);
    }
}
