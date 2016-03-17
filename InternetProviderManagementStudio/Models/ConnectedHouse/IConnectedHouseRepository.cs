using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models.ConnectedHouse
{
    interface IConnectedHouseRepository : IRepository<ConnectedHouseModel>, IRepositoryAsync<ConnectedHouseModel>
    {
    }
}
