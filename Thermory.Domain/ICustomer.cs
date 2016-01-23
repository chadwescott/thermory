namespace Thermory.Domain
{
    public interface ICustomer
    {
        string FirstName { get; }

        string LastName { get; }

        string StreetAddress { get; }

        string City { get; }

        string State { get; }

        string PostalCode { get; }
    }
}
