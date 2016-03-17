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
        private ParentViewModel _parentViewModel;
        private Page _viewPage;
        private List<Button> _actionButtons;
        private Page _customPage;

        public ChildViewModel(ParentViewModel parentViewModel, Page viewPage)
        {
            _parentViewModel = parentViewModel;
            ActionButtons = new List<Button>();
            ViewPage = viewPage;
            viewPage.DataContext = this;
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

        public Page CustomPage
        {
            get
            {
                return _customPage;
            }
            set
            {
                _customPage = value;
                RaisePropertyChanged("CustomPage");
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
    }
}
