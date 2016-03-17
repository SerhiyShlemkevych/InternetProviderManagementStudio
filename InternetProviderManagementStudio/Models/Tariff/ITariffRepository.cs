using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.Models.Tariff
{
    interface ITariffRepository : IRepository<TariffModel>, IRepositoryAsync<TariffModel>
    {
            
    }
}
