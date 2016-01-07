using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("LumberProduct")]
    internal class LumberProduct : IDbLumberProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LumberFamilyId { get; set; }

        [ForeignKey("LumberFamilyId")]
        public LumberFamily Family { get; set; }

        public int Length { get; set; }
    }
}
