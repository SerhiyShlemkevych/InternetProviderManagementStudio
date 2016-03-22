using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.UI.ViewModels.Entities
{
    class AdminisratorViewModel : ViewModel
    {
        private int _id;
        private string _login;
        private string _forename;
        private string _surname;

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

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
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
    }
}
