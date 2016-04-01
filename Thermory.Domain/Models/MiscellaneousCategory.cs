using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class MiscellaneousCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("MiscellaneousCategoryId")]
        public List<MiscellaneousSubCategory> MiscellaneousSubCategories { get; set; }
    }
}
