using System.Collections.Generic;

namespace Thermory.Web.Models.Status
{
    public abstract class StatusGroup
    {
        protected abstract Dictionary<string, StatusLabel> StatusLookup { get; }

        protected StatusGroup(string status)
        {
            Status = status ?? "";
        }

        public StatusLabel StatusLabel
        {
            get { return StatusLookup.ContainsKey(Status) ? StatusLookup[Status] : new PrimaryLabel(); }
        }

        public string Status { get; protected set; }
    }
}