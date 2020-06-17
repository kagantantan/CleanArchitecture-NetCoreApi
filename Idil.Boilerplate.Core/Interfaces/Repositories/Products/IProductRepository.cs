using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Models;

namespace Idil.Boilerplate.Core.Interfaces.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetAll(int pageSize, int pageNumber);
        Task<int> GetProductCount();
    }
}
