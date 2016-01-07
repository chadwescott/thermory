namespace Thermory.Data.Models
{
    /// <summary>
    /// Adds dimensions related to a product. Measurements are in millimeters.
    /// </summary>
    public interface IDbLumberFamily : IDbProductFamily
    {
        int Thickness { get; }
        int Width { get;  }
    }
}
