using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Dtos;
using Idil.Boilerplate.Core.Dtos.Products;
using Idil.Boilerplate.Core.Interfaces;
using Idil.Boilerplate.Core.Interfaces.Repositories.Products;
using Idil.Boilerplate.Core.Models;
using MediatR;

namespace Idil.Boilerplate.Core.Services.Products.Queries
{
    public class GetAllProductsHandler : IRequestHandler<ProductPaginationRequest, PagedResponse<List<ProductDto>>>
    {
        private readonly IProductRepository _repository;
        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedResponse<List<ProductDto>>> Handle(ProductPaginationRequest request, CancellationToken cancellationToken)
        {
            var response = new PagedResponse<List<ProductDto>>(new List<ProductDto>(),request.PageNumber, request.PageSize);

            var allProduct = await _repository.GetAll(request.PageSize, request.PageNumber);

            response.TotalRecords = allProduct.Count;

            var totalPages = ((double)response.TotalRecords / (double)request.PageSize);
            response.TotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            foreach (var product in allProduct)
            {
                response.Data.Add(new ProductDto()
                {
                    Price = product.Price,
                    Name = product.Name
                });
            }

            return response;
        }
    }
}
