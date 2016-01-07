namespace Thermory.Data.Commands
{
    public interface IGetCommand<out T> : ICommand
    {
        T Result { get; }
    }
}
