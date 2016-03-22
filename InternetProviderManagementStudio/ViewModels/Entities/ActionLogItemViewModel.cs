using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ipms.UI.ViewModels;

namespace Ipms.UI.ViewModels.Entities
{
    class ActionLogItemViewModel : ViewModel
    {
        private int _id;
        private string _target;
        private string _action;
        private string _affectedRowIds;
        private DateTime _date;
        private AdminisratorViewModel _administrator;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }
        public AdminisratorViewModel Administrator
        {
            get
            {
                return _administrator;
            }
            set
            {
                _administrator = value;
                RaisePropertyChanged("Administrator");
            }
        }
        public string Target
        {
            get
            {
                return _target;
            }
            set
            {
                _target = value;
                RaisePropertyChanged("Target");
            }
        }
        public string Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
                RaisePropertyChanged("Action");
            }
        }
        public string AffectedRowIds
        {
            get
            {
                return _affectedRowIds;
            }
            set
            {
                _affectedRowIds = value;
                RaisePropertyChanged("AffectedRowIds");
            }
        }
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged("Date");
            }
        }
    }
}
