using System;
using System.Data.Entity;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberProduct : DatabaseContextCommand
    {
        private readonly LumberProduct _model;

        public SaveLumberProduct(LumberProduct model)
        {
            _model = model;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            if (_model.Id == Guid.Empty)
                context.LumberProducts.Add(_model);
            else
                context.Entry(_model).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
