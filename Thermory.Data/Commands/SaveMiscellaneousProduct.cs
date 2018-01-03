using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveMiscellaneousProduct : DatabaseContextCommand
    {
        private readonly MiscellaneousProduct _model;

        public SaveMiscellaneousProduct(MiscellaneousProduct model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;

            var existingMiscellaneousProducts =
                context.MiscellaneousProducts.Where(c => c.MiscellaneousSubCategoryId == _model.MiscellaneousSubCategoryId);

            _model.SortOrder = existingMiscellaneousProducts.Any()
                ? existingMiscellaneousProducts.Select(c => c.SortOrder).Max() + 1
                : 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.MiscellaneousProducts.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
