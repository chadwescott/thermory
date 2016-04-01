using System.Collections.Generic;

namespace Thermory.QueryEngine
{
    public interface IDataAdapter<T>
    {
        int GetRecordCount(QueryConditions conditions = null);
        
        List<T> GetRecords(QueryConditions conditions = null);
    }
}
