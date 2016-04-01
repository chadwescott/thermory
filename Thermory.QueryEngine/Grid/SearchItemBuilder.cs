using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace Thermory.QueryEngine.Grid
{
    public abstract class SearchItemBuilder
    {
        protected abstract IDataParameterFactory GetDbParameterFactory();

        /// <summary>
        /// Builds the search item for the specified type.
        /// </summary>
        /// <typeparam name="T">The specified type</typeparam>
        /// <returns></returns>
        public SearchItem BuildSearchItem<T>(SearchFilter filter)
        {
            if (filter.value == null || filter.value.Count == 0) { return null; }

            var targetType = typeof(T);

            var property = targetType.GetProperty(filter.field);
            if (property != null)
            {
                var column = property.GetCustomAttribute<ColumnAttribute>();
                if (column != null)
                {
                    var sqlBuilder = new StringBuilder();
                    var item = new SearchItem { Parameters = new List<IDataParameter>() };

                    for (int i = 0; i < filter.value.Count; ++i)
                    {
                        var parameter = GetDbParameterFactory().MakeDbParameter(column.Name, i, filter.value[i]);
                        item.Parameters.Add(parameter);

                        if (i > 0) { sqlBuilder.Append(" OR "); }

                        sqlBuilder.AppendFormat(BuildCondition(column.Name, parameter, filter.@operator));
                    }

                    item.DetailSql = sqlBuilder.ToString();
                    return item;
                }
            }

            return null;
        }

        protected abstract string BuildCondition(string columnName, IDataParameter parameter, string @operator);
    }
}
