namespace Thermory.Domain
{
    /// <summary>
    /// Adds dimensions related to a product. Measurements are in millimeters.
    /// </summary>
    public interface ILumberFamily : IProductFamily
    {
        int Thickness { get; }
        int Width { get;  }
    }
}
