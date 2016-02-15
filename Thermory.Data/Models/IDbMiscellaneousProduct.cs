namespace Thermory.Data.Models
{
    public interface IDbMiscellaneousProduct : IDbProduct
    {
        string Name { get; }

        string Description { get; }
    }
}
