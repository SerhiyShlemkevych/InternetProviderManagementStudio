using Ipms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.Repositories
{
    public interface ICustomerRepository : IEntityRepository<CustomerModel>
    {
        void GetCharge(int administratorId);
        void AddFunds(int customerId, decimal count, int administratorId);
    }
}
