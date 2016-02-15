using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("LumberProduct")]
    internal class LumberProduct : Product, IDbLumberProduct
    {
        public int Length { get; set; }
    }
}
