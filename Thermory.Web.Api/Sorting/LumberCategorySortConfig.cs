using System.Collections.Generic;
using System.Linq;
using Thermory.Web.Api.Extensions;
using Thermory.Web.Api.Models.Responses;

namespace Thermory.Web.Api.Sorting
{
    public class LumberCategorySortConfig : SortConfig
    {
        private static readonly List<string> _fields = new List<string>();
        private readonly Dictionary<string, string> _fieldLookup;
        public readonly static LumberCategorySortConfig Instance = new LumberCategorySortConfig();

        private LumberCategorySortConfig()
        {
            _fieldLookup = typeof(LumberCategoryResponseModel).GetJsonPropertyNameDictionary();
            _fields.Add(_fieldLookup[nameof(LumberCategoryResponseModel.Name)]);
        }

        public override string DefaultSortField => _fieldLookup[nameof(LumberCategoryResponseModel.Name)];

        public override List<string> Fields { get { return _fields; } }

        public override string GetModelFieldName(string fieldName)
        {
            return _fieldLookup.ContainsValue(fieldName)
                ? _fieldLookup.SingleOrDefault(f => f.Value == fieldName).Key
                : string.Empty;
        }
    }
}