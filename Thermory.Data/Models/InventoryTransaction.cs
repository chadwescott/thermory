using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class InventoryTransaction : IDbInventoryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType DbTransactionType { get; set; }

        [NotMapped]
        public IDbTransactionType TransactionType { get { return DbTransactionType; } }

        public int UserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
    }
}
