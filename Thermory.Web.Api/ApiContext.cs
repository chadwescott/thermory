using System.Globalization;
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

        private const string ApiKeyKey = "Context-ApiKey";
        private const string CustomerIdKey = "Context-CustomerId";
        private const string IsAdminKey = "Context-IsAdmin";
        private const string ProjectIdKey = "Context-ProjectId";
        private const string ProjectPaymentTypeKey = "Context-ProjectPaymentType";
        private const string ProjectTypeKey = "Context-ProjectType";
        private const string PageSizeKey = "Context-PageSize";
        private const string PageNumberKey = "Context-PageNumber";

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