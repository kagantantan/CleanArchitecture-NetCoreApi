using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Dtos
{
    public class PagedResponse<T> : BaseResponseDto<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.IsSuccess = true;
            this.Errors = null;
        }
    }
}
