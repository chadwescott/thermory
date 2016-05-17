namespace Thermory.Domain.Commands
{
    public abstract class GetCommand<T> : BaseCommand, IGetCommand<T>
    {
        public T Result { get; protected set; }
    }
}
