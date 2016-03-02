using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Text;

namespace Thermory.QueryEngine.Grid
{
    public class SearchFilter
    {
        public string field { get; set; }
        public string type { get; set; }
        public List<object> value { get; set; }
        public string @operator { get; set; }

        /// <summary>
        /// Builds the search item for the specified type.
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <returns></returns>
        public SearchItem BuildSearchItem<T>(IDataParameterFactory parameterFactory)
        {
            if (value == null || value.Count == 0) { return null; }

            var targetType = typeof(T);

            var property = targetType.GetProperty(field);
            if (property != null)
            {
                var column = property.GetCustomAttribute<ColumnAttribute>();
                if (column != null)
                {
                    var sqlBuilder = new StringBuilder();
                    var item = new SearchItem { Parameters = new List<IDataParameter>() };

                    for (int i = 0; i < value.Count; ++i)
                    {
                        var parameter = parameterFactory.MakeDbParameter(column.Name, i, value[i]);
                        item.Parameters.Add(parameter);

                        if (i > 0) { sqlBuilder.Append(" OR "); }

                        switch (@operator)
                        {
                            case SearchOperators.Is:
                                sqlBuilder.AppendFormat("{0} = :{1}", column.Name, parameter.ParameterName);
                                break;
                            case SearchOperators.Contains:
                                sqlBuilder.AppendFormat("{0} LIKE '%' || :{1} || '%'", column.Name, parameter.ParameterName);
                                break;
                            case SearchOperators.Begins:
                                sqlBuilder.AppendFormat("{0} LIKE :{1} || '%'", column.Name, parameter.ParameterName);
                                break;
                            case SearchOperators.Ends:
                                sqlBuilder.AppendFormat("{0} LIKE '%' || :{1}", column.Name, parameter.ParameterName);
                                break;
                        }
                    }

                    item.DetailSql = sqlBuilder.ToString();
                    return item;
                }
            }

            return null;
        }
    }
}
