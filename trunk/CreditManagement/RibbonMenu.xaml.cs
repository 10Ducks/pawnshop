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
    /// Interaction logic for RibbonMenu.xaml
    /// </summary>
    public partial class RibbonMenu : UserControl
    {
        public RibbonMenu()
        {
            InitializeComponent();
        }

        private void ExitButton(object sender, KeyEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            //grid2.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
