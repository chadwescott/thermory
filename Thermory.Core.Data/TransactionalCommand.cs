using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace Thermory.Core.Data
{
    public class TransactionalCommand : DatabaseCommandTemplate
    {
        private readonly IEnumerable<DatabaseCommandTemplate> _databaseCommands;

        public bool CommitChanges { get; set; }

        public TransactionalCommand(IFactory<SqlConnection> sqlConnectionFactory,
            IEnumerable<DatabaseCommandTemplate> databaseCommands, bool commitChanges = true)
            : base(sqlConnectionFactory)
        {
            _databaseCommands = databaseCommands;
            CommitChanges = commitChanges;
        }

        public override void Execute()
        {
            using (var scope = new TransactionScope())
            {
                base.Execute();
                if (CommitChanges)
                    scope.Complete();
            }
        }

        protected override void OnExecute(SqlCommand command)
        {
            foreach (var databaseCommand in _databaseCommands)
                databaseCommand.Execute(command.Connection);
        }
    }
}
