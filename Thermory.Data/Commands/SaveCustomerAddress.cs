using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveCustomerAddress : DatabaseContextCommand
    {
        private readonly Address _address;

        public SaveCustomerAddress(Address address)
        {
            _address = address;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_address.Id == Guid.Empty)
                context.CustomerAddresses.Add(_address);
            else
                context.Entry(_address).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
