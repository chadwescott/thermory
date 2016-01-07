using System.ComponentModel.DataAnnotations.Schema;
using Thermory.Domain;

namespace Thermory.Data.Models
{
    [Table("LumberFamily")]
    internal class LumberFamily : ProductFamily, ILumberFamily
    {
        public int Thickness { get; set; }

        public int Width { get; set; }
    }
}
