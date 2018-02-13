using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thermory.Web.Api.Models.Responses
{
    public abstract class LinkedResource
    {
        [JsonProperty("links")]
        public List<Link> Links { get; set; }

        protected LinkedResource()
        {
            Links = new List<Link>();
        }
    }
}