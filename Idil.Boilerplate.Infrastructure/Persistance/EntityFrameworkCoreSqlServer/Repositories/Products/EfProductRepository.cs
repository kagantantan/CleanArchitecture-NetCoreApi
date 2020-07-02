using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Interfaces.Repositories.Products;
using Idil.Boilerplate.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Idil.Boilerplate.Infrastructure.Persistance.EntityFrameworkCoreSqlServer.Repositories.Products
{
    public class EfProductRepository : EfRepository<Product>, IProductRepository
    {
        private readonly ApplicationContext _context;

        public EfProductRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IReadOnlyList<Product>> GetAll(int pageSize, int pageNumber)
        {
             return await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
