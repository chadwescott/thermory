namespace Thermory.Domain
{
    public interface ILumberProduct : IProduct<ILumberSubCategory>
    {
        int Length { get; }
    }
}
