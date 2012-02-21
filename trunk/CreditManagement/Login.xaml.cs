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

namespace CreditManagement
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
            grid1.Visibility = System.Windows.Visibility.Visible;
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;
            else
            {
                grid1.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
