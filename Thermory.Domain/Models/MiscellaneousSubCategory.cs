using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class MiscellaneousSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MiscellaneousCategoryId { get; set; }

        [ForeignKey("MiscellaneousCategoryId")]
        public MiscellaneousCategory MiscellaneousCategory { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("MiscellaneousSubCategoryId")]
        public IList<MiscellaneousProduct> MiscellaneousProducts { get; set; }
    }
}
