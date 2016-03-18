using InternetProviderManagementStudio.Models;
using InternetProviderManagementStudio.Models.Tariff;
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
using InternetProviderManagementStudio.Views.TariffArea;

namespace InternetProviderManagementStudio.ViewModels
{
    class TariffAreaViewModel : ChildViewModel
    {

        private ITariffRepository _repository;

        private TariffViewModel _selectedSubstituteItem;
        private TariffViewModel _selectedItem;
        private TariffViewModel _newItem;


        public TariffAreaViewModel(MainWindowViewModel parentViewModel, Page viewPage)
            : base(parentViewModel, viewPage)
        {
            EditTariffPage = new EditTariffPage() { DataContext = this };
            TariffSubstitutePage = new TariffSubstitutePage() { DataContext = this };
            CreateTariffPage = new CreateTariffPage() { DataContext = this };
            InitializeCommands();
            CreateActionButtons();



            _repository = new SqlTariffRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);
            DataGridItems = new ObservableCollection<TariffViewModel>();
            SubstituteItems = new ObservableCollection<TariffViewModel>();
            DataGridItems.CollectionChanged += DataGridItems_CollectionChanged;
            foreach (var item in _repository.GetAll())
            {
                DataGridItems.Add(Mapper.Map<TariffViewModel>(item));
            }

            this.PropertyChanged += TariffAreaViewModel_PropertyChanged;
        }


        public ObservableCollection<TariffViewModel> DataGridItems
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

        public TariffViewModel SelectedItem
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

        public TariffViewModel NewItem
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

        private void TariffAreaViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedItem")
            {
                ChangeCustomPageCommand.RaiseCanExecuteChanged();
                if (SelectedItem != null)
                {
                    SelectedItem.RaiseAllpropertiesChanged();
                }                
            }
            else if(e.PropertyName == "NewItem")
            {
                if (NewItem != null)
                {
                    NewItem.RaiseAllpropertiesChanged();
                }
            }
        }

        private void DataGridItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                TariffViewModel tariff = (TariffViewModel)e.NewItems[0];
                if (!tariff.IsArchive)
                {
                    SubstituteItems.Add(tariff);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                TariffViewModel tariff = (TariffViewModel)e.NewItems[0];
                if (SubstituteItems.Contains(tariff))
                {
                    SubstituteItems.Remove(tariff);
                }
                if (SelectedItem == tariff)
                {
                    SelectedItem = null;
                }
                if (SelectedSubstituteItem == tariff)
                {
                    SelectedSubstituteItem = null;
                }
            }
        }

        private void CreateActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create tariff",
                Command = ShowCreatePageCommand,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)

            });
            ActionButtons.Add(new Button()
            {
                Content = "Change teriff",
                Command = ChangeCustomPageCommand,
                CommandParameter = EditTariffPage,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)
            });
            ActionButtons.Add(new Button()
            {
                Content = "Archive teriff",
                Command = ChangeCustomPageCommand,
                CommandParameter = TariffSubstitutePage,
                Margin = new System.Windows.Thickness(0, 5, 0, 5)
            });
        }

        private bool ArchiveTariffCanExecute()
        {
            return SelectedSubstituteItem != null;
        }
        private void ArchiveTariff()
        {
            _repository.Archive(Mapper.Map<TariffModel>(SelectedItem), Mapper.Map<TariffModel>(SelectedSubstituteItem));
        }

        private void ChangeTariff()
        {
            TariffModel model = Mapper.Map<TariffModel>(SelectedItem);
            _repository.Update(model);
            SelectedSubstituteItem = null;
            
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
            NewItem.Id = _repository.Insert(Mapper.Map<TariffModel>(NewItem));
            DataGridItems.Add(NewItem);
            
        }

        private void InitializeCommands()
        {
            ChangeTariffCommand = new RelayCommand(ChangeTariff);
            ArchiveTariffCommand = new RelayCommand(ArchiveTariff, ArchiveTariffCanExecute);
            ChangeCustomPageCommand = new RelayCommand<Page>(ChangeCustomPage, ChangeCustomPageCanExecute);
            ShowCreatePageCommand = new RelayCommand(ShowCreatePage);
            CreateTariffCommand = new RelayCommand(CreateTariff);
        }
    }
}
