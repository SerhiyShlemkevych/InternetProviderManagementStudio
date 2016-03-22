using Ipms.Models;

namespace Ipms.Repositories
{
    public interface IAdministratorRepository
    {
        AdministratorModel Authenticate(string login, string password); 
    }
}
