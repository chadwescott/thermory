using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Thermory.QueryEngine;
using Thermory.QueryEngine.Grid;
using Thermory.QueryEngine.Grid.Response;

namespace Thermory.Web.Grid
{
    public abstract class GridApiController<TModel, TViewModel> : ApiController
        where TViewModel : TModel, IViewModel
    {
        protected abstract ModelAdapter<TModel> CreateModelAdapter();

        protected abstract TViewModel CreateViewModel(TModel model);

        protected abstract IDataAdapter<TModel> GetAdapter();
        
        protected abstract SearchItemBuilder GetSearchItemBuilder();

        public BaseResponse Post([FromBody]PostData<TViewModel> data)
        {
            switch (data.cmd)
            {
                case "get-records":
                    return (HttpContext.Current != null && HttpContext.Current.Session != null)
                        ? GetResults(data)
                        : null;
                case "delete-records":
                    return Delete(data.selected ?? new List<string>());
                case "save-records":
                    return Save(data);
            }

            return null;
        }

        private BaseResponse GetResults(PostData<TViewModel> data)
        {
            try
            {
                var adapter = GetAdapter();

                // Build search item list
                List<SearchItem> searchItems = null;
                if (data.search != null)
                {
                    var searchItemBuilder = GetSearchItemBuilder();
                    searchItems = data.search.Select(searchItemBuilder.BuildSearchItem<TModel>).ToList();
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
                    conditions.SortOptionSql = data.sort.Select(o => o.BuildSql<TModel>()).ToList();
                }

                var result = adapter.GetRecords(conditions);

                // Return the account view response
                return new GetResponse<TViewModel>
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

        protected virtual List<SearchItem> GetRequiredSearchItem()
        {
            return null;
        }

        protected virtual BaseResponse Delete(List<string> deleteIds)
        {
            try
            {
                var adapter = CreateModelAdapter();
                deleteIds.ForEach(adapter.Delete);

                return new GetResponse<TViewModel>
                {
                    status = ResponseCodes.Success
                };
            }
            catch (Exception ex)
            {
                return new ErrorResponse { status = ResponseCodes.Error, message = ex.Message };
            }
        }

        private BaseResponse Save(PostData<TViewModel> data)
        {
            if (data.changes == null)
                return new GetResponse<TViewModel> { status = ResponseCodes.Success };
            try
            {
                var adapter = CreateModelAdapter();
                data.changes.ForEach(view => adapter.Save(view));

                return new GetResponse<TViewModel>
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