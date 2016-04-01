using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Thermory.QueryEngine
{
    public abstract class DatabaseAdapter<T> : IDataAdapter<T>
    {
        protected readonly string ConnectionString;
        protected Type TargetType = typeof(T);

        protected List<PropertyInfo> Properties = null;
        protected string TargetTable = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAdapter{T}"/> class.
        /// </summary>
        protected DatabaseAdapter(string connectionString)
        {
            ConnectionString = connectionString;
            Properties = TargetType.GetProperties().ToList();

            var table = TargetType.GetCustomAttribute<TableAttribute>();
            if (table != null)
            {
                TargetTable = table.Name;
            }
        }

        /// <summary>
        /// Gets the record count for the current type's mapped table.
        /// </summary>
        /// <param name="conditions">The search items.</param>
        /// <returns></returns>
        public int GetRecordCount(QueryConditions conditions = null)
        {
            if (string.IsNullOrWhiteSpace(TargetTable)) { return 0; }
            var searchItems = conditions.SearchItems;
            var searchLogic = conditions.SearchLogic;

            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(connection))
                {
                    // Get the overall record count
                    const string countQuery = @"SELECT COUNT(1) FROM {0}";
                    var queryBuilder = new StringBuilder().AppendFormat(countQuery, TargetTable);

                    if (searchItems != null || conditions.RequiredSearchItems.Any())
                    {
                        var first = true;
                        conditions.RequiredSearchItems.ForEach(delegate(SearchItem item)
                        {
                            if (first)
                            {
                                first = false;
                                queryBuilder.AppendFormat(" WHERE ({0}", item.DetailSql);
                            }
                            else
                                queryBuilder.AppendFormat(" AND {0}", item.DetailSql);
                            item.Parameters.ForEach(
                                parameter =>
                                    command.Parameters.Add(CreateParameter(parameter.ParameterName, parameter.Value)));
                        });

                        if (conditions.RequiredSearchItems.Any())
                            queryBuilder.AppendFormat(")");

                        first = true;
                        searchItems.ForEach(
                            delegate(SearchItem item)
                            {
                                if (first)
                                {
                                    first = false;
                                    queryBuilder.AppendFormat(conditions.RequiredSearchItems.Any() ? " AND (({0})" : " WHERE ({0})",
                                        item.DetailSql);
                                }
                                else
                                    queryBuilder.AppendFormat(" {0} ({1})", searchLogic, item.DetailSql);

                                item.Parameters.ForEach(
                                    parameter =>
                                        command.Parameters.Add(CreateParameter(parameter.ParameterName, parameter.Value)));
                            });

                        if (conditions.RequiredSearchItems.Any() && searchItems.Any())
                        {
                            queryBuilder.AppendFormat(")");
                        }
                    }

                    command.CommandText = queryBuilder.ToString();
                    connection.Open();

                    int count;
                    int.TryParse(Convert.ToString(command.ExecuteScalar()), out count);
                    return count;
                }
            }
        }

        /// <summary>
        /// Gets the record list for this instance's target type.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public List<T> GetRecords(QueryConditions conditions = null)
        {
            var records = new List<T>();

            using (var connection = CreateConnection())
            {
                using (var command = CreateCommand(connection))
                {
                    // Build and execute the main query
                    var queryBuilder = new StringBuilder("SELECT * FROM (SELECT a.*,");
                    if (conditions != null)
                    {
                        if (conditions.SortOptionSql != null)
                            queryBuilder.AppendFormat(" ROW_NUMBER() OVER (ORDER BY {0})", string.Join(", ", conditions.SortOptionSql.ToArray()));
                        else
                            queryBuilder.Append(GetRowNumber());

                        queryBuilder.AppendFormat(" rn FROM {0} a", TargetTable);

                        if (conditions.SearchItems != null || conditions.RequiredSearchItems.Any())
                        {
                            var first = true;
                            conditions.RequiredSearchItems.ForEach(delegate(SearchItem item)
                            {
                                if (first)
                                {
                                    first = false;
                                    queryBuilder.AppendFormat(" WHERE ({0}", item.DetailSql);
                                }
                                else
                                    queryBuilder.AppendFormat(" AND {0}", item.DetailSql);
                                item.Parameters.ForEach(
                                    parameter =>
                                        command.Parameters.Add(CreateParameter(parameter.ParameterName, parameter.Value)));
                            });

                            if (conditions.RequiredSearchItems.Any())
                                queryBuilder.AppendFormat(")");

                            first = true;

                            conditions.SearchItems.ForEach(
                                delegate(SearchItem item)
                                {
                                    if (first)
                                    {
                                        first = false;
                                        queryBuilder.AppendFormat(
                                            conditions.RequiredSearchItems.Any() ? " AND (({0})" : " WHERE ({0})", item.DetailSql);
                                    }
                                    else
                                    {
                                        queryBuilder.AppendFormat(" {0} ({1})", conditions.SearchLogic, item.DetailSql);
                                    }
                                    item.Parameters.ForEach(delegate(IDataParameter parameter) { command.Parameters.Add(parameter); });
                                });

                            if (conditions.RequiredSearchItems.Any() && conditions.SearchItems.Any())
                            {
                                queryBuilder.AppendFormat(")");
                            }
                        }

                        queryBuilder.Append(GetSubQueryTerminator());

                        if (conditions.RecordLimit > 0)
                        {
                            queryBuilder.AppendFormat(" WHERE rn > {0} AND rn <= {1}", conditions.SkipOffset, conditions.SkipOffset + conditions.RecordLimit);
                        }
                    }
                    else
                    {
                        queryBuilder.AppendFormat(" {0} rn FROM {1} a)", GetRowNumber(), TargetTable);
                    }

                    connection.Open();
                    command.CommandText = queryBuilder.ToString();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            records.Add(FillModel(reader));
                        }
                    }
                }
            }

            return records;
        }

        protected virtual string GetSubQueryTerminator()
        {
            return ")";
        }

        protected abstract string GetRowNumber();

        protected abstract DbConnection CreateConnection();

        protected abstract DbCommand CreateCommand(DbConnection connection);

        protected abstract DbParameter CreateParameter(string name, object value);


        /// <summary>
        /// Fills a data model of the current type from the provided reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        protected T FillModel(IDataRecord reader)
        {
            var result = Activator.CreateInstance<T>();

            Properties.ForEach(
                delegate(PropertyInfo property)
                {
                    var column = property.GetCustomAttribute<ColumnAttribute>();
                    if (column == null) return;

                    var value = DataReaderUtility.GetPropertyValueFromDataReaderColumn(reader, property, column);

                    property.SetValue(result, value);
                });

            return result;
        }
    }
}
