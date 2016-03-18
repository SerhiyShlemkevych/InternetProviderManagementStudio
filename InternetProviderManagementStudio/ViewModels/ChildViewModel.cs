using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    class ChildViewModel : ViewModel
    {
        private ParentViewModel _parent;
        private Page _viewPage;
        private List<Button> _actionButtons;

        public ChildViewModel(ParentViewModel parentViewModel, Page viewPage)
        {
            _parent = parentViewModel;
            ActionButtons = new List<Button>();
            ViewPage = viewPage;
            viewPage.DataContext = this;
            CloseCustomPageCommand = new RelayCommand(CloseCustomPage);
        }

        public ParentViewModel Parent
        {
            get
            {
                return _parent;
            }
        }

        public Page ViewPage
        {
            get
            {
                return _viewPage;
            }
            private set
            {
                _viewPage = value;
                RaisePropertyChanged("ViewPage");
            }
        }

        public List<Button> ActionButtons
        {
            get
            {
                return _actionButtons;
            }
            private set
            {
                _actionButtons = value;
                RaisePropertyChanged("ActionButtons");
            }
        }

        public RelayCommand CloseCustomPageCommand
        {
            get;
            private set;
        }

        private void CloseCustomPage()
        {
            Parent.CustomPage = null;
        }
    }
}
