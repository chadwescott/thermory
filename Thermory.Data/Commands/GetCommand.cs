namespace Thermory.Data.Commands
{
    internal abstract class GetCommand<T> : Command, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
