using System;
using System.Collections.Generic;
using System.Text;

namespace DClean.Application.Filters
{
    public class PagedRequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PagedRequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PagedRequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }

        public int GetSkip()
        {
            return PageNumber - 1 * PageSize; 
        }

        public int GetTake()
        {
            return PageSize;
        }
    }
}
