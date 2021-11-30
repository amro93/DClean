namespace DClean.Application.Filters
{
    public class PagedAndSortedRequestParameter : PagedRequestParameter
    {
        public string OrderBy { get; set; }
        public PagedAndSortedRequestParameter()
        {

        }

        public PagedAndSortedRequestParameter(int pageNumber, int pageSize, string orderBy = null) : base(pageNumber, pageSize)
        {
            OrderBy = orderBy;
        }
    }
}
