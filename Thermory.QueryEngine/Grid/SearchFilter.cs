using System.Collections.Generic;

namespace Thermory.QueryEngine.Grid
{
    public class SearchFilter
    {
        public string field { get; set; }
        public string type { get; set; }
        public List<object> value { get; set; }
        public string @operator { get; set; }
    }
}
