using Ipms.UI.Models;
using Ipms.UI.ViewModels.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using GalaSoft.MvvmLight.CommandWpf;
using Ipms.UI.Views.Tariff;
using Ipms.Repositories.Sql;
using Ipms.Repositories;
using Ipms.Models;
using System.Windows.Data;

namespace Ipms.UI.ViewModels
{
    class TariffAreaViewModel : EntityAreaViewModel<TariffViewModel>
    {
        private ITariffRepository _repository;

        private TariffViewModel _selectedSubstituteItem;


        public TariffAreaViewModel(MainViewModel parentViewModel)
            : base(parentViewModel)
        {
            SubstituteItems = new ObservableCollection<TariffViewModel>();
            Tariffs = new ObservableCollection<TariffViewModel>();

            this.PropertyChanged += TariffAreaViewModel_PropertyChanged;
            ViewPage.DataContext = this;
            Refresh();
        }

        #region Properties

        public ObservableCollection<TariffViewModel> Tariffs
        {
            get;
            private set;
        }

        public ObservableCollection<TariffViewModel> SubstituteItems
        {
            get;
            private set;
        }

        public TariffViewModel SelectedSubstituteItem
        {
            get
            {
                return _selectedSubstituteItem;
            }
            set
            {
                _selectedSubstituteItem = value;
                RaisePropertyChanged("SelectedSubstituteItem");
            }
        }
        #region Commands
        public RelayCommand RefreshCommand
        {
            get;
            private set;
        }

        public RelayCommand<Page> SetCustomPageCommand
        {
            get;
            private set;
        }

        public RelayCommand ArchiveTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand EditTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand BeginCreateTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand EndCreateTariffCommand
        {
            get;
            private set;
        }

        public EditTariffPage EditTariffPage
        {
            get;
            private set;
        }

        public TariffSubstitutePage TariffSubstitutePage
        {
            get;
            private set;
        }

        public CreateTariffPage CreateTariffPage
        {
            get;
            private set;
        }

        #endregion

        #endregion

        #region Private functions

