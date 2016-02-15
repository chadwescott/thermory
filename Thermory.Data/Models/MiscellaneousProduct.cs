using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("MiscellaneousProduct")]
    internal class MiscellaneousProduct : Product, IDbMiscellaneousProduct
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
