using GalaSoft.MvvmLight.CommandWpf;
using Ipms.UI.Models;
using Ipms.UI.Views.Authentication;
using Ipms.Repositories;
using Ipms.Repositories.Sql;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace Ipms.UI.ViewModels
{
    class AuthenticationViewModel : ViewModel
    {
        private string _login;
        private AuthenticationWindow _window;
        private IAdministratorRepository _repository;

        public AuthenticationViewModel()
        {
            _repository = new SqlAdministratorRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            InitializeCommands();
            _window = new AuthenticationWindow() { DataContext = this };
            _window.Show();
        }

        #region Properties

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

        #region Commands

        public RelayCommand<PasswordBox> SignInCommand
        {
            get;
            private set;
        }

        public RelayCommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region Private functions

        #region Commands
        private void SignIn(PasswordBox passwordBox)
        {
            var administrator = _repository.Authenticate(Login, passwordBox.Password);
            if(administrator == null)
            {
                MessageBox.Show("Invalid login or password", "Authnetication error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Administrator.Current.Id = administrator.Id;
            Administrator.Current.Login = administrator.Login;
            Administrator.Current.Surname = administrator.Surname;
            Administrator.Current.Forename = administrator.Forename;
                        
            ((App)App.Current).RunMain();
            _window.Close();
        }
        private void Cancel()
        {
            _window.Close();
        }

        #endregion

        private void InitializeCommands()
        {
            CancelCommand = new RelayCommand(Cancel);
            SignInCommand = new RelayCommand<PasswordBox>(SignIn);
        }

        #endregion
    }
}
