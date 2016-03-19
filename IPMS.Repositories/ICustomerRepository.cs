using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories
{
    public interface ICustomerRepository : IRepository<CustomerModel>, IRepositoryAsync<CustomerModel>
    {
        void GetCharge(int AdministratorId);
        Task GetChargeAsync(int AdministratorId);
    }
}
