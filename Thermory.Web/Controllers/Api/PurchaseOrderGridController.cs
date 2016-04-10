using Thermory.Domain.Enums;

namespace Thermory.Web.Controllers.Api
{
    public class PurchaseOrderGridController : OrderGridController
    {
        protected override OrderTypes OrderType
        {
            get { return OrderTypes.PurchaseOrder; }
        }
    }
}