using System;
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
            if (property == null) return null;

            var column = property.GetCustomAttribute<ColumnAttribute>();
            if (column == null) return null;

            var sqlBuilder = new StringBuilder();
            var item = new SearchItem { Parameters = new List<IDataParameter>() };

            if (filter.type == "date")
            {
                var startDate = DateTime.Parse(filter.value[0].ToString());
                var endDate =
                    (filter.@operator == "between" ? DateTime.Parse(filter.value[1].ToString()) : startDate)
                        .AddDays(1).AddMilliseconds(-1);
                var startDateParamString = string.Format("{0}StartDate", column.Name);
                var endDateParamString = string.Format("{0}EndDate", column.Name);
                var startDateParam = GetDbParameterFactory().MakeDbParameter(startDateParamString, 0, startDate);
                var endDateParam = GetDbParameterFactory().MakeDbParameter(endDateParamString, 0, endDate);
                item.Parameters.Add(startDateParam);
                item.Parameters.Add(endDateParam);
                sqlBuilder.AppendFormat("{0} BETWEEN {1} AND {2}", column.Name, startDateParam.ParameterName,
                    endDateParam.ParameterName);
            }
            else
            {
                for (int i = 0; i < filter.value.Count; ++i)
                {
                    var parameter = GetDbParameterFactory().MakeDbParameter(column.Name, i, filter.value[i]);
                    item.Parameters.Add(parameter);

                    if (i > 0)
                    {
                        sqlBuilder.Append(" OR ");
                    }

                    sqlBuilder.AppendFormat(BuildCondition(column.Name, parameter, filter.@operator));
                }
            }

            item.DetailSql = sqlBuilder.ToString();
            return item;
        }

        protected abstract string BuildCondition(string columnName, IDataParameter parameter, string @operator);
    }
}
