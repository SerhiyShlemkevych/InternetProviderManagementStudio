using System.Collections.Generic;
using System.Windows.Controls;

namespace Ipms.UI.ViewModels
{
    abstract class ParentViewModel : ViewModel
    {
        
        private Page _viewPage;
        private IEnumerable<Button> _actionButtons;
        private Page _customPage;

        #region Properties

        public Page ViewPage
        {
            get
            {
                return _viewPage;
            }
            set
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

        public IEnumerable<Button> ActionButtons
        {
            get
            {
                return _actionButtons;
            }
            set
            {
                _actionButtons = value;
                RaisePropertyChanged("ActionButtons");
            }
        }

        #endregion

    }
}
