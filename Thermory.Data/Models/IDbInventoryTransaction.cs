using System;
using System.ComponentModel.DataAnnotations;

namespace Thermory.Data.Models
{
    internal interface IDbInventoryTransaction
    {
        [Key]
        Guid Id { get; }

        Guid TransactionTypeId { get; }

        IDbTransactionType TransactionType { get; }

        int UserId { get; }

        DateTime CreatedOn { get; }
    }
}