using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Data.Models
{
    [Table("LumberCategory")]
    internal class LumberCategory : IDbLumberCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("LumberCategoryId")]
        public List<LumberSubCategory> DbLumberSubCategories { get; set; }

        [NotMapped]
        public IList<IDbLumberSubCategory> LumberSubCategories
        {
            get { return DbLumberSubCategories.ToList<IDbLumberSubCategory>(); }
        }
    }
}
