using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories
{
    public interface IRepositoryAsync<T>
    {
        Task<T> GetAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(int Id);
        Task<int> InsertAsync(T item);
        Task UpdateAsync(T item);
    }
}
