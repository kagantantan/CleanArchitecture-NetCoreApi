using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Idil.Boilerplate.Core.Models;

namespace Idil.Boilerplate.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
