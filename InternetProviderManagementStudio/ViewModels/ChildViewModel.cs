using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    class ChildViewModel<TParentViewModel> : ViewModel
    {
        private TParentViewModel _parentViewModel;
        private Page _viewPage;
        private IEnumerable<Button> _actionButtons;
        private Page _customPage;

        public ChildViewModel(TParentViewModel parentViewModel, Page viewPage, Page customPage)
        {
            _parentViewModel = parentViewModel;
            ActionButtons = new List<Button>();
            ViewPage = viewPage;
            CustomPage = customPage;
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
            private set
            {
                _customPage = value;
                RaisePropertyChanged("CustomPage");
            }
        }

        public IEnumerable<Button> ActionButtons
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
