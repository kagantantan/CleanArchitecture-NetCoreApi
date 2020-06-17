using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Idil.Boilerplate.Core.Dtos.Products
{
    public class ProductPaginationRequest: PaginationRequest, IRequest<PagedResponse<List<ProductDto>>>
    {
    }
}
