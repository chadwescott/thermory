using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberSubCategory : DatabaseCommand
    {
        private readonly LumberSubCategory _model;

        public SaveLumberSubCategory(LumberSubCategory model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;
            _model.SortOrder =
                context.LumberSubCategories.Where(c => c.LumberCategoryId == _model.LumberCategoryId)
                    .DefaultIfEmpty(new LumberSubCategory {SortOrder = 0})
                    .Select(c => c.SortOrder)
                    .Max() + 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.LumberSubCategories.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
