using Thermory.Domain.Enums;

namespace Thermory.Domain.Utils
{
    public static class AdjustmentMultiplier
    {
        public static int GetByOrderType(OrderTypes orderType)
        {
            return orderType == OrderTypes.PurchaseOrder ? 1 : -1;
        }
    }
}
