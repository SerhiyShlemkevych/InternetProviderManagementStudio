using System.Collections.Generic;

namespace Ipms.Repositories
{
    public interface IEntityRepository<T>
    {
        T Get(int Id);
        IEnumerable<T> GetAll();
        int Insert(T item, int AdministratorId);
        void Update(T item, int AdministratorId);
    }
}
