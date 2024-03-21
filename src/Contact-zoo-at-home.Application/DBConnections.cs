using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Contact_zoo_at_home.Application
{
    public static class DBConnections
    {
        public static string ConnectionString { get; set; } = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\pomahtik\\source\\repos\\Contact-zoo-at-home\\src\\Contact-zoo-at-home.Infrastructure.Data\\Database1.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public static DbConnection GetNewDbConnection() => new SqlConnection(ConnectionString);
    }
}
