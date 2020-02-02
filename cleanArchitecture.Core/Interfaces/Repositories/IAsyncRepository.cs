using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cleanArchitecture.Core.Entities;

namespace cleanArchitecture.Core.Interfaces.Repositories
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();        
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);        
    }
}
