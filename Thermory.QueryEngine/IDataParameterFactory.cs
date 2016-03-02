using System.Data;

namespace Thermory.QueryEngine
{
    public interface IDataParameterFactory
    {
        IDataParameter MakeDbParameter(string name, int index, object value);
    }
}
