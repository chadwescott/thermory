using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Data.Models
{
    internal class LumberType : IDbLumberType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid LumberSubCategoryId { get; set; }

        [ForeignKey("LumberSubCategoryId")]
        public LumberSubCategory DbLumberSubCategory { get; set; }

        public IDbLumberSubCategory LumberSubCategory { get { return DbLumberSubCategory; } }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("LumberTypeId")]
        public List<LumberProduct> DbLumberProducts { get; set; }

        [NotMapped]
        public IList<IDbLumberProduct> LumberProducts
        {
            get { return DbLumberProducts.ToList<IDbLumberProduct>(); }
        }
    }
}
