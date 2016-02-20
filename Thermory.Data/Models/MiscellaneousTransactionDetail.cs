﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    internal class MiscellaneousTransactionDetail : IDbMiscellaneousTransactionDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid InventoryTransactionId { get; set; }

        public Guid MiscellaneousProductId { get; set; }

        public int PreviousQuantity { get; set; }

        public int NewQuantity { get; set; }
    }
}
