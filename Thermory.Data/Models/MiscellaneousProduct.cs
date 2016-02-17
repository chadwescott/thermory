using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("MiscellaneousProduct")]
    internal class MiscellaneousProduct : IDbMiscellaneousProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MiscellaneousSubCategoryId { get; set; }

        [ForeignKey("MiscellaneousSubCategoryId")]
        public MiscellaneousSubCategory DbMiscellaneousSubCategory { get; set; }

        [NotMapped]
        public IDbMiscellaneousSubCategory MiscellaneousSubCategory { get { return DbMiscellaneousSubCategory; } }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
    }
}
