using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeletePackage : DatabaseCommand
    {
        private readonly Package _package;

        public DeletePackage(Package package)
        {
            _package = package;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.Packages.Attach(_package);
            context.Packages.Remove(_package);
            context.SaveChanges();
        }
    }
}
