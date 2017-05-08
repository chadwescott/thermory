namespace Thermory.Core
{
    public interface IFactory<out T>
    {
        T Make();
    }
}
