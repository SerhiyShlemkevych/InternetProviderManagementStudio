using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.UI.ViewModels.Entities
{
    class BalanceLogItemViewModel : EntityViewModel
    {
        private int _id;
        private int _customerId;
        private decimal _amount;
        private decimal _balance;
        private DateTime _date;
        private string _description;

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

        public int CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                RaisePropertyChanged("CustomerId");
            }
        }

        public decimal Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                ValidateAmount();
                RaisePropertyChanged("Amount");
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                RaisePropertyChanged("Balance");
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

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public override void Validate()
        {
            ValidateAmount();
        }

        private void ValidateAmount()
        {
            if (Amount <= 0m)
            {
                ValidationErrors["Amount"] = new List<string>() { "Amount has to be grater then 0" };
                RaiseErrorsChanged("Amount");
            }
            else if (ValidationErrors.ContainsKey("Amount"))
            {
                ValidationErrors.Remove("Amount");
                RaiseErrorsChanged("Amount");
            }
        }
    }
}
