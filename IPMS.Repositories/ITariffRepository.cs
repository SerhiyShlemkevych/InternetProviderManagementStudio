using Ipms.Models;

namespace Ipms.Repositories
{
    public interface ITariffRepository : IEntityRepository<TariffModel>
    {
        void Archive(TariffModel target, TariffModel substitute, int AdministratorId);
    }
}
