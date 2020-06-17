using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Dtos
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationRequest()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationRequest(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
