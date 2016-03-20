using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories
{
    public interface ICustomerRepository : IEntityRepository<CustomerModel>
    {
        void GetCharge(int AdministratorId);
    }
}
