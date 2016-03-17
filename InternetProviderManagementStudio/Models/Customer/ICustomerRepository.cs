using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models.Customer
{
    interface ICustomerRepository : IRepository<CustomerModel>, IRepositoryAsync<CustomerModel>
    {
    }
}
