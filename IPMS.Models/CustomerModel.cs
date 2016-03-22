using System;
using System.Collections.Generic;

namespace Ipms.Models
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
        public List<BalanceLogItemModel> BalanceLog
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
