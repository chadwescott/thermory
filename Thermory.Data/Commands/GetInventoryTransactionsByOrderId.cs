using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetInventoryTransactionsByOrderId : DatabaseGetCommand<IList<InventoryTransaction>>
    {
        private readonly Guid _orderId;

        public GetInventoryTransactionsByOrderId(Guid orderId)
        {
            _orderId = orderId;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.InventoryTransactions
                .Include(t => t.TransactionType)
                .Include(t => t.Order.OrderType)
                .Include(t => t.CreatedBy)
                .Include(
                    t =>
                        t.LumberTransactionDetails.Select(
                            d => d.LumberProduct.LumberType.LumberSubCategory.LumberCategory))
                .Include(
                    t =>
                        t.MiscellaneousTransactionDetails.Select(
                            d => d.MiscellaneousProduct.MiscellaneousSubCategory.MiscellaneousCategory))
                .Where(t => t.OrderId == _orderId)
                .OrderBy(t => t.CreatedOn)
                .ToList();
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Thermory.Domain.Models;

//namespace Thermory.Data.Commands
//{
//    internal class GetInventoryTransactionsByOrderId : DatabaseGetCommand<IList<InventoryTransactionView>>
//    {
//        private readonly Guid _orderId;

//        public GetInventoryTransactionsByOrderId(Guid orderId)
//        {
//            _orderId = orderId;
//        }

//        protected override void OnExecute(ThermoryContext context)
//        {
//            Result = context.InventoryTransactionViews
//                .Where(t => t.OrderId == _orderId)
//                .OrderBy(t => t.CreatedOn)
//                .ThenBy(t => t.LumberCategory)
//                .ThenBy(t => t.LumberSubCategory)
//                .ThenBy(t => t.LumberType)
//                .ThenBy(t => t.LengthInMillimeters)
//                .ThenBy(t => t.MiscellaneousCategory)
//                .ThenBy(t => t.MiscellaneousSubCategory)
//                .ThenBy(t => t.MiscellaneousProduct)
//                .ToList();
//        }
//    }
//}
