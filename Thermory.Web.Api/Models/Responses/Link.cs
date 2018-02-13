using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Thermory.Web.Api.Constants;
using Thermory.Web.Api.Routing;

namespace Thermory.Web.Api.Models.Responses
{
    public class Link
    {
        [JsonProperty("href")]
        public string HRef { get; private set; }

        [JsonProperty("method")]
        public string Method { get; private set; }

        [JsonProperty("rel")]
        public string Relationship { get; private set; }

        private Link() { }

        private static Dictionary<LinkType, string> LinkTypeRelationships =
            new Dictionary<LinkType, string>
            {
                { LinkType.Self, Links.Self }
            };

        public static Link MakeLink(Route route, HttpMethod method, LinkType linkType)
        {
            return new Link { HRef = route.Path, Method = method.Method, Relationship = LinkTypeRelationships[linkType] };
        }
    }

    public enum LinkType
    {
        Self
    }
}