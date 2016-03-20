using GalaSoft.MvvmLight.CommandWpf;
using InternetProviderManagementStudio.Views.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    abstract class EntityViewModel<T> : ChildViewModel
    {
        private string _selectedSearchColumn;

        private T _newItem;
        private T _selectedItem;

        public EntityViewModel(ParentViewModel parentViewModel) : base(parentViewModel)
        {
            CloseCustomPageCommand = new RelayCommand(CloseCustomPage);
            Items = new ObservableCollection<T>();
            SearchColumns = new List<string>();
            InitializeSearchColumns();
            InitializeRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            InitializeCustomPages();
            InitializeCommands();
            InitializeActionButtons();
            InitializeViewPage();
        }

        public string SelectedSearchColumn
        {
            get
            {
                return _selectedSearchColumn;
            }
            set
            {
                _selectedSearchColumn = value;
                RaisePropertyChanged("SelectedSearchColumn");
            }
        }

        public List<string> SearchColumns
        {
            get;
            private set;
        }

        public T NewItem
        {
            get
            {
                return _newItem;
            }
            set
            {
                _newItem = value;
                RaisePropertyChanged("NewItem");
            }
        }

        public T SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public ObservableCollection<T> Items
        {
            get;
            private set;
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

        protected abstract void InitializeCommands();
        protected abstract void InitializeViewPage();
        protected abstract void InitializeActionButtons();
        protected abstract void InitializeCustomPages();
        protected abstract void InitializeSearchColumns();
        protected abstract void InitializeRepository(string connectionString);
    }
}
