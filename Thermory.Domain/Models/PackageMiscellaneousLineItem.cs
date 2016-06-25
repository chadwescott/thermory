using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    public class PackageMiscellaneousLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid PackageId { get; set; }

        [ForeignKey("PackageId")]
        public Package Package { get; set; }

        public Guid MiscellaneousProductId { get; set; }

        [ForeignKey("MiscellaneousProductId")]
        public MiscellaneousProduct MiscellaneousProduct { get; set; }

        public int Quantity { get; set; }
    }
}
