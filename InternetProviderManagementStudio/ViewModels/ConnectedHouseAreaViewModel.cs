using Ipms.UI.ViewModels.Entities;
using Ipms.UI.Views.House;
using Ipms.Repositories;
using Ipms.Repositories.Sql;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Ipms.Models;
using Ipms.UI.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Data;

namespace Ipms.UI.ViewModels
{
    class ConnectedHouseAreaViewModel : EntityAreaViewModel<ConnectedHouseViewModel>
    {
        IConnectedHouseRepository _repository;

        public ConnectedHouseAreaViewModel(MainViewModel parentViewModel)
            : base(parentViewModel)
        {
            PropertyChanged += ConnectedHouseAreaViewModel_PropertyChanged;

            ConnectedHouses = new ObservableCollection<ConnectedHouseViewModel>();
        }

        #region Properties

        public ObservableCollection<ConnectedHouseViewModel> ConnectedHouses
        {
            get;
            private set;
        }

        public CreateConnectedHousePage CreateConnectedHousePage
        {
            get;
            private set;
        }

        public EditConnectedHousePage EditConnectedHousePage
        {
            get;
            private set;
        }


        #region Commands

        public RelayCommand EndCreateConnectedHouseCommand
        {
            get;
            private set;
        }

        public RelayCommand EditConnectedHouseCommand
        {
            get;
            private set;
        }

        public RelayCommand BeginCreateConnectedHouseCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowEditPageCommand
        {
            get;
            private set;
        }

        public RelayCommand RefreshCommand
        {
            get;
            private set;
        }
        #endregion

        #endregion

        #region Private functions

        #region Commands

        private void EndCreateConnectedHouse()
        {
            NewItem.Validate();
            if (NewItem.HasErrors)
            {
                return;
            }

            ConnectedHouseModel model = Mapper.Map<ConnectedHouseModel>(NewItem);
            NewItem.Id = _repository.Insert(model, Administrator.Current.Id);
            ConnectedHouses.Add(NewItem);
            NewItem = null;
            CloseCustomPageCommand.Execute(null);
            Search();
        }

        private void EditConnectedHouse()
        {
            SelectedItem.Validate();
            if(SelectedItem.HasErrors)
            {
                return;
            }

            ConnectedHouseModel model = Mapper.Map<ConnectedHouseModel>(SelectedItem);
            _repository.Update(model, Administrator.Current.Id);
            CloseCustomPageCommand.Execute(null);
            Search();
        }

        private void BeginCreateConnectedHouse()
        {
            NewItem = new ConnectedHouseViewModel();
            Parent.CustomPage = CreateConnectedHousePage;
        }

        private void ShowEditPage()
        {
            Parent.CustomPage = EditConnectedHousePage;
        }

        private bool ShowEditPageCanExecute()
        {
            return SelectedItem != null;
        }

        #endregion

        private void ConnectedHouseAreaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedItem")
            {
                ShowEditPageCommand.RaiseCanExecuteChanged();
            }
        }

        #region Search

        private IEnumerable<ConnectedHouseViewModel> SearchById()
        {
            IEnumerable<ConnectedHouseViewModel> result = null;
            int id;
            if (int.TryParse(SearchString, out id))
            {
                result = ConnectedHouses.Where(h => h.Id == id);
            }

            return result;
        }

        private IEnumerable<ConnectedHouseViewModel> SearchByCity()
        {
            return ConnectedHouses.Where(h => h.City.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<ConnectedHouseViewModel> SearchByStreet()
        {
            return ConnectedHouses.Where(h => h.Street.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<ConnectedHouseViewModel> SearchByHouse()
        {
            return ConnectedHouses.Where(h => h.House.ToLower().Contains(SearchString.ToLower()));
        }

        #endregion

        #endregion

        #region Overrides

        public override void Refresh()
        {
            CloseCustomPageCommand.Execute(null);

            ConnectedHouses.Clear();
            foreach (var model in _repository.GetAll())
            {
                ConnectedHouses.Add(Mapper.Map<ConnectedHouseViewModel>(model));
            }

            Search();
        }

        protected override void InitializeSearchColumns()
        {
            SearchColumns.AddRange(new List<string>() { "ID", "City", "Street", "House" });
        }

        protected override void InitializeRepository(string connectionString)
        {
            _repository = new SqlConnectedHouseRepository(connectionString);
        }

        protected override void InitializeCommands()
        {
            BeginCreateConnectedHouseCommand = new RelayCommand(BeginCreateConnectedHouse);
            ShowEditPageCommand = new RelayCommand(ShowEditPage, ShowEditPageCanExecute);
            EndCreateConnectedHouseCommand = new RelayCommand(EndCreateConnectedHouse);
            EditConnectedHouseCommand = new RelayCommand(EditConnectedHouse);
            RefreshCommand = new RelayCommand(Refresh);
        }

        protected override void InitializeActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create home",
                Command = BeginCreateConnectedHouseCommand
            });
            ActionButtons.Add(new Button()
            {
                Content = "Edit home",
                Command = ShowEditPageCommand
            });
        }

        protected override void InitializeViewPage()
        {
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("Id"),
                Header = "ID"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("City"),
                Header = "City"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("Street"),
                Header = "Street"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("House"),
                Header = "House"
            });
        }

        protected override void InitializeCustomPages()
        {
            CreateConnectedHousePage = new CreateConnectedHousePage() { DataContext = this };
            EditConnectedHousePage = new EditConnectedHousePage() { DataContext = this };
        }

        public override void Search()
        {
            CloseCustomPageCommand.Execute(null);
            Items.Clear();

            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrEmpty(SelectedSearchColumn))
            {
                foreach (var item in ConnectedHouses)
                {
                    Items.Add(item);
                }
                return;
            }

            IEnumerable<ConnectedHouseViewModel> result = null;

            switch (SelectedSearchColumn)
            {
                case "ID":
                    {
                        result = SearchById();
                        break;
                    }
                case "City":
                    {
                        result = SearchByCity();
                        break;
                    }
                case "Street":
                    {
                        result = SearchByStreet();
                        break;
                    }
                case "House":
                    {
                        result = SearchByHouse();
                        break;
                    }
            }

            if (result == null)
            {
                return;
            }

            foreach (var item in result)
            {
                Items.Add(item);
            }
        }

        #endregion
    }
}
