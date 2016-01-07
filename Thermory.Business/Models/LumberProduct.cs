using Thermory.Domain;

namespace Thermory.Business.Models
{
    internal class LumberProduct : Product<ILumberSubCategory>, ILumberProduct
    {
        public int Length { get; set; }
    }
}
