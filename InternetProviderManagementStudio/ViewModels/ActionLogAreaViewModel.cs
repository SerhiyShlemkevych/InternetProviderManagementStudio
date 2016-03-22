using Ipms.UI.ViewModels.Entities;
using Ipms.Repositories;
using Ipms.Repositories.Sql;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Ipms.UI.Models;
using Ipms.Models;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;

namespace Ipms.UI.ViewModels
{
    class ActionLogAreaViewModel : EntityAreaViewModel<ActionLogItemViewModel>
    {
        private IActionLogRepository _repository;

        public ActionLogAreaViewModel(ParentViewModel parentViewModel) : base(parentViewModel)
        {
            ActionLogItems = new ObservableCollection<ActionLogItemViewModel>();
        }

        #region Properties

        public ObservableCollection<ActionLogItemViewModel> ActionLogItems
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

        #region Private functions

        #region Search

        private IEnumerable<ActionLogItemViewModel> SearchById()
        {
            IEnumerable<ActionLogItemViewModel> result = null;
            int id;
            if (int.TryParse(SearchString, out id))
            {
                result = ActionLogItems.Where(i => i.Id == id);
            }

            return result;
        }

        private IEnumerable<ActionLogItemViewModel> SearchByDate()
        {
            IEnumerable<ActionLogItemViewModel> result = null;
            DateTime date;
            if (DateTime.TryParse(SearchString, out date))
            {
                result = ActionLogItems.Where(i => i.Date.Date == date.Date);
            }

            return result;
        }

        private IEnumerable<ActionLogItemViewModel> SearchByAction()
        {
            return ActionLogItems.Where(i => i.Action.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<ActionLogItemViewModel> SearchByTarget()
        {
            return ActionLogItems.Where(i => i.Target.ToLower().Contains(SearchString.ToLower()));
        }

        private IEnumerable<ActionLogItemViewModel> SearchByAdministratorLogin()
        {
            return ActionLogItems.Where(i => i.Administrator.Login.ToLower().Contains(SearchString.ToLower()));
        }

        #endregion

        #endregion

        #region Overrides

        public override void Refresh()
        {
            CloseCustomPageCommand.Execute(null);
            ActionLogItems.Clear();

            foreach (var model in _repository.GetAll())
            {
                ActionLogItems.Add(Mapper.Map<ActionLogItemModel, ActionLogItemViewModel>(model, (m, v) =>
                {
                    v.Administrator = Mapper.Map<AdminisratorViewModel>(m.Administrator);
                }));
            }

            Search();
        }

        public override void Search()
        {
            Items.Clear();

            if (string.IsNullOrEmpty(SearchString) || string.IsNullOrEmpty(SelectedSearchColumn))
            {
                foreach (var item in ActionLogItems)
                {
                    Items.Add(item);
                }
                return;
            }

            IEnumerable<ActionLogItemViewModel> result = null;

            switch (SelectedSearchColumn)
            {
                case "ID":
                    {
                        result = SearchById();
                        break;
                    }
                case "Action":
                    {
                        result = SearchByAction();
                        break;
                    }
                case "Date":
                    {
                        result = SearchByDate();
                        break;
                    }
                case "Target":
                    {
                        result = SearchByTarget();
                        break;
                    }
                case "Administrator login":
                    {
                        result = SearchByAdministratorLogin();
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

        protected override void InitializeActionButtons()
        {
            
        }

        protected override void InitializeCommands()
        {
            RefreshCommand = new RelayCommand(Refresh);
        }

        protected override void InitializeCustomPages()
        {
            
        }

        protected override void InitializeRepository(string connectionString)
        {
            _repository = new SqlActionLogRepository(connectionString);
        }

        protected override void InitializeSearchColumns()
        {
            SearchColumns.AddRange(new List<string>() {"ID", "Action", "Target", "Date", "Administrator login" });
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
                Header = "Action",
                Binding = new Binding("Action")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Target",
                Binding = new Binding("Target")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Date",
                Binding = new Binding("Date")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Administrator`s login",
                Binding = new Binding("Administrator.Login")
            });
            ViewPage.DataGridColumns.Add(new DataGridTextColumn()
            {
                Header = "Affected row IDs",
                Binding = new Binding("AffectedRowIds")
            });
        }

        #endregion
    }
}
