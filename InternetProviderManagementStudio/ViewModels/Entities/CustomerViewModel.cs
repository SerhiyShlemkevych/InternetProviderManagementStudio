using IPMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.ViewModels.Entities
{
    class CustomerViewModel : ViewModel
    {
        private int _id;
        private string _forename;
        private string _surname;
        private ConnectedHouseViewModel _house;
        private string _flat;
        private TariffViewModel _tariff;
        private decimal _balance;
        private string _macAddress;
        private string _ipAddress;
        private CustomerState _state;
        private DateTime _lastChargedDate;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }
        public string Forename
        {
            get
            {
                return _forename;
            }
            set
            {
                _forename = value;
                RaisePropertyChanged("Forename");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public ConnectedHouseViewModel House
        {
            get
            {
                return _house;
            }
            set
            {
                _house = value;
                RaisePropertyChanged("House");
            }
        }
        public string Flat
        {
            get
            {
                return _flat;
            }
            set
            {
                _flat = value;
                RaisePropertyChanged("Flat");
            }
        }
        public TariffViewModel Tariff
        {
            get
            {
                return _tariff;
            }
            set{
                _tariff = value;
                RaisePropertyChanged("Tariff");
            }
        }
        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                RaisePropertyChanged("Balance");
            }
        }
        public string MacAddress
        {
            get
            {
                return _macAddress;
            }
            set
            {
                _macAddress = value;
                RaisePropertyChanged("MacAddress");
            }
        }
        public CustomerState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                RaisePropertyChanged("State");
            }
        }
        public string IpAddress
        {
            get
            {
                return _ipAddress;
            }
            set
            {
                _ipAddress = value;
                RaisePropertyChanged("IpAddress");
            }
        }
        public DateTime LastChargedDate
        {
            get
            {
                return _lastChargedDate;
            }
            set
            {
                _lastChargedDate = value;
                RaisePropertyChanged("LastChargedDate");
            }
        }
    }
}
