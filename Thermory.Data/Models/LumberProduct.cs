using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("LumberProduct")]
    internal class LumberProduct : Product, IDbLumberProduct
    {
        public Guid LumberFamilyId { get; set; }

        [ForeignKey("LumberFamilyId")]
        public LumberFamily Family { get; set; }

        public int Length { get; set; }
    }
}
