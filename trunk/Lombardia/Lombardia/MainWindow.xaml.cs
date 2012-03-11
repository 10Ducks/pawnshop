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
using System.Globalization;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.IO;
using System.Windows.Xps.Serialization;

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
            ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
            InputLanguageManager.Current.InputLanguageChanged += new InputLanguageEventHandler(Current_InputLanguageChanged);
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create
            Page1 selectCustomer = new Page1();
            selectCustomer.next.Click += new RoutedEventHandler(next_customer_Click);
            selectCustomer.inpPassport1.SelectionChanged += new SelectionChangedEventHandler(inpPassport1_SelectionChanged);
            selectCustomer.inpSName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.dataGrid1.MouseDoubleClick += new MouseButtonEventHandler(dataGrid1_MouseDoubleClick);

            if (countryList == null)
                countryList = new Countrys();

            foreach (string country in countryList.list)
            {
                selectCustomer.inpPassport1.Items.Add(country);
            }

            selectCustomer.inpPassport1.SelectedIndex = 0;
            //selectCustomer.dataGrid1.Height = mainGrid.ActualHeight - 450;
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
            // Print Page
            Page10 selectItem = new Page10();

            XpsDocument doc = new XpsDocument(@"asd.xps", FileAccess.ReadWrite);
            //XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(doc);
            //XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(doc), false);
            //FrameworkElement visual = null;
            //rsm.SaveAsXaml(visual);
            selectItem.documentViewer1.Document = doc.GetFixedDocumentSequence();
            
            //selectItem.documentViewer1.

            pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(selectItem);
            ribbonButton1_finish.IsEnabled = false;
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
            item1.Content = "Առաջին ենթատեսակ";
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Երկրորդ ենթատեսակ";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Երրորդ ենթատեսակ";
            var selectItem = pageStack.Children[1];
            ((Page2)selectItem).comboBox2.Items.Clear();
            ((Page2)selectItem).comboBox2.Items.Add(item1);
            ((Page2)selectItem).comboBox2.Items.Add(item2);
            ((Page2)selectItem).comboBox2.Items.Add(item3);
            ((Page2)selectItem).comboBox2.SelectedIndex = 0;

        }

        private void inpPassport1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // If country is changed, passport format must be changed also   
            if (pageStack.Children.Count > 1)
            {
                var selectItem = pageStack.Children[1];
                if (selectItem.GetType().Name == "Page1")
                {
                    if (((Page1)selectItem).inpPassport1.SelectedIndex == 0)
                    {
                        ((Page1)selectItem).inpPassport2.Visibility = System.Windows.Visibility.Visible;
                        ((Page1)selectItem).inpPassport2.Text = String.Empty;
                        ((Page1)selectItem).inpPassport3.Width = 190;
                        ((Page1)selectItem).inpPassport3.Margin = new Thickness(394, 126, 0, 0);
                        ((Page1)selectItem).label9.IsEnabled = true;
                        ((Page1)selectItem).inpPassport4.IsEnabled = true;
                        ((Page1)selectItem).inpPassport5.IsEnabled = true;
                    }
                    else
                    {
                        ((Page1)selectItem).inpPassport2.Visibility = System.Windows.Visibility.Hidden;
                        ((Page1)selectItem).inpPassport2.Text = String.Empty;
                        ((Page1)selectItem).inpPassport3.Width = 239;
                        ((Page1)selectItem).inpPassport3.Margin = new Thickness(345, 126, 0, 0);
                        ((Page1)selectItem).label9.IsEnabled = false;
                        ((Page1)selectItem).inpPassport4.IsEnabled = false;
                        ((Page1)selectItem).inpPassport5.IsEnabled = false;
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
                    added_item.itemType = ((Page2)selectItem).comboBox1.Text;
                    added_item.itemSubType = ((Page2)selectItem).comboBox2.Text;
                    added_item.descr = ((Page2)selectItem).textBox11.Text;

                    added_item.measure_value1 = ((Page2)selectItem).textBox12.Text;
                    added_item.measure_unit1 = ((Page2)selectItem).comboBox3.Text;
                    added_item.measure_value2 = ((Page2)selectItem).textBox7.Text;
                    added_item.measure_unit2 = ((Page2)selectItem).comboBox4.Text;

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

        private void Current_InputLanguageChanged(object sender, InputLanguageEventArgs e)
        {
            ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }

        private void ribbonButton8_Click(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = CultureInfo.CreateSpecificCulture("hy");
            ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }

        private void inputedName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox name = (TextBox)sender;
            var selectItem = pageStack.Children[1];
            if (name.Text.StartsWith("Art"))
            {
                Customer suggest = new Customer("Ավետիսյան", "Արտակ", "Սամվելի", "ՀՀ", "AG 32142, 004, 27/05/2009", "ք. Երևան, Իսակովի 3/4", "010265798, 093655869", "Ծնվ. 11/05/1988");

                ((Page1)selectItem).dataGrid1.Items.Clear();
                ((Page1)selectItem).dataGrid1.Items.Add(suggest);
            }
            else if (name.Text.StartsWith("Արտ"))
            {
                Customer suggest = new Customer("Ավետիսյան", "Արտակ", "Սամվելի", "ՀՀ", "AG 32142, 004, 27/05/2009", "ք. Երևան, Իսակովի 3/4", "010265798, 093655869", "Ծնվ. 11/05/1988");

                ((Page1)selectItem).dataGrid1.Items.Clear();
                ((Page1)selectItem).dataGrid1.Items.Add(suggest);
            }
            else if (name.Text.StartsWith("Ավե"))
            {
                Customer suggest = new Customer("Ավետիսյան", "Արտակ", "Սամվելի", "ՀՀ", "AG 32142, 004, 27/05/2009", "ք. Երևան, Իսակովի 3/4", "010265798, 093655869", "Ծնվ. 11/05/1988");
                Customer suggest1 = new Customer("Ավետիսյան", "Արամ", "Սամվելի", "ՀՀ", "AG 002511, 025, 27/05/2010", "ք. Երևան, Արշակունյաց 35, բն. 9", "010444875, 095599855", "Սոց. քարտ 541465435455");

                ((Page1)selectItem).dataGrid1.Items.Clear();
                ((Page1)selectItem).dataGrid1.Items.Add(suggest);
                ((Page1)selectItem).dataGrid1.Items.Add(suggest1);
            }
            else if (name.Text.StartsWith("Ave"))
            {
                Customer suggest = new Customer("Ավետիսյան", "Արտակ", "Սամվելի", "ՀՀ", "AG 32142, 004, 27/05/2009", "ք. Երևան, Իսակովի 3/4", "010265798, 093655869", "Ծնվ. 11/05/1988");
                Customer suggest1 = new Customer("Ավետիսյան", "Արամ", "Սամվելի", "ՀՀ", "AG 002511, 025, 27/05/2010", "ք. Երևան, Արշակունյաց 35, բն. 9", "010444875, 095599855", "Սոց. քարտ 541465435455");

                ((Page1)selectItem).dataGrid1.Items.Clear();
                ((Page1)selectItem).dataGrid1.Items.Add(suggest);
                ((Page1)selectItem).dataGrid1.Items.Add(suggest1);
            }
            else
            {
                ((Page1)selectItem).dataGrid1.Items.Clear();
            }
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectItem = pageStack.Children[1];
            Customer selected = (Customer)((DataGrid)sender).CurrentItem;
            ((Page1)selectItem).inpSName.Text = selected.secondName;
            ((Page1)selectItem).inpFName.Text = selected.firstName;
            ((Page1)selectItem).inpMName.Text = selected.middleName;

            ((Page1)selectItem).inpPassport1.Text = selected.country;
            ((Page1)selectItem).inpPassport2.Text = selected.passportData.Split(new Char[] {',', ' '})[0];
            ((Page1)selectItem).inpPassport3.Text = selected.passportData.Split(new Char[] { ',', ' ' })[1];
            ((Page1)selectItem).inpPassport4.Text = selected.passportData.Split(new Char[] { ',', ' ' })[3];
            ((Page1)selectItem).inpPassport5.Text = selected.passportData.Split(new Char[] { ',', ' ' })[5];

            ((Page1)selectItem).inpAddress.Text = selected.address;
            ((Page1)selectItem).inpPhone.Text = selected.phone;
            ((Page1)selectItem).inpDetails.Text = selected.details;
        }

        private void ribbonButton9_Click(object sender, RoutedEventArgs e)
        {
            // Edit Items Tree
        }

        private void ribbonButton1_delete_Click(object sender, RoutedEventArgs e)
        {
            // Delete a contract
            Page9 page = new Page9();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = true;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton_export_Click(object sender, RoutedEventArgs e)
        {
            // Export to Excel
        }

        private void ribbonButton_editor_Click(object sender, RoutedEventArgs e)
        {
            // Show Document Template Editor
            Page11 page = new Page11();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }
    }
}
