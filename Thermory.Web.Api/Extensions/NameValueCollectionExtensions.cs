using System;
using System.Collections.Specialized;

namespace Thermory.Web.Api.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static T ParseValue<T>(this NameValueCollection collection, string key, T defaultValue)
        {
            try
            {
                return (T)Convert.ChangeType(collection[key], typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}