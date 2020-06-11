using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Dtos.Products
{
    public class ProductResponse : BaseResponseDto
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
