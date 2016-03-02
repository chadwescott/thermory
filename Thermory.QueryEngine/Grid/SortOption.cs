using System.Data.Linq.Mapping;
using System.Reflection;

namespace Thermory.QueryEngine.Grid
{
    public class SortOption
    {
        public string field { get; set; }
        public string direction { get; set; }

        /// <summary>
        /// Builds the SQL for the specified type based on the current field and direction.
        /// </summary>
        /// <typeparam name="T">The type to target</typeparam>
        /// <returns></returns>
        public string BuildSql<T>()
        {
            var targetType = typeof(T);

            var property = targetType.GetProperty(field);
            if (property == null) return null;
            var column = property.GetCustomAttribute<ColumnAttribute>();
            return column != null ? string.Format("{0} {1}", column.Name, direction) : null;
        }
    }
}
