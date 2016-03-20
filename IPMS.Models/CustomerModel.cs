using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMS.Models
{
    public class CustomerModel
    {
        public int Id
        {
            get;
            set;
        }
        public string Forename
        {
            get;
            set;
        }
        public string Surname
        {
            get;
            set;
        }
        public int HouseId
        {
            get;
            set;
        }
        public string Flat
        {
            get;
            set;
        }
        public int TariffId
        {
            get;
            set;
        }
        public decimal Balance
        {
            get;
            set;
        }
        public string MacAddress
        {
            get;
            set;
        }
        public CustomerState State
        {
            get;
            set;
        }
        public string IpAddress
        {
            get;
            set;
        }
        public DateTime LastChargedDate
        {
            get;
            set;
        }
    }
}
