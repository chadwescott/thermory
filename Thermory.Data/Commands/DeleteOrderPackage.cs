using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeleteOrderPackage : DatabaseCommand
    {
        private readonly Package _package;

        public DeleteOrderPackage(Package package)
        {
            _package = package;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.OrderPackages.Attach(_package);
            context.OrderPackages.Remove(_package);
            context.SaveChanges();
        }
    }
}
