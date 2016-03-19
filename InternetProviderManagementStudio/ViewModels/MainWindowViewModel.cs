using GalaSoft.MvvmLight.CommandWpf;
using InternetProviderManagementStudio.Views.Main;
using InternetProviderManagementStudio.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    class MainWindowViewModel : ParentViewModel
    {
        private MainWindow _window;

        private TariffAreaViewModel _tariffViewModel;
        private CustomerAreaViewModel _cusomerViewModel;
        private ConnectedHouseAreaViewModel _houseViewModel;


        private RelayCommand<ChildViewModel> _changeViewModelCommand;

        public MainWindowViewModel()
        {
            InitializeCommands();

            _tariffViewModel = new TariffAreaViewModel(this);
            _houseViewModel = new ConnectedHouseAreaViewModel(this);
            _cusomerViewModel = new CustomerAreaViewModel(this);

            _window = new MainWindow() { DataContext = this };
            _window.Show();
        }

        public ChildViewModel TariffViewModel
        {
            get
            {
                return _tariffViewModel;
            }
        }
        public ChildViewModel HouseViewModel
        {
            get
            {
                return _houseViewModel;
            }
        }
        public ChildViewModel CustomerViewModel
        {
            get
            {
                return _cusomerViewModel;
            }
        }

        public RelayCommand<ChildViewModel> ChangeViewModelCommand
        {
            get
            {
                return _changeViewModelCommand;
            }
        }

        private void ChangeViewModel(ChildViewModel viewModel)
        {
            ViewPage = viewModel.ViewPage;
            ActionButtons = viewModel.ActionButtons;
        }

        private void InitializeCommands()
        {
            _changeViewModelCommand = new RelayCommand<ChildViewModel>(ChangeViewModel);
        }
    }
}
