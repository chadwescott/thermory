using System.Data;
using Thermory.QueryEngine.Grid;

namespace Thermory.QueryEngine.SqlServer
{
    public class SqlServerSearchItemBuilder : SearchItemBuilder
    {
        private readonly IDataParameterFactory _parameterFactory = new SqlServerParameterFactory();

        protected override IDataParameterFactory GetDbParameterFactory()
        {
            return _parameterFactory;
        }

        protected override string BuildCondition(string columnName, IDataParameter parameter, string @operator)
        {
            switch (@operator)
            {
                case SearchOperators.Is:
                    return string.Format("{0} = {1}", columnName, parameter.ParameterName);
                case SearchOperators.Contains:
                    parameter.Value = string.Format("%{0}%", parameter.Value);
                    return string.Format("{0} LIKE {1}", columnName, parameter.ParameterName);
                case SearchOperators.Begins:
                    parameter.Value = string.Format("{0}%", parameter.Value);
                    return string.Format("{0} LIKE {1}", columnName, parameter.ParameterName);
                case SearchOperators.Ends:
                    parameter.Value = string.Format("%{0}", parameter.Value);
                    return string.Format("{0} LIKE {1}", columnName, parameter.ParameterName);
                default:
                    return "";
            }
        }
    }
}
