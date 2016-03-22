using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipms.Models;

namespace Ipms.Repositories
{
    public interface IAdministratorRepository
    {
        AdministratorModel Authenticate(string login, string password); 
    }
}
