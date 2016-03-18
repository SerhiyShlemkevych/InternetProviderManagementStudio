using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetProviderManagementStudio.ViewModels.Entities
{
    class TariffViewModel : ViewModel
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

        public override void RaiseAllpropertiesChanged()
        {
            RaisePropertyChanged("Id");
            RaisePropertyChanged("Price");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("UploadSpeed");
            RaisePropertyChanged("DownloadSpeed");
            RaisePropertyChanged("IsArchive");
        }
    }
}
