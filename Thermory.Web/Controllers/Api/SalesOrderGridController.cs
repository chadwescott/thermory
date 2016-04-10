using Thermory.Domain.Enums;

namespace Thermory.Web.Controllers.Api
{
    public class SalesOrderGridController : OrderGridController
    {
        protected override OrderTypes OrderType
        {
            get { return OrderTypes.SalesOrder; }
        }
    }
}