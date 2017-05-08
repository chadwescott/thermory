namespace Thermory.Core
{
    public abstract class GetCommand<T> : CommandTemplate, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
