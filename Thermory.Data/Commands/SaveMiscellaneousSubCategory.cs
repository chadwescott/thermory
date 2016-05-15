using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveMiscellaneousSubCategory : DatabaseCommand
    {
        private readonly MiscellaneousSubCategory _model;

        public SaveMiscellaneousSubCategory(MiscellaneousSubCategory model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;

            var existingSubCategories =
                context.MiscellaneousSubCategories.Where(c => c.MiscellaneousCategoryId == _model.MiscellaneousCategoryId);

            _model.SortOrder = existingSubCategories.Any()
                ? existingSubCategories.Select(c => c.SortOrder).Max() + 1
                : 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.MiscellaneousSubCategories.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
