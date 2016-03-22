using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Specialized;

namespace Ipms.UI.Views.Shared
{
    /// <summary>
    /// Interaction logic for ViewPage.xaml
    /// </summary>
    public partial class ViewPage : Page
    {
        public ViewPage()
        {
            InitializeComponent();
            DataGridColumns = new ObservableCollection<DataGridColumn>();
            DataGridColumns.CollectionChanged += DataGridColumns_CollectionChanged;
        }

        public ObservableCollection<DataGridColumn> DataGridColumns
        {
            get;
            private set;
        }

        private void DataGridColumns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    {
                        dataGrid.Columns.Add((DataGridColumn)e.NewItems[0]);
                        break;
                    }
                case NotifyCollectionChangedAction.Remove:
                    {
                        dataGrid.Columns.Remove((DataGridColumn)e.OldItems[0]);
                        break;
                    }
            }
        }
    }
}
