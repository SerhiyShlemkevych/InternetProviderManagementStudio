using GalaSoft.MvvmLight.CommandWpf;
using Ipms.UI.Models;
using Ipms.UI.ViewModels.Entities;
using Ipms.UI.Views.Customer;
using Ipms.UI.Views.House;
using Ipms.UI.Views.Shared;
using Ipms.UI.Views.Tariff;
using Ipms.Models;
using Ipms.Repositories;
using Ipms.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ipms.UI.ViewModels
{
    class CustomerAreaViewModel : EntityAreaViewModel<CustomerViewModel>
    {
        private ICustomerRepository _customerRepository;
        private IConnectedHouseRepository _connectedHouseRepository;
        private ITariffRepository _tariffRepository;

        private BalanceLogItemViewModel _funds;

        public CustomerAreaViewModel(MainViewModel parentViewModel)
            : base(parentViewModel)
        {
            Tariffs = new ObservableCollection<TariffViewModel>();
            Houses = new ObservableCollection<ConnectedHouseViewModel>();
            Customers = new ObservableCollection<CustomerViewModel>();

            CustomerStates = new List<CustomerState>() { CustomerState.Active, CustomerState.Suspended };
        }

        #region Properties

        #region Custom pages

        public AddFundsPage AddFundsPage
        {
            get;
            private set;
        }

        public ShowCustomersBalanceLogPage ShowCustomersBalanceLogPage
        {
            get;
            private set;
        }

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

        public BalanceLogItemViewModel Funds
        {
            get
            {
                return _funds;
            }
            set
            {
                _funds = value;
                RaisePropertyChanged("Funds");
            }
        }

        public List<CustomerState> CustomerStates
        {
            get;
            private set;
        }

        public ObservableCollection<CustomerViewModel> Customers
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

        public RelayCommand BeginAddFundsCommand
        {
            get;
            private set;
        }

        public RelayCommand EndAddFundsCommand
        {
            get;
            private set;
        }

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

        #region Search

        private IEnumerable<CustomerViewModel> SearchById()
        {
            IEnumerable<CustomerViewModel> result = null;
            int id;
            if (int.TryParse(SearchString, out id))
            {
                result = Customers.Where(c => c.Id == id);
            }

            return result;
        }

        private IEnumerable<CustomerViewModel> SearchByForename()
        {
            return Customers.Where(c => c.Forename.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchBySurname()
        {
            return Customers.Where(c => c.Surname.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByCity()
        {
            return Customers.Where(c => c.House.City.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByStreet()
        {
            return Customers.Where(c => c.House.Street.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByHouse()
        {
            return Customers.Where(c => c.House.House.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByFlat()
        {
            return Customers.Where(c => c.Flat.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByTariff()
        {
            return Customers.Where(c => c.Tariff.Name.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByIpAddress()
        {
            return Customers.Where(c => c.IpAddress.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByMacAddress()
        {
            return Customers.Where(c => c.MacAddress.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByState()
        {
            return Customers.Where(c => c.State.ToString().ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<CustomerViewModel> SearchByLastChargedDate()
        {
            IEnumerable<CustomerViewModel> result = null;
            DateTime date;
            if (DateTime.TryParse(SearchString, out date))
            {
                result = Customers.Where(i => i.LastChargedDate.Date == date.Date);
            }

            return result;
        }

        private IEnumerable<CustomerViewModel> SearchByBalance()
        {
            IEnumerable<CustomerViewModel> result = null;
            decimal balance;
            if (decimal.TryParse(SearchString, out balance))
            {
                result = Customers.Where(c => c.Balance == balance);
            }

            return result;
        }



        #endregion

        #region Commands

        private void BeginAddFunds()
        {
            Funds = new BalanceLogItemViewModel();
            SetCustomPage(AddFundsPage);
        }

        private void EndAddFunds()
        {
            Funds.Validate();
            if(Funds.HasErrors)
            {
                return;
            }

            _customerRepository.AddFunds(SelectedItem.Id, Funds.Amount, Administrator.Current.Id);
            CloseCustomPageCommand.Execute(null);
            Refresh();
        }

        private void EndCreateCustomer()
        {
            NewItem.Validate();
            if(NewItem.HasErrors)
            {
                return;
            }
            NewItem.ValidateUniqe(Items);
            if (NewItem.HasErrors)
            {
                return;
            }

            CustomerModel model = Mapper.Map<CustomerViewModel, CustomerModel>(NewItem, (v, m) =>
            {
                m.HouseId = v.House.Id;
                m.TariffId = v.Tariff.Id;
            });
            NewItem.Id = _customerRepository.Insert(model, Administrator.Current.Id);
            Customers.Add(NewItem);
            NewItem = null;
            CloseCustomPageCommand.Execute(null);
            Search();
        }

        private void EditCustomer()
        {
            SelectedItem.Validate();
            if (SelectedItem.HasErrors)
            {
                return;
            }
            SelectedItem.ValidateUniqe(Items);
            if (SelectedItem.HasErrors)
            {
                return;
            }

            CustomerModel model = Mapper.Map<CustomerViewModel, CustomerModel>(SelectedItem, (v, m) =>
            {
                m.HouseId = v.House.Id;
                m.TariffId = v.Tariff.Id;
            });
            _customerRepository.Update(model, Administrator.Current.Id);
            CloseCustomPageCommand.Execute(null);
            Search();
        }

        private void GetCharge()
        {
            CloseCustomPageCommand.Execute(null);
            _customerRepository.GetCharge(Administrator.Current.Id);
            Refresh();
        }

        private void ShowTariff()
        {
            SetCustomPage(ShowTariffPage);
        }

        private void ShowHouse()
        {
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

        #endregion

        #region Overrrides

        public override void Refresh()
        {
            CloseCustomPageCommand.Execute(null);

            Tariffs.Clear();
            Customers.Clear();
            Houses.Clear();

            foreach (var model in _tariffRepository.GetAll())
            {
                if (model.IsArchive)
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
                Customers.Add(Mapper.Map<CustomerModel, CustomerViewModel>(model, (m, v) =>
                {
                    v.Tariff = Tariffs.Where(t => t.Id == m.TariffId).First();
                    v.House = Houses.Where(h => h.Id == m.HouseId).First();
                }));
            }
            Search();
        }

        protected override void InitializeActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create customer",
                Command = BeginCreateCustomerCommand,
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
            ActionButtons.Add(new Button()
            {
                Content = "Show cusmoner`s tariff plan",
                Command = ShowTariffCommand,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Show cusmoner`s house",
                Command = ShowHouseCommand,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Show cusmoner`s balance log",
                Command = SetCustomPageCommand,
                CommandParameter = ShowCustomersBalanceLogPage,
                Margin = new Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Add funds to customer",
                Command = BeginAddFundsCommand,
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
            BeginAddFundsCommand = new RelayCommand(BeginAddFunds, SetCustomPageCanExecute);
            EndAddFundsCommand = new RelayCommand(EndAddFunds);
        }

        protected override void InitializeCustomPages()
        {
            ShowTariffPage = new ShowTariffPage() { DataContext = this };
            HousePage = new ShowConnectedHousePage() { DataContext = this };
            EditCustomerPage = new EditCustomerPage() { DataContext = this };
            MoveCustomerPage = new MoveCustomerPage() { DataContext = this };
            ChangeCustomersTariffPage = new ChangeCustomersTariffPage() { DataContext = this };
            CreateCustomerPage = new CreateCustomerPage() { DataContext = this };
            ShowCustomersBalanceLogPage = new ShowCustomersBalanceLogPage() { DataContext = this };
            AddFundsPage = new AddFundsPage() { DataContext = this };
        }

        protected override void InitializeRepository(string connectionString)
        {
            _customerRepository = new SqlCustomerRepository(connectionString);
            _connectedHouseRepository = new SqlConnectedHouseRepository(connectionString);
            _tariffRepository = new SqlTariffRepository(connectionString);
        }

        protected override void InitializeSearchColumns()
        {
            SearchColumns.AddRange(new List<string>() { "ID", "Forename", "Surname", "Tariff name", "City", "Street", "House", "Flat", "Balance", "IP address", "MAC address", "Last charged date", "State" });
        }

        protected override void InitializeViewPage()
        {
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "ID",
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
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Tariff name",
                Binding = new Binding("Tariff.Name")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "House Id",
                Binding = new Binding("House.Id")
            });
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

        public override void Search()
        {
            CloseCustomPageCommand.Execute(null);
            Items.Clear();

            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrEmpty(SelectedSearchColumn))
            {
                foreach (var item in Customers)
                {
                    Items.Add(item);
                }
                return;
            }

            IEnumerable<CustomerViewModel> result = null;

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
                case "Forename":
                    {
                        result = SearchByForename();
                        break;
                    }
                case "Surname":
                    {
                        result = SearchBySurname();
                        break;
                    }
                case "Tariff name":
                    {
                        result = SearchByTariff();
                        break;
                    }
                case "Flat":
                    {
                        result = SearchByFlat();
                        break;
                    }
                case "IP address":
                    {
                        result = SearchByIpAddress();
                        break;
                    }
                case "MAC address":
                    {
                        result = SearchByMacAddress();
                        break;
                    }
                case "Last charged date":
                    {
                        result = SearchByLastChargedDate();
                        break;
                    }
                case "Balance":
                    {
                        result = SearchByBalance();
                        break;
                    }
                case "State":
                    {
                        result = SearchByState();
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
