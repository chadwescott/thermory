using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberCategory : DatabaseCommand
    {
        private readonly LumberCategory _model;

        public SaveLumberCategory(LumberCategory model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;
            _model.SortOrder =
                DatabaseCommandDirectory.Instance.GetAllLumberCategories().Select(c => c.SortOrder).Max() + 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.LumberCategories.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
