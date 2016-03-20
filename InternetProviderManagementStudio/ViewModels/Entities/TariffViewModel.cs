using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.ViewModels.Entities
{
    class TariffViewModel : EntityViewModel
    {
        private int _id;
        private string _name;
        private decimal _price;
        private int _dowloadSpeed;
        private int _uploadSpeed;
        private bool _IsArchive;

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
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                ValidateName();
                RaisePropertyChanged("Name");
            }
        }
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                ValidatePrice();
                RaisePropertyChanged("Price");
            }
        }
        public int DownloadSpeed
        {
            get
            {
                return _dowloadSpeed;
            }
            set
            {
                _dowloadSpeed = value;
                ValidateDownloadSpeed();
                RaisePropertyChanged("DownloadSpeed");
            }
        }
        public int UploadSpeed
        {
            get
            {
                return _uploadSpeed;
            }
            set
            {
                _uploadSpeed = value;
                ValidateUploadSpeed();
                RaisePropertyChanged("UploadSpeed");
            }
        }
        public bool IsArchive
        {
            get
            {
                return _IsArchive;
            }
            set
            {
                _IsArchive = value;
                RaisePropertyChanged("IsArchive");
            }
        }

        public override void Validate()
        {
            ValidateName();
            ValidatePrice();
            ValidateUploadSpeed();
            ValidateDownloadSpeed();
        }

        private void ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                ValidationErrors["Name"] = new List<string>() { "Name is required" };
                RaiseErrorsChanged("Name");
            }
            else if (Name.Length > 128)
            {
                ValidationErrors["Name"] = new List<string>() { "Max length is limited (128)" };
                RaiseErrorsChanged("Name");
            }            
            else if (ValidationErrors.ContainsKey("Name"))
            {
                ValidationErrors.Remove("Name");
                RaiseErrorsChanged("Name");
            }
        }

        private void ValidatePrice()
        {
            if (Price <= 0m)
            {
                ValidationErrors["Price"] = new List<string>() { "Price has to be grater then 0" };
                RaiseErrorsChanged("Price");
            }
            else if (ValidationErrors.ContainsKey("Price"))
            {
                ValidationErrors.Remove("Price");
                RaiseErrorsChanged("Price");
            }
        }

        private void ValidateDownloadSpeed()
        {
            if (DownloadSpeed <= 0)
            {
                ValidationErrors["DownloadSpeed"] = new List<string>() { "Download speed has to be grater then 0" };
                RaiseErrorsChanged("DownloadSpeed");
            }
            else if (ValidationErrors.ContainsKey("DownloadSpeed"))
            {
                ValidationErrors.Remove("DownloadSpeed");
                RaiseErrorsChanged("DownloadSpeed");
            }
        }

        private void ValidateUploadSpeed()
        {
            if (UploadSpeed <= 0)
            {
                ValidationErrors["UploadSpeed"] = new List<string>() { "Upload speed has to be grater then 0" };
                RaiseErrorsChanged("UploadSpeed");
            }
            else if (ValidationErrors.ContainsKey("UploadSpeed"))
            {
                ValidationErrors.Remove("UploadSpeed");
                RaiseErrorsChanged("UploadSpeed");
            }

        }

    }
}
