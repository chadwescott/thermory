namespace Thermory.Core
{
    public interface IGetCommand<T> : ICommand
    {
        T Result { get; }
    }
}
