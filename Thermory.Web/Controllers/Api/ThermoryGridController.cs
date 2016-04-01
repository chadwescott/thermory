using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using Thermory.QueryEngine;
using Thermory.QueryEngine.Grid;
using Thermory.QueryEngine.Grid.Response;
using Thermory.QueryEngine.SqlServer;
using Thermory.Web.Models;
using WebMatrix.WebData;

namespace Thermory.Web.Controllers.Api
{
    public abstract class ThermoryGridController<T> : ApiController
        where T : IViewModel
    {
        public BaseResponse Post(PostData<T> data)
        {
            switch (data.cmd)
            {
                case "get-records":
                    return (HttpContext.Current != null && WebSecurity.CurrentUserId > 0)
                        ? GetResults(data)
                        : null;
            }

            return null;
        }

        private BaseResponse GetResults(PostData<T> data)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var adapter = new SqlServerAdapter<T>(connectionString);

                // Build search item list
                List<SearchItem> searchItems = null;
                if (data.search != null)
                {
                    var searchItemBuilder = new SqlServerSearchItemBuilder();
                    searchItems = data.search.Select(searchItemBuilder.BuildSearchItem<T>).ToList();
                }

                // Build the paging, sort and filter conditions
                var conditions = new QueryConditions
                {
                    RequiredSearchItems = GetRequiredSearchItem(),
                    SkipOffset = data.offset,
                    RecordLimit = data.limit,
                    SearchLogic = data.searchLogic,
                    SearchItems = searchItems
                };

                // Get the total record count from cache or the database
                int currentCount;
                if (!int.TryParse(Convert.ToString(HttpContext.Current.Session[SessionVariables.DataCount]), out currentCount) || data.offset == 0)
                {
                    currentCount = adapter.GetRecordCount(conditions);
                    HttpContext.Current.Session[SessionVariables.DataCount] = currentCount;
                }

                if (data.sort != null)
                {
                    conditions.SortOptionSql = data.sort.Select(o => o.BuildSql<T>()).ToList();
                }

                var result = adapter.GetRecords(conditions);

                // Return the account view response
                return new GetResponse<T>
                {
                    status = ResponseCodes.Success,
                    total = currentCount,
                    records = result
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse
                {
                    status = ResponseCodes.Error,
                    message = ex.Message
                };
            }
        }

        protected virtual List<SearchItem> GetRequiredSearchItem()
        {
            return null;
        }
    }
}