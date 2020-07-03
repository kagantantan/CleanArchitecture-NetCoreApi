using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Dtos;
using Idil.Boilerplate.Core.Dtos.Products;
using Idil.Boilerplate.Core.Interfaces;
using Idil.Boilerplate.Core.Interfaces.Repositories;
using Idil.Boilerplate.Core.Models;
using MediatR;

namespace Idil.Boilerplate.Core.Services.Products.Queries
{
    public class GetProductsHandler : IRequestHandler<ProductRequest, BaseResponseDto<List<ProductDto>>>
    {
        private readonly IRepository<Product> _repository;
        public GetProductsHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<BaseResponseDto<List<ProductDto>>> Handle(ProductRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<List<ProductDto>>(new List<ProductDto>());
            var allProduct = await _repository.ListAllAsync();
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
