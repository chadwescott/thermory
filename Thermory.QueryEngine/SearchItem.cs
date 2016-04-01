using System.Collections.Generic;
using System.Data;

namespace Thermory.QueryEngine
{
    public class SearchItem
    {
        /// <summary>
        /// Gets or sets the detail SQL.
        /// </summary>
        /// <value>
        /// The detail SQL.
        /// </value>
        public string DetailSql { get; set; }
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public List<IDataParameter> Parameters { get; set; }
    }
}