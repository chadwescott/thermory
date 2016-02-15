namespace Thermory.Data.Models
{
    public interface IDbLumberProduct : IDbProduct
    {
        int Length { get; }
    }
}
