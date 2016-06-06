using System.Collections.Generic;
using Thermory.Domain.Constants;

namespace Thermory.Web.Models.Status
{
    public class OrderStatusGroup : StatusGroup
    {
        public OrderStatusGroup(string status)
            : base(status)
        { }

        private readonly Dictionary<string, StatusLabel> _statusLabelClasses = new Dictionary<string, StatusLabel>
        {
            {OrderStatusNames.Deleted, new DangerLabel()},
            {OrderStatusNames.InTransit, new WarningLabel()},
            {OrderStatusNames.Loaded, new SuccessLabel()},
            {OrderStatusNames.PackagingSlipCreated, new InfoLabel()},
            {OrderStatusNames.Pulled, new WarningLabel()},
            {OrderStatusNames.Received, new SuccessLabel()},
            {OrderStatusNames.SentToWarehouse, new InfoLabel()},
            {OrderStatusNames.WarehouseReceived, new InfoLabel()}
        };

        protected override Dictionary<string, StatusLabel> StatusLookup
        {
            get { return _statusLabelClasses; }
        }
    }
}