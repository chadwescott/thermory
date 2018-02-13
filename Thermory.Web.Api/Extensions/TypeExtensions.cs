using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Thermory.Web.Api.Extensions
{
    public static class TypeExtensions
    {
        public static Dictionary<string, string> GetJsonPropertyNameDictionary(this Type modelType)
        {
            return modelType.GetProperties()
                .ToDictionary(p => p.Name, p => p.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? p.Name);
        }
    }
}