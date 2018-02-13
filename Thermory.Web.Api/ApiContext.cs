using System.Globalization;
using System.Web.Helpers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace Thermory.Web.Api
{
    public class ApiContext
    {
        private ModelStateDictionary _modelState;

        public ApiContext(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        private const string PageSizeKey = "Context-PageSize";
        private const string PageNumberKey = "Context-PageNumber";
        private const string SortDirectionKey = "Context-SortDirection";
        private const string SortFieldKey = "Context-SortField";

        public int PageSize
        {
            get { return (int)GetModelStateValue(PageSizeKey); }
            set { AddOrUpdateModelState(PageSizeKey, value); }
        }

        public int PageNumber
        {
            get { return (int)(GetModelStateValue(PageNumberKey)); }
            set { AddOrUpdateModelState(PageNumberKey, value); }
        }

        public SortDirection SortDirection
        {
            get { return (SortDirection)GetModelStateValue(SortDirectionKey); }
            set { AddOrUpdateModelState(SortDirectionKey, value); }
        }

        public string SortField
        {
            get { return GetModelStateValue(SortFieldKey).ToString(); }
            set { AddOrUpdateModelState(SortFieldKey, value); }
        }

        protected void AddOrUpdateModelState(string key, object value)
        {
            var val = new ValueProviderResult(value, value?.ToString(), CultureInfo.CurrentCulture);
            if (_modelState.ContainsKey(key))
                _modelState[key].Value = val;
            else
                _modelState.Add(key, new ModelState() { Value = val });

        }

        protected object GetModelStateValue(string key)
        {
            return _modelState[key].Value.RawValue;
        }
    }
}