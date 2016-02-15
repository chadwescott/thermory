namespace Thermory.Data.Commands
{
    internal abstract class GetCommand<T> : DatabaseCommand, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
