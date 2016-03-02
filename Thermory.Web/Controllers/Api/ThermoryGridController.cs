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
    public abstract class ThermoryGridController<TM, TV> : ApiController
        where TV : TM, IViewModel
    {
        public BaseResponse Post(PostData<TV> data)
        {
            switch (data.cmd)
            {
                case "get-records":
                    return (HttpContext.Current != null && WebSecurity.CurrentUserId > 0)
                        ? GetResults(data)
                        : null;
                case "delete-records":
                    return Delete(data.selected ?? new List<string>());
                case "save-records":
                    return Save(data);
            }

            return null;
        }

        private BaseResponse GetResults(PostData<TV> data)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var adapter = new SqlServerAdapter<TM>(connectionString);

                // Build search item list
                List<SearchItem> searchItems = null;
                if (data.search != null)
                {
                    var searchItemBuilder = new SqlServerSearchItemBuilder();
                    searchItems = data.search.Select(searchItemBuilder.BuildSearchItem<TM>).ToList();
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
                    conditions.SortOptionSql = data.sort.Select(o => o.BuildSql<TM>()).ToList();
                }

                var result = adapter.GetRecords(conditions);

                // Return the account view response
                return new GetResponse<TV>
                {
                    status = ResponseCodes.Success,
                    total = currentCount,
                    records = result.Select(CreateViewModel).ToList()
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


        protected ModelAdapter<TM> CreateModelAdapter()
        {
            return new ModelAdapter<TM>();
        }

        protected abstract TV CreateViewModel(TM model);

        protected virtual List<SearchItem> GetRequiredSearchItem()
        {
            return null;
        }

        private BaseResponse Save(PostData<TV> data)
        {
            if (data.changes == null)
                return new GetResponse<TV> { status = ResponseCodes.Success };
            try
            {
                var adapter = CreateModelAdapter();
                data.changes.ForEach(view => adapter.Save(view));

                return new GetResponse<TV>
                {
                    status = ResponseCodes.Success
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { status = ResponseCodes.Error, message = ex.Message };
            }
        }

        protected virtual BaseResponse Delete(List<string> deleteIds)
        {
            try
            {
                var adapter = CreateModelAdapter();
                deleteIds.ForEach(adapter.Delete);

                return new GetResponse<TV>
                {
                    status = ResponseCodes.Success
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { status = ResponseCodes.Error, message = ex.Message };
            }
        }
    }
}