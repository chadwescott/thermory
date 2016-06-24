using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreateOrderPackage : DatabaseCommand
    {
        private readonly Package _package;

        public CreateOrderPackage(Package package)
        {
            _package = package;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderPackages.Add(_package);
            context.SaveChanges();
        }
    }
}
