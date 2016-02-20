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

        public Guid LumberTypeId { get; set; }

        public IDbLumberType LumberType { get; set; }

        public int Length { get; set; }

        public int Quantity { get; set; }
    }
}
