namespace Thermory.Domain
{
    public interface IProductFamily
    {
        string Name { get; }
        bool IsActive { get; set; }
        IProductFamily Parent { get; }
    }
}
