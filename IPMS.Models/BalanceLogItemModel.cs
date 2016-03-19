using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Models
{
    class BalanceLogItemModel
    {
        public int Id
        {
            get;
            set;
        }

        public int CustomerId
        {
            get;
            set;
        }

        public decimal Amount
        {
            get;
            set;
        }

        public decimal Balance
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
