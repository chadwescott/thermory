using System.Collections.Generic;
using Thermory.Data.Commands;

namespace Thermory.Data.CommandBuilders
{
    internal abstract class CommandBuilder
    {
        private List<DatabaseContextCommand> _commands = new List<DatabaseContextCommand>();

        public List<DatabaseContextCommand> Commands
        {
            get { return _commands; }
            protected set { _commands = value; }
        }
    }
}
