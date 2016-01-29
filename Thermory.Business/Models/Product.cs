using System;
using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class Product<T> : IProduct<T>
        where T : IProductType
    {
        public Guid Id { get; set; }

        public T ProductType { get; set; }
    }
}
