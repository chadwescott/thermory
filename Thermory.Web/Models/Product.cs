using System;
using Thermory.Domain;

namespace Thermory.Web.Models
{
    public class Product : IProduct
    {
        public Guid Id { get; set; }
    }
}