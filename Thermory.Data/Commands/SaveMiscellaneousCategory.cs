using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveMiscellaneousCategory : DatabaseCommand
    {
        private readonly MiscellaneousCategory _model;

        public SaveMiscellaneousCategory(MiscellaneousCategory model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;
            _model.SortOrder = context.MiscellaneousCategories.Select(c => c.SortOrder).Max() + 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.MiscellaneousCategories.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
