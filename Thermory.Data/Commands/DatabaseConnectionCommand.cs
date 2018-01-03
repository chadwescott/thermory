using System.Data;
using System.Data.SqlClient;

namespace Thermory.Data.Commands
{
    internal abstract class DatabaseConnectionCommand : DatabaseContextCommand
    {
        protected sealed override void OnExecute(ThermoryContext context)
        {
            var connection = new SqlConnection(context.Database.Connection.ConnectionString);
            try
            {
                connection.Open();
                OnExecute(connection);
            }
            finally
            {
                connection.Close();
            }
        }

        protected abstract void OnExecute(SqlConnection connection);
    }
}
