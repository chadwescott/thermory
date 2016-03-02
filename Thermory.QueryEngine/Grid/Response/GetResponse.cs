using System.Collections.Generic;

namespace Thermory.QueryEngine.Grid.Response
{
    public class GetResponse<T> : BaseResponse
    {
        public int total { get; set; }

        public IEnumerable<T> records { get; set; } 
    }
}
