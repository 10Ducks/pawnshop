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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton(object sender, KeyEventArgs e)
        {
            // Exit
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create
        }

        private void ribbonButton2_Click(object sender, RoutedEventArgs e)
        {
            // Search Contract
        }

        private void ribbonButton3_Click(object sender, RoutedEventArgs e)
        {
            // Hot List
        }

        private void ribbonButton4_Click(object sender, RoutedEventArgs e)
        {
            // All contracts
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // On Window Loaded
            ribbon.IsEnabled = false;
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            // Login on enter
            if (!e.Key.Equals(Key.Enter)) return;
            else
            {
                // Make login procedure
                loginForm.IsEnabled = false;
                loginForm.Visibility = Visibility.Hidden;
                ribbon.IsEnabled = true;
            }
        }


    }
}
