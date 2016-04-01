using System.Linq;
using Thermory.QueryEngine.Grid;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers.Api
{
    public class OrderGridController : ThermoryGridController<OrderView>
    {
        protected override void ValidatePostData(PostData<OrderView> data)
        {
            var orderNumberSort = data.sort.SingleOrDefault(s => s.field == "OrderNumberString");
            if (orderNumberSort != null)
                orderNumberSort.field = "OrderNumber";
        }
    }
}