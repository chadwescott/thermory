using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class MiscellaneousProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MiscellaneousSubCategoryId { get; set; }

        [ForeignKey("MiscellaneousSubCategoryId")]
        public MiscellaneousSubCategory MiscellaneousSubCategory { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal? Weight { get; set; }

        public int SortOrder { get; set; }

        public int Quantity { get; set; }
    }
}
