using Thermory.Domain.Constants;
using Thermory.Domain.Enums;

namespace Thermory.Domain
{
    public static class AdjustmentMultiplier
    {
        public static int GetByOrderType(OrderTypes orderType)
        {
            return orderType == OrderTypes.PurchaseOrder ? 1 : -1;
        }
    }
}
