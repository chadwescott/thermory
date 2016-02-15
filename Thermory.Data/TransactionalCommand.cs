using System;
using System.Collections.Generic;
using Thermory.Data.Commands;

namespace Thermory.Data
{
    internal class TransactionalCommand : DatabaseCommand
    {
        private readonly IEnumerable<DatabaseCommand> _commands;

        public TransactionalCommand(IEnumerable<DatabaseCommand> commands)
        {
            _commands = commands;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            context.Database.BeginTransaction();
            base.OnBeforeExecute(context);
        }

        protected override void OnExecute(ThermoryContext context)
        {
            foreach (var command in _commands)
                command.Execute(context);
        }

        protected override void OnAfterExecute(ThermoryContext context)
        {
            context.Database.CurrentTransaction.Commit();
            base.OnAfterExecute(context);
        }

        protected override void HandleException(ThermoryContext context, Exception ex)
        {
            context.Database.CurrentTransaction.Rollback();
            base.HandleException(context, ex);
        }
    }
}
