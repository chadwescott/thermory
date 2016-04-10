using System.Collections.Generic;
using System.Linq;
using Thermory.Domain.Constants;
using Thermory.Domain.Enums;
using Thermory.QueryEngine.Grid;
using Thermory.Web.Models;

namespace Thermory.Web.Controllers.Api
{
    public abstract class OrderGridController : ThermoryGridController<OrderView>
    {
        protected abstract OrderTypes OrderType { get; }

        protected override void ValidatePostData(PostData<OrderView> data)
        {
            AddOrderTypeSearchFilter(data);
            var orderNumberSort = data.sort.SingleOrDefault(s => s.field == "OrderNumberString");
            if (orderNumberSort != null)
                orderNumberSort.field = "OrderNumber";
        }

        private void AddOrderTypeSearchFilter(PostData<OrderView> data)
        {
            var orderTypeFilter = new SearchFilter
            {
                field = "OrderType",
                value = new List<object> {OrderTypeNames.GetOrderTypeName(OrderType)},
                @operator = "is",
                type = "list"
            };
            if (data.search == null)
                data.search = new List<SearchFilter>();
            data.search.Add(orderTypeFilter);
        }
    }
}