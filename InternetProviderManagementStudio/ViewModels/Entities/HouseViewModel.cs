using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.ViewModels.Entities
{
    public class ConnectedHouseViewModel : ViewModel
    {
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
                RaisePropertyChanged("House");
            }
        }
    }
}
