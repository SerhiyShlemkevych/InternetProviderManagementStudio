using GalaSoft.MvvmLight.CommandWpf;
using Ipms.UI.Views.Main;
using Ipms.UI.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Ipms.UI.ViewModels
{
    class MainViewModel : ParentViewModel
    {
        private MainWindow _window;

        private RelayCommand<ChildViewModel> _changeViewModelCommand;

        public MainViewModel()
        {
            InitializeCommands();

            TariffArea = new TariffAreaViewModel(this);
            ConnectedHouseArea = new ConnectedHouseAreaViewModel(this);
            CustomerArea = new CustomerAreaViewModel(this);
            ActionLogArea = new ActionLogAreaViewModel(this);

            _window = new MainWindow() { DataContext = this };
            _window.Show();

            ChangeViewModel(CustomerArea);
        }

        public ChildViewModel TariffArea
        {
            get;
            private set;
        }
        public ChildViewModel ConnectedHouseArea
        {
            get;
            private set;
        }
        public ChildViewModel CustomerArea
        {
            get;
            private set;
        }

        public ChildViewModel ActionLogArea
        {
            get;
            private set;
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
            viewModel.Refresh();

            ViewPage = viewModel.ViewPage;
            ActionButtons = viewModel.ActionButtons;
        }

        private void InitializeCommands()
        {
            _changeViewModelCommand = new RelayCommand<ChildViewModel>(ChangeViewModel);
        }
    }
}