        private void TariffAreaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                RegenerateSubsituteItems();
                SetCustomPageCommand.RaiseCanExecuteChanged();
            }
        }

        #region Commands

        private bool ArchiveTariffCanExecute()
        {
            return SelectedSubstituteItem != null;
        }
        private void ArchiveTariff()
        {
            _repository.Archive(Mapper.Map<TariffModel>(SelectedItem), Mapper.Map<TariffModel>(SelectedSubstituteItem), Administrator.Current.Id);
            CloseCustomPageCommand.Execute(null);
        }

        private void EditTariff()
        {
            SelectedItem.Validate();
            if (SelectedItem.HasErrors)
            {
                return;
            }

            TariffModel model = Mapper.Map<TariffModel>(SelectedItem);
            _repository.Update(model, Administrator.Current.Id);
            SelectedSubstituteItem = null;
            CloseCustomPageCommand.Execute(null);
            Search();
        }

        private void SetCustomPage(Page page)
        {
            Parent.CustomPage = page;
        }

        private bool ChangeCustomPageCanExecute(Page page)
        {
            if(SelectedItem == null)
            {
                return false;
            }
            return !SelectedItem.IsArchive;
        }

        private void BeginCreateTariff()
        {
            SetCustomPage(CreateTariffPage);
            NewItem = new TariffViewModel();
        }

        private void EndCreateTariff()
        {
            NewItem.Validate();
            if (NewItem.HasErrors)
            {
                return;
            }

            NewItem.Id = _repository.Insert(Mapper.Map<TariffModel>(NewItem), Administrator.Current.Id);
            Tariffs.Add(NewItem);
            CloseCustomPageCommand.Execute(null);
            Search();
        }


        #endregion

        private void RegenerateSubsituteItems()
        {
            SubstituteItems.Clear();
            foreach (var item in Items)
            {
                if (item.IsArchive || item == SelectedItem)
                {
                    continue;
                }

                SubstituteItems.Add(item);
            }
        }

        #region Search

        private IEnumerable<TariffViewModel> SearchById()
        {
            IEnumerable<TariffViewModel> result = null;
            int id;
            if (int.TryParse(SearchString, out id))
            {
                result = Tariffs.Where(t => t.Id == id);
            }

            return result;
        }

        private IEnumerable<TariffViewModel> SearchByUploadSpeed()
        {
            IEnumerable<TariffViewModel> result = null;
            int uploadSpeed;
            if (int.TryParse(SearchString, out uploadSpeed))
            {
                result = Tariffs.Where(t => t.UploadSpeed == uploadSpeed);
            }

            return result;
        }

        private IEnumerable<TariffViewModel> SearchByDownloadSpeed()
        {
            IEnumerable<TariffViewModel> result = null;
            int downloadSpeed;
            if (int.TryParse(SearchString, out downloadSpeed))
            {
                result = Tariffs.Where(t => t.DownloadSpeed == downloadSpeed);
            }

            return result;
        }

        private IEnumerable<TariffViewModel> SearchByPrice()
        {
            IEnumerable<TariffViewModel> result = null;
            decimal price;
            if (decimal.TryParse(SearchString, out price))
            {
                result = Tariffs.Where(t => t.Price == price);
            }

            return result;
        }
        private IEnumerable<TariffViewModel> SearchByName()
        {
            return Tariffs.Where(t => t.Name.ToLower().Contains(SearchString.ToLower()));
        }

        #endregion



        #endregion

        #region Overrides

        public override void Refresh()
        {
            CloseCustomPageCommand.Execute(null);

            Tariffs.Clear();

            foreach (var item in _repository.GetAll())
            {
                Tariffs.Add(Mapper.Map<TariffViewModel>(item));
            }
            RegenerateSubsituteItems();
            Search();
        }

        protected override void InitializeCommands()
        {
            EditTariffCommand = new RelayCommand(EditTariff);
            ArchiveTariffCommand = new RelayCommand(ArchiveTariff, ArchiveTariffCanExecute);
            SetCustomPageCommand = new RelayCommand<Page>(SetCustomPage, ChangeCustomPageCanExecute);
            BeginCreateTariffCommand = new RelayCommand(BeginCreateTariff);
            EndCreateTariffCommand = new RelayCommand(EndCreateTariff);
            RefreshCommand = new RelayCommand(Refresh);
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
                Binding = new Binding("Name"),
                Header = "Name"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("Price"),
                Header = "Price"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("DownloadSpeed"),
                Header = "Download speed"
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Binding = new Binding("UploadSpeed"),
                Header = "Upload speed"
            });
            ViewPage.DataGridColumns.Add(new DataGridCheckBoxColumn()
            {
                Binding = new Binding("IsArchive"),
                Header = "Archive"
            });
        }

        protected override void InitializeActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create tariff",
                Command = BeginCreateTariffCommand,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)

            });
            ActionButtons.Add(new Button()
            {
                Content = "Change tariff",
                Command = SetCustomPageCommand,
                CommandParameter = EditTariffPage,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Archive tariff",
                Command = SetCustomPageCommand,
                CommandParameter = TariffSubstitutePage,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)
            });
        }

        protected override void InitializeCustomPages()
        {
            EditTariffPage = new EditTariffPage() { DataContext = this };
            TariffSubstitutePage = new TariffSubstitutePage() { DataContext = this };
            CreateTariffPage = new CreateTariffPage() { DataContext = this };
        }

        protected override void InitializeSearchColumns()
        {
            SearchColumns.AddRange(new List<string> { "Id", "Name", "Price", "Download speed", "Upload speed" });
        }

        protected override void InitializeRepository(string connectionString)
        {
            _repository = new SqlTariffRepository(connectionString);
        }



        public override void Search()
        {
            CloseCustomPageCommand.Execute(null);
            Items.Clear();

            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrEmpty(SelectedSearchColumn))
            {
                foreach (var item in Tariffs)
                {
                    Items.Add(item);
                }
                return;
            }

            IEnumerable<TariffViewModel> result = null;

            switch (SelectedSearchColumn)
            {
                case "ID":
                    {
                        result = SearchById();
                        break;
                    }
                case "Name":
                    {
                        result = SearchByName();
                        break;
                    }
                case "Download speed":
                    {
                        result = SearchByDownloadSpeed();
                        break;
                    }
                case "Upload speed":
                    {
                        result = SearchByUploadSpeed();
                        break;
                    }
                case "Price":
                    {
                        result = SearchByPrice();
                        break;
                    }
            }

            if(result == null)
            {
                return;
            }

            foreach(var item in result)
            {
                Items.Add(item);
            }
        }


        #endregion
    }
}
