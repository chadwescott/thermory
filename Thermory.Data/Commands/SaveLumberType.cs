using System;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberType : DatabaseCommand
    {
        private readonly LumberType _model;

        public SaveLumberType(LumberType model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;

            var existingLumberTypes =
                context.LumberTypes.Where(c => c.LumberSubCategoryId == _model.LumberSubCategoryId);

            _model.SortOrder = existingLumberTypes.Any()
                ? existingLumberTypes.Select(c => c.SortOrder).Max() + 1
                : 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.LumberTypes.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
