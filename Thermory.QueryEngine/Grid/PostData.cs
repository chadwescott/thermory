using System.Collections.Generic;

namespace Thermory.QueryEngine.Grid
{
    public class PostData<T>
    {
        public string cmd { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public List<string> selected { get; set; }
        public List<T> changes { get; set; }
        public string searchLogic { get; set; }
        public List<SearchFilter> search { get; set; }
        public List<SortOption> sort { get; set; }
    }
}
