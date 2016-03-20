using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories
{
    public interface IEntityRepository<T>
    {
        T Get(int Id);
        IEnumerable<T> GetAll();
        int Insert(T item, int AdministratorId);
        void Update(T item, int AdministratorId);
    }
}
