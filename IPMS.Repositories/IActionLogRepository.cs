using Ipms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.Repositories
{
    public interface IActionLogRepository
    {
        IEnumerable<ActionLogItemModel> GetAll();
    }
}
