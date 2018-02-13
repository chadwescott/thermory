namespace Thermory.Web.Api.Constants
{
    public class Pagination
    {
        public const int DefaultPageSize = 50;
        public const int MaxPageSize = 500;

        public const string First = "first";
        public const string Previous = "prev";
        public const string Next = "next";
        public const string Last = "last";

        public const string PageNumber = "pageNumber";
        public const string PageSize = "pageSize";

        public const string LinkHeaderTemplate = "<{0}>;rel=\"{1}\"";
    }
}