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
        Countrys countryList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create
            Page1 selectCustomer = new Page1();
            selectCustomer.next.Click += new RoutedEventHandler(next_customer_Click);
            selectCustomer.countrys.SelectionChanged +=new SelectionChangedEventHandler(countrys_SelectionChanged);

            if (countryList == null)
                countryList = new Countrys();

            foreach (string country in countryList.list)
            {
                selectCustomer.countrys.Items.Add(country);
            }

            selectCustomer.countrys.SelectedIndex = 0;

            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(selectCustomer);
            ribbonButton1_cancel.IsEnabled = true;
            
        }

        private void ribbonButton2_Click(object sender, RoutedEventArgs e)
        {
            // Search Contract
            Page5 page = new Page5();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton3_Click(object sender, RoutedEventArgs e)
        {
            // Hot List
            Page3 page = new Page3();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton4_Click(object sender, RoutedEventArgs e)
        {
            // All contracts Search by date
            Page4 page = new Page4();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
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
            selectItem.button1.Click += new RoutedEventHandler(button_add_item_Click);
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

        private void countrys_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // If country is changed, passport format must be changed also   
            if (pageStack.Children.Count > 1)
            {
                var selectItem = pageStack.Children[1];
                if (selectItem.GetType().Name == "Page1")
                {
                    if (((Page1)selectItem).countrys.SelectedIndex == 0)
                    {
                        ((Page1)selectItem).textBox3.Visibility = System.Windows.Visibility.Visible;
                        ((Page1)selectItem).textBox3.Text = String.Empty;
                        ((Page1)selectItem).textBox4.Width = 190;
                        ((Page1)selectItem).textBox4.Margin = new Thickness(394, 126, 0, 0);
                        ((Page1)selectItem).label9.IsEnabled = true;
                        ((Page1)selectItem).textBox9.IsEnabled = true;
                        ((Page1)selectItem).textBox10.IsEnabled = true;
                    }
                    else
                    {
                        ((Page1)selectItem).textBox3.Visibility = System.Windows.Visibility.Hidden;
                        ((Page1)selectItem).textBox3.Text = String.Empty;
                        ((Page1)selectItem).textBox4.Width = 239;
                        ((Page1)selectItem).textBox4.Margin = new Thickness(345, 126, 0, 0);
                        ((Page1)selectItem).label9.IsEnabled = false;
                        ((Page1)selectItem).textBox9.IsEnabled = false;
                        ((Page1)selectItem).textBox10.IsEnabled = false;
                    }
                }
            }
        }
        private void button_add_item_Click(object sender, RoutedEventArgs e)
        {
            // On Items page ADD button clicked

            if (pageStack.Children.Count > 1)
            {
                var selectItem = pageStack.Children[1];
                if (selectItem.GetType().Name == "Page2")
                {
                    Item added_item = new Item();
                    added_item.itemType = ((Page2)selectItem).comboBox1.SelectionBoxItem.ToString();
                    added_item.itemSubType = ((Page2)selectItem).comboBox2.SelectionBoxItem.ToString();
                    added_item.descr = ((Page2)selectItem).textBox11.Text;

                    added_item.measure_value1 = ((Page2)selectItem).textBox12.Text;
                    added_item.measure_unit1 = ((Page2)selectItem).comboBox3.SelectionBoxItem.ToString();
                    added_item.measure_value2 = ((Page2)selectItem).textBox7.Text;
                    added_item.measure_unit2 = ((Page2)selectItem).comboBox4.SelectionBoxItem.ToString();

                    ((Page2)selectItem).dataGrid1.Items.Add(added_item);

                }

            }
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            // Exit
            this.Close();
        }

        private void ribbonButton5_Click(object sender, RoutedEventArgs e)
        {
            // Add new Client
            Page6 page = new Page6();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton6_Click(object sender, RoutedEventArgs e)
        {
            // Search Client
            Page7 page = new Page7();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton7_Click(object sender, RoutedEventArgs e)
        {
            // Edit Client Data
            Page8 page = new Page8();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }
    }
}
