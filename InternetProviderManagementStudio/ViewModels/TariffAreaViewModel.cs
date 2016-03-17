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

namespace InternetProviderManagementStudio.ViewModels
{
    class TariffAreaViewModel : ChildViewModel
    {
        private ObservableCollection<TariffViewModel> _dataGridItems;

        private ITariffRepository _repository;

        public TariffAreaViewModel(MainWindowViewModel parentViewModel, Page viewPage)
            : base(parentViewModel, viewPage)
        {
            InitializeCommands();
            CreateActionButtons();

            _repository = new SqlTariffRepository("");
            _dataGridItems = new ObservableCollection<TariffViewModel>();           
            foreach(var item in _repository.GetAll())
            {
                _dataGridItems.Add(Mapper.Map<TariffViewModel>(item));
            }
            _dataGridItems.CollectionChanged += _dataGridItems_CollectionChanged;
        }

        private void _dataGridItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateActionButtons()
        {
            ActionButtons.Add(new Button()
            {
                Content = "Create teriff",
                Command = null
            });
            ActionButtons.Add(new Button()
            {
                Content = "Change teriff",
                Command = null
            });
            ActionButtons.Add(new Button()
            {
                Content = "Remove teriff",
                Command = null
            });
        }

        private void InitializeCommands()
        {

        }
    }
}
