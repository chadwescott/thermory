using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveCustomer : DatabaseCommand
    {
        private readonly Customer _customer;

        public SaveCustomer(Customer customer)
        {
            _customer = customer;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_customer.Id == Guid.Empty)
                context.Customers.Add(_customer);
            else
                context.Entry(_customer).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
