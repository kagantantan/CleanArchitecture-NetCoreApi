using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Dtos
{
    public class BaseResponseDto<T>
    {
        public BaseResponseDto()
        {
        }
        public BaseResponseDto(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
