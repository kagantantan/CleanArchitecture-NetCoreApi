using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Idil.Boilerplate.Core.Dtos.Products
{
    public class ProductRequest : BaseRequestDto, IRequest<BaseResponseDto<List<ProductDto>>>
    {
    }
}
