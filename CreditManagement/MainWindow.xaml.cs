using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;

namespace CreditManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        MainWindowViewModel _viewModel;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)base.DataContext;
            
            //grid2.Visibility = System.Windows.Visibility.Hidden;
            //grid3.Visibility = System.Windows.Visibility.Hidden;
            //ribbon.Visibility = System.Windows.Visibility.Hidden;
            //ribbon.Margin = new Thickness(0,-500,0,500);
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void DoLogin(object sender, DoWorkEventArgs e)
        {
            
        }

        private void LoginCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Thread.Sleep(4000);
            
            //ribbon.Visibility = System.Windows.Visibility.Visible;
            //ribbon.UpdateLayout();
            //ribbon.Margin = new Thickness(0, -22, 0, 22);
        }

        private void StartLogin()
        {
            //Show your wait dialog
            BackgroundWorker login = new BackgroundWorker();
            login.DoWork += DoLogin;
            login.RunWorkerCompleted += LoginCompleted;
            login.RunWorkerAsync();
        }


    }
}
