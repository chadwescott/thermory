using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Thermory.Core.Data
{
    public abstract class DatabaseCommand : DatabaseCommandTemplate
    {
        protected DatabaseCommand(IFactory<SqlConnection> sqlConnectionFactory)
            : base(sqlConnectionFactory)
        { }

        protected sealed override void OnExecute(SqlCommand command)
        {
            command.CommandType = CommandType;
            command.CommandText = CommandText;
            foreach (var parameter in CommandParameters)
                command.Parameters.Add(parameter);
            ExecuteCommand(command);
        }

        protected virtual void ExecuteCommand(SqlCommand command)
        {
            command.ExecuteNonQuery();
        }

        protected abstract CommandType CommandType { get; }

        protected abstract string CommandText { get; }

        protected abstract IEnumerable<SqlParameter> CommandParameters { get; }
    }
}
