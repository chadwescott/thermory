using System;
using System.Data.SqlClient;

namespace Thermory.Core.Data
{
    public abstract class DatabaseCommandTemplate : ICommand
    {
        protected readonly IFactory<SqlConnection> SqlConnectionFactory;

        protected DatabaseCommandTemplate(IFactory<SqlConnection> sqlConnectionFactory)
        {
            SqlConnectionFactory = sqlConnectionFactory;
        }

        public virtual void Execute()
        {
            using (var connection = SqlConnectionFactory.Make())
            {
                connection.Open();
                Execute(connection);
            }
        }

        public void Execute(SqlConnection connection)
        {
            using (var command = new SqlCommand { Connection = connection })
            {
                try
                {
                    OnBeforeExecute(command);
                    OnExecute(command);
                    OnAfterExecute(command);
                }
                catch (Exception ex)
                {
                    HandleException(command, ex);
                }
            }
        }

        protected virtual void OnBeforeExecute(SqlCommand command)
        { }

        protected abstract void OnExecute(SqlCommand command);

        protected virtual void OnAfterExecute(SqlCommand command)
        { }

        protected virtual void HandleException(SqlCommand command, Exception ex)
        {
            throw ex;
        }
    }
}
