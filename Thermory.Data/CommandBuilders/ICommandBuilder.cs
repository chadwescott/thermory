using System.Collections.Generic;
using Thermory.Data.Commands;

namespace Thermory.Data.CommandBuilders
{
    internal interface ICommandBuilder
    {
        List<DatabaseCommand> Commands { get; } 
    }
}
