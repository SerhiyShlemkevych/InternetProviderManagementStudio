using Ipms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.Repositories
{
    public interface ITariffRepository : IEntityRepository<TariffModel>
    {
        void Archive(TariffModel target, TariffModel substitute, int AdministratorId);
    }
}
