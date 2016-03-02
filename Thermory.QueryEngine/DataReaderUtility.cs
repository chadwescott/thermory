using System;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace Thermory.QueryEngine
{
    internal static class DataReaderUtility
    {
        private static readonly Type NullableCharType = typeof(char?);
        private static readonly Type NullableDateTimeType = typeof(DateTime?);
        private static readonly Type DateTimeType = typeof(DateTime);
        private static readonly Type IntType = typeof(int);
        private static readonly Type BoolType = typeof(bool);

        public static object GetPropertyValueFromDataReaderColumn(IDataRecord reader, PropertyInfo property, ColumnAttribute column)
        {
            if (property.PropertyType == NullableCharType)
                return (reader.IsDBNull(reader.GetOrdinal(column.Name)))
                    ? new char?()
                    : Convert.ToString(reader[column.Name]).FirstOrDefault();

            if (property.PropertyType == NullableDateTimeType)
                return (reader.IsDBNull(reader.GetOrdinal(column.Name)))
                    ? new DateTime?()
                    : reader.GetDateTime(reader.GetOrdinal(column.Name));

            if (property.PropertyType == DateTimeType)
                return reader.GetDateTime(reader.GetOrdinal(column.Name));

            if (property.PropertyType == IntType)
                return reader.GetInt32(reader.GetOrdinal(column.Name));

            if (property.PropertyType == BoolType)
                return reader.GetBoolean(reader.GetOrdinal(column.Name));

            return !reader.IsDBNull(reader.GetOrdinal(column.Name)) ? Convert.ToString(reader[column.Name]) : null;
        }
    }
}
