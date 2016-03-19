using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPMS.Models;

namespace IPMS.Repositories
{
    public interface IAdministratorRepository
    {
        AdministratorModel Authenticate(string login, string password); 
    }
}
