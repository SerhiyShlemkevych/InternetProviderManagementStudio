﻿using InternetProviderManagementStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace InternetProviderManagementStudio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel _mainWindowViewModel;
        private AuthenticationViewModel _authenticationViewModel;

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            _authenticationViewModel = new AuthenticationViewModel();
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}\nInner exception message: {1}", e.Exception.Message, e.Exception.InnerException == null ? "" : e.Exception.InnerException.Message );
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occurred: {0}\nInner exception message: {1}", e.Exception.Message, e.Exception.InnerException == null ? "" : e.Exception.InnerException.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        public void RunMain()
        {
            _authenticationViewModel = null;
            _mainWindowViewModel = new MainWindowViewModel();
        }
    }
}
