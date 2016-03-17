using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models
{
    interface IRepository<T>
    {
        T Get(int Id);
        IEnumerable<T> GetAll();
        void Delete(int Id);
        int Insert(T item);
        void Update(T item);
    }
}
