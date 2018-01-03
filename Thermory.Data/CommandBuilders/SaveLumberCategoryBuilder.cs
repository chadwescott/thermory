using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class SaveLumberCategoryBuilder : CommandBuilder
    {
        public SaveLumberCategoryBuilder(LumberCategory model)
        {
            AddSortOrderCleanupCommands(model);
            Commands.Add(new SaveLumberCategory(model));
        }

        private void AddSortOrderCleanupCommands(LumberCategory model)
        {
            var command = new GetAllLumberCategories(true);
            command.Execute();

            var categories = command.Result;
            if (categories.All(c => c.Id == model.Id || c.SortOrder != model.SortOrder)) return;

            if (model.Id == null)
                UpdateNewCategorySortOrder(model, categories);
            else
                UpdateExistingCategorySortOrder(model, categories);
        }

        private void UpdateNewCategorySortOrder(LumberCategory model, IList<LumberCategory> categories)
        {
            IncrementExistingSortOrder(model.SortOrder, categories.Max(c => c.SortOrder) + 1, categories);
        }

        private void UpdateExistingCategorySortOrder(LumberCategory model, IList<LumberCategory> categories)
        {
            var dbModel = categories.Single(c => c.Id == model.Id);
            var sortOrder = model.SortOrder;

            var modelCopy = model.Clone();
            modelCopy.SortOrder = -1;
            Commands.Add(new SaveLumberCategory(modelCopy));

            if (dbModel.SortOrder < sortOrder)
                DecrementExistingSortOrder(dbModel.SortOrder, sortOrder, categories);
            else
                IncrementExistingSortOrder(sortOrder, dbModel.SortOrder, categories);
            
            model.SortOrder = sortOrder;
        }

        private void IncrementExistingSortOrder(int minSortOrder, int maxSortOrder, IList<LumberCategory> categories)
        {
            foreach (var category in categories.Where(c => c.SortOrder >= minSortOrder && c.SortOrder < maxSortOrder).OrderByDescending(c => c.SortOrder))
            {
                category.SortOrder++;
                Commands.Add(new SaveLumberCategory(category));
            }
        }

        private void DecrementExistingSortOrder(int minSortOrder, int maxSortOrder, IList<LumberCategory> categories)
        {
            foreach (var category in categories.Where(c => c.SortOrder >= minSortOrder && c.SortOrder <= maxSortOrder))
            {
                category.SortOrder--;
                Commands.Add(new SaveLumberCategory(category));
            }
        }
    }
}
