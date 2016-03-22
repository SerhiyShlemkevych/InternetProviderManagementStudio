using GalaSoft.MvvmLight.CommandWpf;
using Ipms.UI.Views.Main;

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

        #region Properties

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

        #region Commands

        public RelayCommand<ChildViewModel> ChangeViewModelCommand
        {
            get
            {
                return _changeViewModelCommand;
            }
        }

        #endregion

        #endregion

        #region Private functions

        #region Commands

        private void ChangeViewModel(ChildViewModel viewModel)
        {
            viewModel.Refresh();

            ViewPage = viewModel.ViewPage;
            ActionButtons = viewModel.ActionButtons;
        }

        #endregion

        private void InitializeCommands()
        {
            _changeViewModelCommand = new RelayCommand<ChildViewModel>(ChangeViewModel);
        }

        #endregion
    }
}
