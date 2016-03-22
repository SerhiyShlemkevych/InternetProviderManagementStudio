using Ipms.UI.Views.Shared;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Ipms.UI.ViewModels
{
    abstract class ChildViewModel : ViewModel
    {

        public ChildViewModel(ParentViewModel parentViewModel)
        {
            Parent = parentViewModel;
            ActionButtons = new List<Button>();
            ViewPage = new ViewPage() { DataContext = this };
        }

        #region Properties

        public ParentViewModel Parent
        {
            get;
            private set;
        }

        public ViewPage ViewPage
        {
            get;
            private set;
        }

        public List<Button> ActionButtons
        {
            get;
            private set;
        }

        #endregion

        public abstract void Refresh();
    }
}
