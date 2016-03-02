using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;

namespace Thermory.QueryEngine.SqlServer
{
    public class SqlServerAdapter<T> : DatabaseAdapter<T>
    {
        private readonly string _sortField;

        public SqlServerAdapter(string connectionString)
            : base(connectionString)
        {
            var keyProperty =
                TargetType.GetProperties().FirstOrDefault(prop => Attribute.IsDefined(prop, typeof (KeyAttribute)));
            if (keyProperty != null)
            {
                var columnAttribute = keyProperty.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault();
                _sortField = columnAttribute != null ? ((ColumnAttribute)columnAttribute).Name : keyProperty.Name;
            }
            else
                _sortField = TargetType.GetProperties().First().Name;
        }

        protected override string GetSubQueryTerminator()
        {
            return ") AS t";
        }

        protected override string GetRowNumber()
        {
            return string.Format(" ROW_NUMBER() OVER (ORDER BY {0})", _sortField);
        }

        protected override DbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        protected override DbCommand CreateCommand(DbConnection connection)
        {
            return new SqlCommand(null, (SqlConnection)connection);
        }

        protected override DbParameter CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }
    }
}
