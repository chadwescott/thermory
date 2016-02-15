using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("ProductFamily")]
    internal class ProductFamily : IDbProductFamily
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        [Column("ParentId")]
        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public ProductFamily DbProductFamily { get; set; }

        public List<ProductFamily> ChildProductFamilies { get; set; }

        [Column("SortOrder")]
        public int SortOrder { get; set; }
        
        [NotMapped]
        public IDbProductFamily Parent { get { return DbProductFamily; } }
    }
}
