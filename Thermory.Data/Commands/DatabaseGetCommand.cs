using Thermory.Domain.Commands;

namespace Thermory.Data.Commands
{
    internal abstract class DatabaseGetCommand<T> : DatabaseCommand, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
