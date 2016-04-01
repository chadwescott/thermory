using System.Collections.Generic;

namespace Thermory.QueryEngine
{
    public class QueryConditions
    {
        /// <summary>
        /// Gets or sets the SearchItem that is mandatory in all cases
        /// </summary>
        public List<SearchItem> RequiredSearchItems { get; set; }
        /// <summary>
        /// Gets or sets the skip offset.
        /// </summary>
        /// <value>
        /// The skip offset.
        /// </value>
        public int SkipOffset { get; set; }
        /// <summary>
        /// Gets or sets the record limit.
        /// </summary>
        /// <value>
        /// The record limit.
        /// </value>
        public int RecordLimit { get; set; }
        /// <summary>
        /// Gets or sets the sort options.
        /// </summary>
        /// <value>
        /// The sort options.
        /// </value>
        public List<string> SortOptionSql { get; set; }
        /// <summary>
        /// Gets or sets the search logic operator.
        /// </summary>
        /// <value>
        /// The search logic.
        /// </value>
        public string SearchLogic { get; set; }
        /// <summary>
        /// Gets or sets the search items.
        /// </summary>
        /// <value>
        /// The search items.
        /// </value>
        public List<SearchItem> SearchItems { get; set; }
    }
}
