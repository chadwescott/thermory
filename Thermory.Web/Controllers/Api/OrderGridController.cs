using Thermory.Web.Models;

namespace Thermory.Web.Controllers.Api
{
    public class OrderGridController : ThermoryGridController<Order, Order>
    {
        protected override Order CreateViewModel(Order model)
        {
            return model;
        }
    }
}