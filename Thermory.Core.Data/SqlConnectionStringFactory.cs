using System.Data.SqlClient;

namespace Thermory.Core.Data
{
    public class SqlConnectionStringFactory : IFactory<SqlConnection>
    {
        private readonly string _connectionString;

        public SqlConnectionStringFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Make()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
