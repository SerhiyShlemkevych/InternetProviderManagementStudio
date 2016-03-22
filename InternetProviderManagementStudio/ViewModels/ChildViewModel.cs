using Ipms.UI.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public abstract void Refresh();
    }
}
