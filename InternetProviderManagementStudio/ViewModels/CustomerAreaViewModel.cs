using GalaSoft.MvvmLight.CommandWpf;
using InternetProviderManagementStudio.Models;
using InternetProviderManagementStudio.ViewModels.Entities;
using InternetProviderManagementStudio.Views.Customer;
using InternetProviderManagementStudio.Views.House;
using InternetProviderManagementStudio.Views.Shared;
using InternetProviderManagementStudio.Views.Tariff;
using IPMS.Models;
using IPMS.Repositories;
using IPMS.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InternetProviderManagementStudio.ViewModels
{
    class CustomerAreaViewModel : EntityViewModel<CustomerViewModel>
    {
        private ICustomerRepository _customerRepository;
        private IConnectedHouseRepository _connectedHouseRepository;
        private ITariffRepository _tariffRepository;

        public CustomerAreaViewModel(MainViewModel parentViewModel)
            : base(parentViewModel)
        {
            Tariffs = new ObservableCollection<TariffViewModel>();
            Houses = new ObservableCollection<ConnectedHouseViewModel>();

            CustomerStates = new List<CustomerState>() { CustomerState.Active, CustomerState.Suspended };

            Refresh();
        }

        #region Properties

        #region Custom pages
        public ShowTariffPage ShowTariffPage
        {
            get;
            private set;
        }

        public ShowConnectedHousePage HousePage
        {
            get;
            private set;
        }

        public CreateCustomerPage CreateCustomerPage
        {
            get;
            private set;
        }

        public MoveCustomerPage MoveCustomerPage
        {
            get;
            private set;
        }

        public ChangeCustomersTariffPage ChangeCustomersTariffPage
        {
            get;
            private set;
        }

        public EditCustomerPage EditCustomerPage
        {
            get;
            private set;
        }

        #endregion

        public List<CustomerState> CustomerStates
        {
            get;
            private set;
        }

        public ObservableCollection<TariffViewModel> Tariffs
        {
            get;
            private set;
        }

        public ObservableCollection<ConnectedHouseViewModel> Houses
        {
            get;
            private set;
        }

        #region Commands

        public RelayCommand<Page> SetCustomPageCommand
        {
            get;
            private set;
        }

        public RelayCommand EndCreateCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand EditCustomerCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowHouseCommand
        {
            get;
            private set;
        }

        public RelayCommand GetChargeCommand
        {
            get;
            private set;
        }

        public RelayCommand RefreshCommand
        {
            get;
            private set;
        }

        public RelayCommand BeginCreateCustomerCommand
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region Private functions

        #region Commands
        private void Refresh()
        {
            CloseCustomPageCommand.Execute(null);

            Tariffs.Clear();
            Items.Clear();
            Houses.Clear();

            foreach (var model in _tariffRepository.GetAll())
            {
                if(model.IsArchive)
                {
                    continue;
                }

                Tariffs.Add(Mapper.Map<TariffViewModel>(model));
            }
            foreach (var model in _connectedHouseRepository.GetAll())
            {
                Houses.Add(Mapper.Map<ConnectedHouseViewModel>(model));
            }
            foreach (var model in _customerRepository.GetAll())
            {
                Items.Add(Mapper.Map<CustomerModel, CustomerViewModel>(model, (m, v) =>
                {
                    v.Tariff = Tariffs.Where(t => t.Id == m.TariffId).First();
                    v.House = Houses.Where(h => h.Id == m.HouseId).First();
                }));
            }
        }

        private void EndCreateCustomer()
        {
            CustomerModel model = Mapper.Map<CustomerViewModel, CustomerModel>(NewItem, (v, m) =>
            {
                m.HouseId = v.House.Id;
                m.TariffId = v.Tariff.Id;
            });
            NewItem.Id = _customerRepository.Insert(model, Administartor.Current.Id);
            Items.Add(NewItem);
            NewItem = null;
            CloseCustomPageCommand.Execute(null);
        }

        private void EditCustomer()
        {
            CustomerModel model = Mapper.Map<CustomerViewModel, CustomerModel>(SelectedItem, (v, m) =>
            {
                m.HouseId = v.House.Id;
                m.TariffId = v.Tariff.Id;
            });
            _customerRepository.Update(model, Administartor.Current.Id);
            CloseCustomPageCommand.Execute(null);
        }

        private void GetCharge()
        {
            CloseCustomPageCommand.Execute(null);
            _customerRepository.GetCharge(Administartor.Current.Id);
        }

        private void ShowTariff()
        {
            SelectedTariff = SelectedItem.Tariff;
            SetCustomPage(ShowTariffPage);
        }

        private void ShowHouse()
        {
            SelectedHouse = SelectedItem.House;
            SetCustomPage(HousePage);
        }

        private void BeginCreateCustomer()
        {
            NewItem = new CustomerViewModel() { State = CustomerState.Active };
            SetCustomPage(CreateCustomerPage);
        }

        private void SetCustomPage(Page page)
        {
            Parent.CustomPage = page;
        }

        private bool SetCustomPageCanExecute(Page page)
        {
            return SelectedItem != null;
        }

        private bool SetCustomPageCanExecute()
        {
            return SelectedItem != null;
        }
        #endregion
        private DataGridTemplateColumn CreateButtonColumn(RelayCommand command, string header)
        {
            DataGridTemplateColumn column = new DataGridTemplateColumn() { Header = header };
            column.CellTemplate = new DataTemplate();

            FrameworkElementFactory buttonFactory = new FrameworkElementFactory(typeof(Button));
            buttonFactory.SetValue(Button.CommandProperty, command);
            buttonFactory.SetValue(Button.ContentProperty, "View");

            column.CellTemplate.VisualTree = buttonFactory;

            return column;
        }
        #endregion

        #region Overrrides
        protected override void InitializeActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create customer",
                Command = ShowCreatePageCommand,
                Margin = new Thickness(0, 5, 0 ,5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Move customer",
                Command = SetCustomPageCommand,
                CommandParameter = MoveCustomerPage,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Change customer`s tariff plan",
                Command = SetCustomPageCommand,
                CommandParameter = ChangeCustomersTariffPage,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Edit customer",
                Command = SetCustomPageCommand,
                CommandParameter = EditCustomerPage,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Get charge from customers",
                Command = GetChargeCommand,
                Margin = new Thickness(0, 5, 0, 5)
            });
        }

        protected override void InitializeCommands()
        {
            BeginCreateCustomerCommand = new RelayCommand(BeginCreateCustomer);
            EndCreateCustomerCommand = new RelayCommand(EndCreateCustomer);
            SetCustomPageCommand = new RelayCommand<Page>(SetCustomPage, SetCustomPageCanExecute);
            ShowTariffCommand = new RelayCommand(ShowTariff, SetCustomPageCanExecute);
            ShowHouseCommand = new RelayCommand(ShowHouse, SetCustomPageCanExecute);
            GetChargeCommand = new RelayCommand(GetCharge);
            RefreshCommand = new RelayCommand(Refresh);
            EditCustomerCommand = new RelayCommand(EditCustomer);
        }

        protected override void InitializeCustomPages()
        {
            ShowTariffPage = new ShowTariffPage() { DataContext = this };
            HousePage = new ShowConnectedHousePage() { DataContext = this };
            EditCustomerPage = new EditCustomerPage() { DataContext = this };
            MoveCustomerPage = new MoveCustomerPage() { DataContext = this };
            ChangeCustomersTariffPage = new ChangeCustomersTariffPage() { DataContext = this };
            CreateCustomerPage = new CreateCustomerPage() { DataContext = this };
        }

        protected override void InitializeRepository(string connectionString)
        {
            _customerRepository = new SqlCustomerRepository(connectionString);
            _connectedHouseRepository = new SqlConnectedHouseRepository(connectionString);
            _tariffRepository = new SqlTariffRepository(connectionString);
        }

        protected override void InitializeSearchColumns()
        {
            SearchColumns.AddRange(new List<string>() { "Id", "Forename", "Surname", "Tariff name", "City", "Street", "House", "Flat", "Balance", "IP address", "MAC address", "Last charged date" });
        }

        protected override void InitializeViewPage()
        {
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Id",
                Binding = new Binding("Id")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Forename",
                Binding = new Binding("Forename")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Surname",
                Binding = new Binding("Surname")
            });
            ViewPage.DataGridColumns.Add(CreateButtonColumn(ShowTariffCommand, "Tariff"));
            ViewPage.DataGridColumns.Add(CreateButtonColumn(ShowHouseCommand, "House"));
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Flat",
                Binding = new Binding("Flat")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "IP address",
                Binding = new Binding("IpAddress")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "MAC address",
                Binding = new Binding("MacAddress")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "State",
                Binding = new Binding("State")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Balance",
                Binding = new Binding("Balance")
            });
        }

        #endregion
    }
}
