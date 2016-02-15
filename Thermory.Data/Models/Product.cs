using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("Product")]
    internal class Product : IDbProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ProductFamilyId { get; set; }
        
        [ForeignKey("Id")]
        public ProductInventory DbInventory { get; set; }

        [NotMapped]
        public IDbProductInventory Inventory { get { return DbInventory; } }
    }
}
