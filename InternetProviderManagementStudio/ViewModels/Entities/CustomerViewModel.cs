using Ipms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ipms.UI.ViewModels.Entities
{
    class CustomerViewModel : EntityViewModel
    {
        private Regex _ipRegex = new Regex(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$");
        private Regex _macRegex = new Regex(@"^([0-9a-fA-F]{2}:??){5}([0-9a-fA-F]{2})$");
        private Regex _flatRegex = new Regex(@"^([0-9]{1,})([a-zA-Z]{0,})$");

        private int _id;
        private string _forename;
        private string _surname;
        private ConnectedHouseViewModel _house;
        private string _flat;
        private TariffViewModel _tariff;
        private decimal _balance;
        private List<BalanceLogItemModel> _balanceLog;
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
                ValidateForename();
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
                ValidateSurname();
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
                ValidateFlat();
                RaisePropertyChanged("Flat");
            }
        }
        public TariffViewModel Tariff
        {
            get
            {
                return _tariff;
            }
            set
            {
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

        public List<BalanceLogItemModel> BalanceLog
        {
            get
            {
                return _balanceLog;
            }
            set
            {
                _balanceLog = value;
                RaisePropertyChanged("BalanceLog");
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
                ValidateMacAddress();
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
                ValidateIpAddress();
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

        #region Validation

        public void ValidateUniqe(IEnumerable<CustomerViewModel> otherCustomers)
        {
            ValidateIpAddressUnique(otherCustomers);
            ValidateMacAddressUnique(otherCustomers);
        }

        private void ValidateIpAddressUnique(IEnumerable<CustomerViewModel> otherCustomers)
        {
            if (otherCustomers.Where(c => c.IpAddress == IpAddress && Id != c.Id).Count() > 0)
            {
                ValidationErrors["IpAddress"] = new List<string>() { "IP address must be unique" };
                RaiseErrorsChanged("IpAddress");
            }
            else if (ValidationErrors.ContainsKey("IpAddress"))
            {
                ValidationErrors.Remove("IpAddress");
                RaiseErrorsChanged("IpAddress");
            }
        }

        private void ValidateMacAddressUnique(IEnumerable<CustomerViewModel> otherCustomers)
        {
            if (otherCustomers.Where(c => c.MacAddress == MacAddress && Id != c.Id).Count() > 0)
            {
                ValidationErrors["MacAddress"] = new List<string>() { "Mac address must be unique" };
                RaiseErrorsChanged("MacAddress");
            }
            else if (ValidationErrors.ContainsKey("MacAddress"))
            {
                ValidationErrors.Remove("MacAddress");
                RaiseErrorsChanged("MacAddress");
            }
        }

        public override void Validate()
        {
            ValidateForename();
            ValidateSurname();
            ValidateFlat();
            ValidateIpAddress();
            ValidateMacAddress();
        }

        private void ValidateForename()
        {
            if (string.IsNullOrEmpty(Forename))
            {
                ValidationErrors["Forename"] = new List<string>() { "Forename is required" };
                RaiseErrorsChanged("Forename");
            }
            else if (Forename.Length > 128)
            {
                ValidationErrors["Forename"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("Forename");
            }
            else if (ValidationErrors.ContainsKey("Forename"))
            {
                ValidationErrors.Remove("Forename");
                RaiseErrorsChanged("Forename");
            }
        }

        private void ValidateSurname()
        {
            if (string.IsNullOrEmpty(Surname))
            {
                ValidationErrors["Surname"] = new List<string>() { "Surname is required" };
                RaiseErrorsChanged("Surname");
            }
            else if (Forename.Length > 128)
            {
                ValidationErrors["Surname"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("Surname");
            }
            else if (ValidationErrors.ContainsKey("Surname"))
            {
                ValidationErrors.Remove("Surname");
                RaiseErrorsChanged("Surname");
            }
        }

        private void ValidateFlat()
        {
            if (string.IsNullOrEmpty(Flat))
            {
                ValidationErrors["Flat"] = new List<string>() { "Flat is required" };
                RaiseErrorsChanged("Flat");
            }
            else if (Flat.Length > 16)
            {
                ValidationErrors["Flat"] = new List<string>() { "Max length is limited (16)" };
                RaiseErrorsChanged("Flat");
            }
            else if (!_flatRegex.IsMatch(Flat))
            {
                ValidationErrors["Flat"] = new List<string>() { "Flat has to be in format {number} or {number}{letter}" };
                RaiseErrorsChanged("Flat");
            }
            else if (ValidationErrors.ContainsKey("Flat"))
            {
                ValidationErrors.Remove("Flat");
                RaiseErrorsChanged("Flat");
            }
        }

        private void ValidateIpAddress()
        {
            if (string.IsNullOrEmpty(IpAddress))
            {
                ValidationErrors["IpAddress"] = new List<string>() { "IP address is required" };
                RaiseErrorsChanged("IpAddress");
            }
            else if (!_ipRegex.IsMatch(IpAddress))
            {
                ValidationErrors["IpAddress"] = new List<string>() { "IP address is not valid}" };
                RaiseErrorsChanged("IpAddress");
            }
            else if (ValidationErrors.ContainsKey("IpAddress"))
            {
                ValidationErrors.Remove("IpAddress");
                RaiseErrorsChanged("IpAddress");
            }
        }

        private void ValidateMacAddress()
        {
            if (string.IsNullOrEmpty(MacAddress))
            {
                ValidationErrors["MacAddress"] = new List<string>() { "MAC address is required" };
                RaiseErrorsChanged("MacAddress");
            }
            else if (!_macRegex.IsMatch(MacAddress))
            {
                ValidationErrors["MacAddress"] = new List<string>() { "MAC address is not valid" };
                RaiseErrorsChanged("MacAddress");
            }
            else if (ValidationErrors.ContainsKey("MacAddress"))
            {
                ValidationErrors.Remove("MacAddress");
                RaiseErrorsChanged("MacAddress");
            }
        }

        #endregion
    }
}
