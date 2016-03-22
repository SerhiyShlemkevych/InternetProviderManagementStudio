using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace Ipms.UI.ViewModels
{
    abstract class EntityAreaViewModel<T> : ChildViewModel
    {
        private string _selectedSearchColumn;
        private string _searchString;

        private T _newItem;
        private T _selectedItem;

        public EntityAreaViewModel(ParentViewModel parentViewModel) : base(parentViewModel)
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

        #region Properties

        public string SelectedSearchColumn
        {
            get
            {
                return _selectedSearchColumn;
            }
            set
            {
                _selectedSearchColumn = value;
                Search();
                RaisePropertyChanged("SelectedSearchColumn");
            }
        }

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                Search();
                RaisePropertyChanged("SearchString");
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

        #endregion

        #region Private functions

        private void CloseCustomPage()
        {
            Parent.CustomPage = null;
        }

        #endregion


        

        public abstract void Search();       

        protected abstract void InitializeCommands();
        protected abstract void InitializeViewPage();
        protected abstract void InitializeActionButtons();
        protected abstract void InitializeCustomPages();
        protected abstract void InitializeSearchColumns();
        protected abstract void InitializeRepository(string connectionString);
    }
}
