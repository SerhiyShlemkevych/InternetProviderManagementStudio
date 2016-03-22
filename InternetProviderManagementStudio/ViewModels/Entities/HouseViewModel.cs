using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ipms.UI.ViewModels.Entities
{
    public class ConnectedHouseViewModel : EntityViewModel
    {
        private Regex _houseRegex = new Regex(@"^([0-9]{1,})([a-zA-F]{0,})$");

        private int _id;
        private string _city;
        private string _street;
        private string _house;

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
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                ValidateCity();
                RaisePropertyChanged("City");
            }
        }
        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                ValidateStreet();
                RaisePropertyChanged("Street");
            }
        }
        public string House
        {
            get
            {
                return _house;
            }
            set
            {
                _house = value;
                ValidateHouse();
                RaisePropertyChanged("House");
            }
        }

        #region Validation

        public override void Validate()
        {
            ValidateCity();
            ValidateStreet();
            ValidateHouse();
        }

        private void ValidateCity()
        {
            if (string.IsNullOrEmpty(City))
            {
                ValidationErrors["City"] = new List<string>() { "City is required" };
                RaiseErrorsChanged("City");
            }
            else if (City.Length > 128)
            {
                ValidationErrors["City"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("City");
            }
            else if (ValidationErrors.ContainsKey("City"))
            {
                ValidationErrors.Remove("City");
                RaiseErrorsChanged("City");
            }
        }

        private void ValidateStreet()
        {
            if (string.IsNullOrEmpty(Street))
            {
                ValidationErrors["Street"] = new List<string>() { "Street is required" };
                RaiseErrorsChanged("Street");
            }
            else if (Street.Length > 128)
            {
                ValidationErrors["Street"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("Street");
            }
            else if (ValidationErrors.ContainsKey("Street"))
            {
                ValidationErrors.Remove("Street");
                RaiseErrorsChanged("Street");
            }
        }

        private void ValidateHouse()
        {
            if (string.IsNullOrEmpty(House))
            {
                ValidationErrors["House"] = new List<string>() { "House is required" };
                RaiseErrorsChanged("House");
            }
            else if (House.Length > 128)
            {
                ValidationErrors["House"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("House");
            }
            else if (!_houseRegex.IsMatch(House))
            {
                ValidationErrors["House"] = new List<string>() { "House has to be in format {number} or {number}{letter}" };
                RaiseErrorsChanged("House");
            }
            else if (ValidationErrors.ContainsKey("House"))
            {
                ValidationErrors.Remove("House");
                RaiseErrorsChanged("House");
            }
        }

        #endregion
    }
}
