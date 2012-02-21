using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;

namespace CreditManagement
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Construction
        public MainWindowViewModel()
        {
            _user = new string('1',0);
            _password = new string('1', 0);
            _status = new string('1', 0);
        }
        #endregion

        #region Members
        string _user;
        string _password;
        string _status;
        #endregion

        #region Properties
        public string Status
        {
            get { return _status; }
            set { 
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public string Username
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        private void RaisePropertyChanged(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
