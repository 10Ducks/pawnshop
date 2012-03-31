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
using Lombardia.Classes;

namespace Lombardia
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page12 : UserControl
    {
        public Page12()
        {
            InitializeComponent();
        }

        private void grid2_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            //dataGrid1.Items.Add(new dictPercents() { type = "Ոսկեղեն", from_amount = "0", to_amount = "0", monthly = "0.0", yearly = "0.0", ratio = "0.0"});
            //dataGrid1.BeginEdit();
        }
      
    }
}
