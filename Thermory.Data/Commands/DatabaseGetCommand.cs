using Thermory.Domain.Commands;

namespace Thermory.Data.Commands
{
    internal abstract class DatabaseGetCommand<T> : DatabaseContextCommand, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
