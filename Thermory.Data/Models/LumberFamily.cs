using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Data.Models
{
    [Table("LumberFamily")]
    internal class LumberFamily : ProductFamily, IDbLumberFamily
    {
        public int Thickness { get; set; }

        public int Width { get; set; }
    }
}
