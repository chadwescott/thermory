namespace Thermory.Domain.Commands
{
    public interface IGetCommand<out T> : ICommand
    {
        T Result { get; }
    }
}
