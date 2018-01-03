using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SavePackagingType : DatabaseContextCommand
    {
        private readonly PackagingType _packagingType;

        public SavePackagingType(PackagingType packagingType)
        {
            _packagingType = packagingType;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_packagingType.Id == Guid.Empty)
                context.PackagingTypes.Add(_packagingType);
            else
                context.Entry(_packagingType).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
