using Ipms.Models;
using System.Collections.Generic;

namespace Ipms.Repositories
{
    public interface IActionLogRepository
    {
        IEnumerable<ActionLogItemModel> GetAll();
    }
}
