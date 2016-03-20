using InternetProviderManagementStudio.Models;
using InternetProviderManagementStudio.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Configuration;
using GalaSoft.MvvmLight.CommandWpf;
using InternetProviderManagementStudio.ViewModels;
using InternetProviderManagementStudio.Views.Tariff;
using IPMS.Repositories.Sql;
using IPMS.Repositories;
using IPMS.Models;
using System.Windows.Data;

namespace InternetProviderManagementStudio.ViewModels
{
    class TariffAreaViewModel : EntityViewModel<TariffViewModel>
    {
        private ITariffRepository _repository;

        private TariffViewModel _selectedSubstituteItem;


        public TariffAreaViewModel(MainViewModel parentViewModel)
            : base(parentViewModel)
        {
            SubstituteItems = new ObservableCollection<TariffViewModel>();


            this.PropertyChanged += TariffAreaViewModel_PropertyChanged;
            ViewPage.DataContext = this;
            Refresh();
        }

        #region Properties

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
            _repository.Archive(Mapper.Map<TariffModel>(SelectedItem), Mapper.Map<TariffModel>(SelectedSubstituteItem), Administartor.Current.Id);
            CloseCustomPageCommand.Execute(null);
        }

        private void EditTariff()
        {
            SelectedItem.Validate();
            if(SelectedItem.HasErrors)
            {
                return;
            }

            TariffModel model = Mapper.Map<TariffModel>(SelectedItem);
            _repository.Update(model, Administartor.Current.Id);
            SelectedSubstituteItem = null;
            CloseCustomPageCommand.Execute(null);
        }

        private void SetCustomPage(Page page)
        {
            Parent.CustomPage = page;
        }

        private bool ChangeCustomPageCanExecute(Page page)
        {
            return SelectedItem != null;
        }

        private void BeginCreateTariff()
        {
            SetCustomPage(CreateTariffPage);
            NewItem = new TariffViewModel();
        }

        private void EndCreateTariff()
        {
            NewItem.Validate();
            if(NewItem.HasErrors)
            {
                return;
            }

            NewItem.Id = _repository.Insert(Mapper.Map<TariffModel>(NewItem), Administartor.Current.Id);
            Items.Add(NewItem);
            CloseCustomPageCommand.Execute(null);
        }

        private void Refresh()
        {
            Items.Clear();
            
            foreach (var item in _repository.GetAll())
            {
                Items.Add(Mapper.Map<TariffViewModel>(item));
            }
            RegenerateSubsituteItems();
            CloseCustomPageCommand.Execute(null);
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
                Header = "Id"
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
        #endregion
    }
}
