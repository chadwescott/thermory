using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class CreatePackage : DatabaseCommand
    {
        private readonly Package _package;

        public CreatePackage(Package package)
        {
            _package = package;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.Packages.Add(_package);
            context.SaveChanges();
        }
    }
}
