namespace Thermory.Domain
{
    public interface ILumberProduct : IProduct<ILumberProductType>
    {
        ILumberInventory Inventory { get; set;  }

        int LengthInMillmeters { get; }

        double LengthInInches { get; }
    }
}
