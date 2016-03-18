using IPMS.Repositories;
using IPMS.Repositories.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    class HouseAreaViewModel : ChildViewModel
    {
        IConnectedHouseRepository _repository;

        public HouseAreaViewModel(MainWindowViewModel parentViewModel, Page viewPage)
            : base(parentViewModel, viewPage)
        {
            InitializeCommands();
            AddActionButtons();

            _repository = new SqlConnectedHouseRepository(ConfigurationManager.ConnectionStrings["default"].ConnectionString);

        }

        private void InitializeCommands()
        {

        }

        private void AddActionButtons()
        {

        }
    }
}
