using Ipms.Models;

namespace Ipms.Repositories
{
    public interface ICustomerRepository : IEntityRepository<CustomerModel>
    {
        void GetCharge(int administratorId);
        void AddFunds(int customerId, decimal count, int administratorId);
    }
}
