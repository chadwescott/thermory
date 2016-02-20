using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Data.Models
{
    internal class MiscellaneousSubCategory : IDbMiscellaneousSubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid MiscellaneousCategoryId { get; set; }

        [ForeignKey("MiscellaneousCategoryId")]
        public MiscellaneousCategory DbMiscellaneousCategory { get; set; }

        [NotMapped]
        public IDbMiscellaneousCategory MiscellaneousCategory { get { return DbMiscellaneousCategory; } }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        [ForeignKey("MiscellaneousSubCategoryId")]
        public IList<MiscellaneousProduct> DbMiscellaneousProducts { get; set; }

        [NotMapped]
        public IList<IDbMiscellaneousProduct> MiscellaneousProducts
        {
            get { return DbMiscellaneousProducts.ToList<IDbMiscellaneousProduct>(); }
        }
    }
}
