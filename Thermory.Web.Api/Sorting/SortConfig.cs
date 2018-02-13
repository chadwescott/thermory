using System.Collections.Generic;
using System.Web.Helpers;

namespace Thermory.Web.Api.Sorting
{
    public abstract class SortConfig : ISortConfig
    {
        public SortDirection Direction { get; set; } = SortDirection.Ascending;

        public abstract string DefaultSortField { get; }

        public abstract List<string> Fields { get; }

        public abstract string GetModelFieldName(string fieldName);
    }
}