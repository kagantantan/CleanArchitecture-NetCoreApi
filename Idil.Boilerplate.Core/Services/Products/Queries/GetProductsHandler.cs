using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Dtos.Products;
using Idil.Boilerplate.Core.Interfaces;
using Idil.Boilerplate.Core.Models;
using MediatR;

namespace Idil.Boilerplate.Core.Services.Products.Queries
{
    public class GetProductsHandler : IRequestHandler<ProductRequest, ProductResponse>
    {
        private readonly IRepository<Product> _repository;
        public GetProductsHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task<ProductResponse> Handle(ProductRequest request, CancellationToken cancellationToken)
        {
            var allProduct = await _repository.ListAllAsync();
            ProductResponse response = new ProductResponse();
            foreach (var product in allProduct)
            {
                response.Products.Add(new ProductDto()
                {
                    Price = product.Price,
                    Name = product.Name
                });
            }

            return response;
        }
    }
}
