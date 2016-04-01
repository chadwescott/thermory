using System.Collections.Generic;
using Thermory.Data.Commands;

namespace Thermory.Data.CommandBuilders
{
    internal abstract class CommandBuilder
    {
        private readonly List<DatabaseCommand> _commands = new List<DatabaseCommand>();

        public List<DatabaseCommand> Commands { get { return _commands; } }
    }
}
