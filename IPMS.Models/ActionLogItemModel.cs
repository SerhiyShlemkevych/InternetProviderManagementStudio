using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.Models
{
    public class ActionLogItemModel
    {
        public int Id
        {
            get;
            set;
        }
        public AdministratorModel Administrator
        {
            get;
            set;
        }
        public string Target
        {
            get;
            set;
        }
        public string Action
        {
            get;
            set;
        }
        public string AffectedRowIds
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
    }
}
