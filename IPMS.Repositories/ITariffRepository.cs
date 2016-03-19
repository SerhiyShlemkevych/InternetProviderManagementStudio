using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Repositories
{
    public interface ITariffRepository : IRepository<TariffModel>, IRepositoryAsync<TariffModel>
    {
        void Archive(TariffModel target, TariffModel substitute, int AdministratorId);
        Task ArchiveAsync(TariffModel target, TariffModel substitute, int AdministratorId);
    }
}
