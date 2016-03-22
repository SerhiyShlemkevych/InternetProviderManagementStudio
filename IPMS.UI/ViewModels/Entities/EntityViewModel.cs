using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipms.UI.ViewModels.Entities
{
    public abstract class EntityViewModel : ViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors;

        public EntityViewModel()
        {
            _validationErrors = new Dictionary<string, ICollection<string>>();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get
            {
                return _validationErrors.Count > 0;
            }
        }

        protected Dictionary<string, ICollection<string>> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
            {
                return null;
            }

            return _validationErrors[propertyName];
        }

        public void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public abstract void Validate();
    }
}
