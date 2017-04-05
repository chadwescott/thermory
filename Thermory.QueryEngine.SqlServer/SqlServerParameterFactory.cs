using System.Data;
using System.Data.SqlClient;

namespace Thermory.QueryEngine.SqlServer
{
    internal class SqlServerParameterFactory : IDataParameterFactory
    {
        public IDataParameter MakeDbParameter(string name, int index, object value)
        {
            return new SqlParameter(string.Format("@{0}{1}", name, index), value);
        }
    }
}
