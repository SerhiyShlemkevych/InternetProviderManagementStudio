using GalaSoft.MvvmLight.CommandWpf;
using InternetProviderManagementStudio.Models;
using InternetProviderManagementStudio.Views.Authentication;
using IPMS.Repositories;
using IPMS.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    class AuthenticationViewModel : EntityViewModel
    {
        private string _login;
        private AuthenticationWindow _window;
        private IAdministratorRepository _repository;

        public AuthenticationViewModel()
        {
            _repository = new SqlAdministratorRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            InitializeCommands();
            _window = new AuthenticationWindow() { DataContext = this };
            _window.passwordBox.Password = "111111";
            Login = "ihor1970";
            _window.Show();
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

        private void SignIn(PasswordBox passwordBox)
        {
            string password = Encoder.Encode(passwordBox.Password);
            var administrator = _repository.Authenticate(Login, password);
            if(administrator == null)
            {
                MessageBox.Show("Invalid login or password", "Authnetication error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Administartor.Current.Id = administrator.Id;
            Administartor.Current.Login = administrator.Login;
            Administartor.Current.Surname = administrator.Surname;
            Administartor.Current.Forename = administrator.Forename;
                        
            ((App)App.Current).RunMain();
            _window.Close();
        }
        private void Cancel()
        {
            _window.Close();
        }

        private void InitializeCommands()
        {
            CancelCommand = new RelayCommand(Cancel);
            SignInCommand = new RelayCommand<PasswordBox>(SignIn);
        }
    }
}
