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


        public TariffAreaViewModel(MainWindowViewModel parentViewModel)
            : base(parentViewModel)
        {
            SearchColumns.AddRange(new List<string> { "Id", "Name", "Price", "Download speed", "Upload speed" });

            _repository = new SqlTariffRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
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

        public RelayCommand<Page> ChangeCustomPageCommand
        {
            get;
            private set;
        }

        public RelayCommand ArchiveTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand ChangeTariffCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowCreatePageCommand
        {
            get;
            private set;
        }

        public RelayCommand CreateTariffCommand
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
                ChangeCustomPageCommand.RaiseCanExecuteChanged();
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
            CloseCustomPageCommand.Execute(Type.Missing);
        }

        private void ChangeTariff()
        {
            TariffModel model = Mapper.Map<TariffModel>(SelectedItem);
            _repository.Update(model, Administartor.Current.Id);
            SelectedSubstituteItem = null;
            CloseCustomPageCommand.Execute(Type.Missing);
        }

        private void ChangeCustomPage(Page page)
        {
            Parent.CustomPage = page;
        }

        private bool ChangeCustomPageCanExecute(Page page)
        {
            return SelectedItem != null;
        }

        private void ShowCreatePage()
        {
            ChangeCustomPage(CreateTariffPage);
            NewItem = new TariffViewModel();
        }

        private void CreateTariff()
        {
            NewItem.Id = _repository.Insert(Mapper.Map<TariffModel>(NewItem), Administartor.Current.Id);
            Items.Add(NewItem);
            CloseCustomPageCommand.Execute(Type.Missing);
        }

        private void Refresh()
        {
            Items.Clear();
            foreach (var item in _repository.GetAll())
            {
                Items.Add(Mapper.Map<TariffViewModel>(item));
            }
            RegenerateSubsituteItems();
            CloseCustomPageCommand.Execute(Type.Missing);
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
            ChangeTariffCommand = new RelayCommand(ChangeTariff);
            ArchiveTariffCommand = new RelayCommand(ArchiveTariff, ArchiveTariffCanExecute);
            ChangeCustomPageCommand = new RelayCommand<Page>(ChangeCustomPage, ChangeCustomPageCanExecute);
            ShowCreatePageCommand = new RelayCommand(ShowCreatePage);
            CreateTariffCommand = new RelayCommand(CreateTariff);
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
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
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
                Command = ShowCreatePageCommand,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)

            });
            ActionButtons.Add(new Button()
            {
                Content = "Change tariff",
                Command = ChangeCustomPageCommand,
                CommandParameter = EditTariffPage,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Archive tariff",
                Command = ChangeCustomPageCommand,
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
        #endregion
    }
}
