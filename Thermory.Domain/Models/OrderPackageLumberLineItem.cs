﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain.Utils;

namespace Thermory.Domain.Models
{
    public class OrderPackageLumberLineItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid OrderPackageId { get; set; }

        [ForeignKey("OrderPackageId")]
        public OrderPackage OrderPackage { get; set; }

        public Guid LumberProductId { get; set; }

        [ForeignKey("LumberProductId")]
        public LumberProduct LumberProduct { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public string PacksHtml
        {
            get { return HtmlHelpers.GetPacksHtml(Quantity, LumberProduct.LumberType.LumberSubCategory.BundleSize); }
        }

        [NotMapped]
        public double LinearFeet { get { return LumberProduct.LengthInInches * Quantity; } }

        [NotMapped]
        public double SquareFeet { get { return Math.Round(LinearFeet * LumberProduct.LumberType.LumberSubCategory.WidthInInches / 12, 0); } }
    }
}