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

namespace Lombardia
{
    /// <summary>
    /// Interaction logic for Page13.xaml
    /// </summary>
    public partial class Page13 : UserControl
    {
        public Page13()
        {
            InitializeComponent();
          
            //ContentControl cc = documentViewer1.Template.FindName("PART_FindToolBarHost", documentViewer1) as ContentControl;
            //if (cc != null)
            //    cc.Visibility = Visibility.Hidden;
        }

        private void grid2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            reader1.Height = Application.Current.MainWindow.ActualHeight - 200;
        }
    }
}
