using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Domain.Models
{
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public int PackageNumber { get; set; }

        public double? Height { get; set; }

        public double? Length { get; set; }

        public double? Width { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        [ForeignKey("PackageId")]
        public List<PackageLumberLineItem> PackageLumberLineItems { get; set; }

        [ForeignKey("PackageId")]
        public List<PackageMiscellaneousLineItem> PackageMiscellaneousLineItems { get; set; }

        [NotMapped]
        public string Dimensions
        {
            get
            {
                return Height == null || Length == null || Width == null
                    ? ""
                    : string.Format("{0} x {1} x {2}", Height, Length, Width);
            }
        }

        [NotMapped]
        public double Weight
        {
            get
            {
                return (Order == null ? 0 : Order.PackagingType.Weight ?? 0) + (PackageLumberLineItems == null ? 0 : PackageLumberLineItems.Sum(li => li.Weight)) +
                    (PackageMiscellaneousLineItems == null ? 0 : PackageMiscellaneousLineItems.Sum(li => li.Weight));
            }
        }
    }
}
