using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Data.Models
{
    [Table("LumberSubCategory")]
    internal class LumberSubCategory : IDbLumberSubCategory
    {
        public Guid Id { get; set; }

        public Guid LumberCategoryId { get; set; }

        [ForeignKey("LumberCategoryId")]
        public LumberCategory DbLumberCategory { get; set; }

        [NotMapped]
        public IDbLumberCategory LumberCategory { get { return DbLumberCategory; } }

        public string Name { get; set; }

        public int Width { get; set; }

        public int Thickness { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("LumberSubCategoryId")]
        public List<LumberType> DbLumberTypes { get; set; }

        [NotMapped]
        public IList<IDbLumberType> LumberTypes
        {
            get { return DbLumberTypes.ToList<IDbLumberType>(); }
        }
    }
}
