﻿using System;
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
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : UserControl
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void grid2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dataGrid1.Height = Application.Current.MainWindow.ActualHeight - 445;
        }
    }
}