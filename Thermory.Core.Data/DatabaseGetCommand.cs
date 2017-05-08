using System.Data.SqlClient;

namespace Thermory.Core.Data
{
    public abstract class DatabaseGetCommand<T> : DatabaseCommand, IGetCommand<T>
    {
        public T Result { get; protected set; }

        protected DatabaseGetCommand(IFactory<SqlConnection> sqlConnectionFactory)
            : base(sqlConnectionFactory)
        { }
    }
}
