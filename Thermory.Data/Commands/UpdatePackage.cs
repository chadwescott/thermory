using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class UpdatePackage : DatabaseContextCommand
    {
        private readonly Package _package;

        public UpdatePackage(Package package)
        {
            _package = package;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var dbPackage = context.Packages.SingleOrDefault(p => p.Id == _package.Id);
            if (dbPackage == null) return;

            dbPackage.Height = _package.Height;
            dbPackage.Length = _package.Length;
            dbPackage.Width = _package.Width;
            context.SaveChanges();
        }
    }
}
