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
            Page1 selectCustomer = new Page1();
            selectCustomer.next.Click += new RoutedEventHandler(next_customer_Click);
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(selectCustomer);
            ribbonButton1_cancel.IsEnabled = true;
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
                pageStack.Children.RemoveAt(1);
                ribbon.IsEnabled = true;
            }
        }

        private void next_customer_Click(object sender, RoutedEventArgs e)
        {
            // Clicked Next button on Create Contract Page
            // Add itms to contract page
            Page2 selectItem = new Page2();
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Առաջին տեսակ";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Երկրորդ տեսակ";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Երրորդ տեսակ";
            selectItem.comboBox1.Items.Add(item1);
            selectItem.comboBox1.Items.Add(item2);
            selectItem.comboBox1.Items.Add(item3);

            selectItem.comboBox1.SelectionChanged += new SelectionChangedEventHandler(comboBox1_SelectionChanged);
            selectItem.next.Click += new RoutedEventHandler(next_item_Click);
            pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(selectItem);
            ribbonButton1_finish.IsEnabled = true;
        }

        private void next_item_Click(object sender, RoutedEventArgs e)
        {
            // All contracts
        }

        private void ribbonButton1_cancel_Click(object sender, RoutedEventArgs e)
        {
            // Cancel the contract creation
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton1_finish_Click(object sender, RoutedEventArgs e)
        {
            // Finish and print Contract
        }

        private void comboBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // After Selecting Item First Type
            // Second Combo must be filled with correct values
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Առաջին տեսակ";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Երկրորդ տեսակ";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Երրորդ տեսակ";
            var selectItem = pageStack.Children[1];
            ((Page2)selectItem).comboBox2.Items.Add(item1);

        }
    }
}
