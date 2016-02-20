using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Data.Models
{
    [Table("MiscellaneousCategory")]
    internal class MiscellaneousCategory : IDbMiscellaneousCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("MiscellaneousCategoryId")]
        public List<MiscellaneousSubCategory> DbMiscellaneousSubCategories { get; set; }

        [NotMapped]
        public IList<IDbMiscellaneousSubCategory> MiscellaneousSubCategories
        {
            get { return DbMiscellaneousSubCategories.ToList<IDbMiscellaneousSubCategory>(); }
        }
    }
}
