using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


// Generic repository fro crud operations


namespace Dapper.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        Task<int> AddAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> ExistAsync(int id);
    }
}
