﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class InventoryTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserProfile CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public Guid? OrderId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("InventoryTransactionId")]
        public List<LumberTransactionDetail> LumberTransactionDetails { get; set; }

        [ForeignKey("InventoryTransactionId")]
        public List<MiscellaneousTransactionDetail> MiscellaneousTransactionDetails { get; set; }
    }
}
