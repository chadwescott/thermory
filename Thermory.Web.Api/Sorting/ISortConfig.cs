using System.Collections.Generic;
using System.Web.Helpers;

namespace Thermory.Web.Api.Sorting
{
    public interface ISortConfig
    {
        string DefaultSortField { get; }
        SortDirection Direction { get; set; }
        List<string> Fields { get; }
        string GetModelFieldName(string fieldName);
    }
}