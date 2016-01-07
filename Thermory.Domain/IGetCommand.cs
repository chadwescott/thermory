namespace Thermory.Domain
{
    public interface IGetCommand<out T> : ICommand
    {
        T Result { get; }
    }
}
