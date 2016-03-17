﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InternetProviderManagementStudio.ViewModels
{
    abstract class ParentViewModel : ViewModel
    {
        private Page _viewPage;
        private IEnumerable<Button> _actionButtons;
        private Page _customPage;

        public Page ViewPage
        {
            get
            {
                return _viewPage;
            }
            set
            {
                _viewPage = value;
                RaisePropertyChanged("ViewPage");
            }
        }

        public Page CustomPage
        {
            get
            {
                return _customPage;
            }
            set
            {
                _customPage = value;
                RaisePropertyChanged("CustomPage");
            }
        }

        public IEnumerable<Button> ActionButtons
        {
            get
            {
                return _actionButtons;
            }
            set
            {
                _actionButtons = value;
                RaisePropertyChanged("ActionButtons");
            }
        }
    }
}