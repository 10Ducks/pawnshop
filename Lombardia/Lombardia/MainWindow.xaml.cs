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
using Lombardia.Classes;
using System.Collections;
using System.Threading;

namespace Lombardia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Countrys countryList;
        private Database db;
        private string loggedInUser;
        private string loggedInUserPermissions;
        private Customer selectedCustomer;
        private ArrayList dictPercents;
        private ArrayList selectedItems;
        private Contract selectedContract;
        

        public MainWindow()
        {
            InitializeComponent();            
            //ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
            InputLanguageManager.Current.InputLanguageChanged += new InputLanguageEventHandler(Current_InputLanguageChanged);
            db = new Database();
            cUsername.Focus();
            if (dictPercents == null)
            {
                dictPercents = new ArrayList();
                dictPercents = db.getPercents();
            }
        }

        private void ribbonButton1_Click(object sender, RoutedEventArgs e)
        {
            // Create
            Page1 selectCustomer = new Page1();
            selectCustomer.next.Click += new RoutedEventHandler(next_customer_Click);
            selectCustomer.inpPassport1.SelectionChanged += new SelectionChangedEventHandler(inpPassport1_SelectionChanged);
            selectCustomer.inpSName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpFName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpMName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            selectCustomer.inpPassport2.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport3.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport4.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport5.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            selectCustomer.inpPhone.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpAddress.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpDetails.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);            

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

                if (db.login(cUsername.Text, Password.Password, out loggedInUser, out loggedInUserPermissions))
                {
                    pageStack.Children.RemoveAt(1);
                    ribbon.IsEnabled = true;
                }
                else
                {
                    LoginStatus.Content = "Մուտքն արգելված է";
                }
            }
        }

        private void next_customer_Click(object sender, RoutedEventArgs e)
        {
            // Clicked Next button on Create Contract Page
            // Add itms to contract page
            var selectItem1 = pageStack.Children[1];

            selectedCustomer = new Customer(
                        ((Page1)selectItem1).inpSName.Text, 
                        ((Page1)selectItem1).inpFName.Text,
                        ((Page1)selectItem1).inpMName.Text, 
                        ((Page1)selectItem1).inpPassport1.SelectedItem.ToString(),
                        ((Page1)selectItem1).inpPassport2.Text + " " +
                        ((Page1)selectItem1).inpPassport3.Text + " " +
                        ((Page1)selectItem1).inpPassport4.Text + " " +
                        ((Page1)selectItem1).inpPassport5.Text,
                        ((Page1)selectItem1).inpAddress.Text,
                        ((Page1)selectItem1).inpPhone.Text, 
                        ((Page1)selectItem1).inpDetails.Text);

            if (String.IsNullOrWhiteSpace(selectedCustomer.secondName) ||
                String.IsNullOrWhiteSpace(selectedCustomer.firstName) ||
                String.IsNullOrWhiteSpace(selectedCustomer.passportData))
            {
                return;
            }

            Page2 selectItem = new Page2();
            
            ComboBoxItem item1 = new ComboBoxItem();
            item1.Content = "Ոսկեղեն";            
            ComboBoxItem item2 = new ComboBoxItem();
            item2.Content = "Տեխնիկա";
            ComboBoxItem item3 = new ComboBoxItem();
            item3.Content = "Ավտոմեքենա";
            ComboBoxItem item4 = new ComboBoxItem();
            item4.Content = "Այլ";

            selectItem.comboBox1.Items.Add(item1);
            selectItem.comboBox1.Items.Add(item2);
            selectItem.comboBox1.Items.Add(item3);
            selectItem.comboBox1.Items.Add(item4);

            selectItem.customerNameLabel.Content = selectedCustomer.secondName + " "
                + selectedCustomer.firstName + " " + selectedCustomer.middleName;
            selectItem.customerDataLabel.Content = "Անձնագիր՝ " + selectedCustomer.passportData + ", Հեռ. "
                + selectedCustomer.phone;

            selectItem.contractDetailsLabel.Content = "Սկիզբ՝ " + System.DateTime.Now.ToShortDateString()
                + "\nԱվարտ՝ " + System.DateTime.Now.AddMonths(1).ToShortDateString();

            if (selectedItems == null)
                selectedItems = new ArrayList();

            selectedItems.Clear();

            selectItem.comboBox1.SelectionChanged += new SelectionChangedEventHandler(comboBox1_SelectionChanged);
            selectItem.next.Click += new RoutedEventHandler(next_item_Click);
            selectItem.button1.Click += new RoutedEventHandler(button_add_item_Click);
            selectItem.button2.Click += new RoutedEventHandler(button_back_to_customer_Click);
            pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(selectItem);
            ribbonButton1_finish.IsEnabled = true;
        }

        private void next_item_Click(object sender, RoutedEventArgs e)
        {
            // Print Page
            var currentItem = pageStack.Children[1];

            if (selectedCustomer != null
                && selectedItems != null
                && selectedItems.Count != 0
                && !String.IsNullOrEmpty(((Page2)currentItem).textBoxAmount.Text)
                && Double.Parse(((Page2)currentItem).textBoxAmount.Text) > 0)
            {
                Page10 selectItem = new Page10();
                
                double amount = Double.Parse(((Page2)currentItem).textBoxAmount.Text);
                string selectedItemsType = ((Item)selectedItems[0]).itemType;
                double totalCost = 0;
                double percent = 0;

                foreach (Item item in selectedItems)
                {
                    totalCost += Double.Parse(item.price);
                }
                
                foreach( dictPercents dictItem in dictPercents )
                {
                    if (dictItem.type == selectedItemsType)
                    {
                        if (amount > Double.Parse(dictItem.from_amount) && amount < Double.Parse(dictItem.to_amount))
                            percent = Double.Parse(dictItem.yearly);
                    }
                }
                
                selectedContract = new Contract("0001", amount, totalCost, percent);

                selectItem.ClientName.Text = selectedCustomer.secondName + " " + selectedCustomer.firstName + " " + selectedCustomer.middleName;
                selectItem.ClientName1.Text = selectedCustomer.secondName + " " + selectedCustomer.firstName + " " + selectedCustomer.middleName;
                selectItem.ClientAddress.Text = selectedCustomer.address;
                selectItem.ClientPhone.Text = selectedCustomer.phone;
                selectItem.Passport.Text = selectedCustomer.passportData;
                selectItem.DateNow.Text = System.DateTime.Now.ToShortDateString();
                selectItem.DateNext.Text = System.DateTime.Now.AddMonths(1).ToShortDateString();
                selectItem.AmountWords.Text = Utils.NumberToWords((int)amount);
                selectItem.Amount.Text = amount.ToString();
                selectItem.contractID1.Text = selectedContract.number;
                selectItem.contractID2.Text = selectedContract.number;
                selectItem.Percent1.Text = selectedContract.percent.ToString();
                selectItem.PercentLegal.Text = selectedContract.percentLegal.ToString();
                selectItem.Percent2.Text = selectedContract.percentAdded.ToString();
                selectItem.Total.Text = selectedContract.amountExpected.ToString();
                selectItem.RealCost.Text = selectedContract.amountEstimated.ToString();


                selectItem.PrintContract1.Click +=new RoutedEventHandler(PrintContract1_Click);
                selectItem.PrintContractCancel.Click += new RoutedEventHandler(ribbonButton1_cancel_Click);
                
                pageStack.Children.RemoveAt(1);
                pageStack.Children.Add(selectItem);
                ribbonButton1_finish.IsEnabled = false;
            }
        }

        private void PrintContract1_Click(object sender, RoutedEventArgs e)
        {
            // Print Contract Page 1 clicked
            var selectItem = pageStack.Children[1];

            PrintDialog pd = new PrintDialog();           

            IDocumentPaginatorSource dps = ((Page10)selectItem).conractPage1;
            pd.PrintDocument(dps.DocumentPaginator, "Contract Page 1");
            pd.PrintDocument(dps.DocumentPaginator, "Contract Page 1 Copy");

            Page13 page = new Page13();

            page.ClientName.Text = selectedCustomer.secondName + " " + selectedCustomer.firstName + " " + selectedCustomer.middleName;
            page.ClientName1.Text = selectedCustomer.secondName + " " + selectedCustomer.firstName + " " + selectedCustomer.middleName;
            page.contractID1.Text = selectedContract.number;
            page.contractID2.Text = selectedContract.number;
            page.contractID3.Text = selectedContract.number;
            page.Passport.Text = selectedCustomer.passportData;
            page.Passport1.Text = selectedCustomer.passportData;
            page.Date1.Text = selectedContract.startDate;
            page.Date2.Text = selectedContract.startDate;
            page.RealCost.Text = selectedContract.amountEstimated.ToString();
            page.RealCostWords.Text = Utils.NumberToWords((int)selectedContract.amountEstimated);

            page.PrintContract1.Click += new RoutedEventHandler(PrintContract2_Click);
            page.PrintContractCancel.Click += new RoutedEventHandler(ribbonButton1_cancel_Click);

            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
            pageStack.Children.Add(page);


        }

        private void PrintContract2_Click(object sender, RoutedEventArgs e)
        {
            // Print Contract Page 1 clicked
            var selectItem = pageStack.Children[1];

            PrintDialog pd = new PrintDialog();

            IDocumentPaginatorSource dps = ((Page13)selectItem).conractPage1;
            pd.PrintDocument(dps.DocumentPaginator, "Contract Page 2");
            pd.PrintDocument(dps.DocumentPaginator, "Contract Page 2 Copy");

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

                Thread oThread = new Thread(new ParameterizedThreadStart(getCustomerSuggestion));
                oThread.Start(selectItem);
            }
        }

        public void getCustomerSuggestion(object selectItem)
        {
            // If country is changed, passport format must be changed also            
                
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
            else if (selectItem.GetType().Name == "Page6")
            {
                if (((Page6)selectItem).inpPassport1.SelectedIndex == 0)
                {
                    ((Page6)selectItem).inpPassport2.Visibility = System.Windows.Visibility.Visible;
                    ((Page6)selectItem).inpPassport2.Text = String.Empty;
                    ((Page6)selectItem).inpPassport3.Width = 190;
                    ((Page6)selectItem).inpPassport3.Margin = new Thickness(394, 126, 0, 0);
                    ((Page6)selectItem).label9.IsEnabled = true;
                    ((Page6)selectItem).inpPassport4.IsEnabled = true;
                    ((Page6)selectItem).inpPassport5.IsEnabled = true;
                }
                else
                {
                    ((Page6)selectItem).inpPassport2.Visibility = System.Windows.Visibility.Hidden;
                    ((Page6)selectItem).inpPassport2.Text = String.Empty;
                    ((Page6)selectItem).inpPassport3.Width = 239;
                    ((Page6)selectItem).inpPassport3.Margin = new Thickness(345, 126, 0, 0);
                    ((Page6)selectItem).label9.IsEnabled = false;
                    ((Page6)selectItem).inpPassport4.IsEnabled = false;
                    ((Page6)selectItem).inpPassport5.IsEnabled = false;
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
                    added_item.itemType = (((Page2)selectItem).comboBox1.SelectedIndex + 1).ToString();
                    added_item.itemSubType = ((Page2)selectItem).comboBox2.Text;
                    added_item.descr = ((Page2)selectItem).textBox11.Text;

                    added_item.measures = "";
                    added_item.price = ((Page2)selectItem).textBox1.Text;

                    selectedItems.Add(added_item);

                    double totalCost = 0;

                    foreach (Item item in selectedItems)
                    {
                        totalCost += Double.Parse(item.price);
                    }
                    ((Page2)selectItem).totalsLabel.Content = "Ընդհանուր գնահատված գումար՝ "+ totalCost.ToString() +" AMD";

                    ((Page2)selectItem).dataGrid1.Items.Add(added_item);

                }

            }
        }

        private void button_back_to_customer_Click(object sender, RoutedEventArgs e)
        {
            Page1 selectCustomer = new Page1();
            selectCustomer.next.Click += new RoutedEventHandler(next_customer_Click);
            selectCustomer.inpPassport1.SelectionChanged += new SelectionChangedEventHandler(inpPassport1_SelectionChanged);
            selectCustomer.inpSName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpFName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpMName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            selectCustomer.inpPassport2.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport3.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport4.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpPassport5.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            selectCustomer.inpPhone.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpAddress.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            selectCustomer.inpDetails.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            selectCustomer.dataGrid1.MouseDoubleClick += new MouseButtonEventHandler(dataGrid1_MouseDoubleClick);

            selectCustomer.inpSName.Text = selectedCustomer.secondName;
            selectCustomer.inpFName.Text = selectedCustomer.firstName;
            selectCustomer.inpMName.Text = selectedCustomer.middleName;

            selectCustomer.inpPassport1.Text = selectedCustomer.country;
            selectCustomer.inpPassport2.Text = selectedCustomer.passportData.Split(new Char[] { ',', ' ' })[0];
            selectCustomer.inpPassport3.Text = selectedCustomer.passportData.Split(new Char[] { ',', ' ' })[1];
            selectCustomer.inpPassport4.Text = selectedCustomer.passportData.Split(new Char[] { ',', ' ' })[2];
            selectCustomer.inpPassport5.Text = selectedCustomer.passportData.Split(new Char[] { ',', ' ' })[3];

            selectCustomer.inpAddress.Text = selectedCustomer.address;
            selectCustomer.inpPhone.Text = selectedCustomer.phone;
            selectCustomer.inpDetails.Text = selectedCustomer.details;

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

            //var selectItem = pageStack.Children[1];
            
            
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

            if (countryList == null)
                countryList = new Countrys();

            foreach (string country in countryList.list)
            {
                page.inpPassport1.Items.Add(country);
            }

            page.inpPassport1.SelectedIndex = 0;

            page.inpSName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpFName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpMName.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            page.inpPassport2.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpPassport3.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpPassport4.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpPassport5.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            page.inpPhone.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpAddress.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);
            page.inpDetails.TextChanged += new TextChangedEventHandler(inputedName_TextChanged);

            page.inpPassport1.SelectionChanged += new SelectionChangedEventHandler(inpPassport1_SelectionChanged);
            page.next.Click += new RoutedEventHandler(insert_customer_Click);
            page.dataGrid1.MouseDoubleClick += new MouseButtonEventHandler(dataGrid1_MouseDoubleClick);

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
            //ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }

        private void ribbonButton8_Click(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = CultureInfo.CreateSpecificCulture("hy");
            //ribbonButton8.Label = InputLanguageManager.Current.CurrentInputLanguage.DisplayName;
        }

        private void inputedName_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var selectItem = pageStack.Children[1];

            ArrayList customers = new ArrayList();

            if (selectItem.GetType().Name == "Page1")
            {
                customers = db.searchCustomer(((Page1)selectItem).inpSName.Text,
                    ((Page1)selectItem).inpFName.Text,
                    ((Page1)selectItem).inpMName.Text,
                    ((Page1)selectItem).inpPassport2.Text + " " + ((Page1)selectItem).inpPassport3.Text + " " +
                    ((Page1)selectItem).inpPassport4.Text + " " + ((Page1)selectItem).inpPassport5.Text,
                    ((Page1)selectItem).inpPhone.Text,
                    ((Page1)selectItem).inpAddress.Text);

                ((Page1)selectItem).dataGrid1.Items.Clear();

                if (customers != null)
                {
                    foreach (Customer suggest in customers)
                    {
                        ((Page1)selectItem).dataGrid1.Items.Add(suggest);
                    }
                }
            }
            else if (selectItem.GetType().Name == "Page6")
            {
                customers = db.searchCustomer(((Page6)selectItem).inpSName.Text,
                    ((Page6)selectItem).inpFName.Text,
                    ((Page6)selectItem).inpMName.Text,
                    ((Page6)selectItem).inpPassport2.Text + " " + ((Page6)selectItem).inpPassport3.Text + " " +
                    ((Page6)selectItem).inpPassport4.Text + " " + ((Page6)selectItem).inpPassport5.Text,
                    ((Page6)selectItem).inpPhone.Text,
                    ((Page6)selectItem).inpAddress.Text);

                ((Page6)selectItem).dataGrid1.Items.Clear();

                if (customers != null)
                {
                    foreach (Customer suggest in customers)
                    {
                        ((Page6)selectItem).dataGrid1.Items.Add(suggest);
                    }
                }
            }
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectItem = pageStack.Children[1];
            Customer selected = (Customer)((DataGrid)sender).CurrentItem;

            if (selectItem.GetType().Name == "Page1")
            {
                ((Page1)selectItem).inpSName.Text = selected.secondName;
                ((Page1)selectItem).inpFName.Text = selected.firstName;
                ((Page1)selectItem).inpMName.Text = selected.middleName;

                ((Page1)selectItem).inpPassport1.Text = selected.country;
                ((Page1)selectItem).inpPassport2.Text = selected.passportData.Split(new Char[] { ',', ' ' })[0];
                ((Page1)selectItem).inpPassport3.Text = selected.passportData.Split(new Char[] { ',', ' ' })[1];
                ((Page1)selectItem).inpPassport4.Text = selected.passportData.Split(new Char[] { ',', ' ' })[2];
                ((Page1)selectItem).inpPassport5.Text = selected.passportData.Split(new Char[] { ',', ' ' })[3];

                ((Page1)selectItem).inpAddress.Text = selected.address;
                ((Page1)selectItem).inpPhone.Text = selected.phone;
                ((Page1)selectItem).inpDetails.Text = selected.details;
            }
            else if (selectItem.GetType().Name == "Page6")
            {
                ((Page6)selectItem).inpSName.Text = selected.secondName;
                ((Page6)selectItem).inpFName.Text = selected.firstName;
                ((Page6)selectItem).inpMName.Text = selected.middleName;

                ((Page6)selectItem).inpPassport1.Text = selected.country;
                ((Page6)selectItem).inpPassport2.Text = selected.passportData.Split(new Char[] { ',', ' ' })[0];
                ((Page6)selectItem).inpPassport3.Text = selected.passportData.Split(new Char[] { ',', ' ' })[1];
                ((Page6)selectItem).inpPassport4.Text = selected.passportData.Split(new Char[] { ',', ' ' })[2];
                ((Page6)selectItem).inpPassport5.Text = selected.passportData.Split(new Char[] { ',', ' ' })[3];

                ((Page6)selectItem).inpAddress.Text = selected.address;
                ((Page6)selectItem).inpPhone.Text = selected.phone;
                ((Page6)selectItem).inpDetails.Text = selected.details;
            }
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

        private void insert_customer_Click(object sender, RoutedEventArgs e)
        {
            // Insert new customer data to DB
            var selectItem = pageStack.Children[1];

            Customer selected = new Customer(
                        ((Page6)selectItem).inpSName.Text, 
                        ((Page6)selectItem).inpFName.Text,
                        ((Page6)selectItem).inpMName.Text, 
                        ((Page6)selectItem).inpPassport1.SelectedItem.ToString(),
                        ((Page6)selectItem).inpPassport2.Text + " " +
                        ((Page6)selectItem).inpPassport3.Text + " " +
                        ((Page6)selectItem).inpPassport4.Text + " " +
                        ((Page6)selectItem).inpPassport5.Text,
                        ((Page6)selectItem).inpAddress.Text,
                        ((Page6)selectItem).inpPhone.Text, 
                        ((Page6)selectItem).inpDetails.Text);

            db.insertCustomer(selected);

            ArrayList customers = new ArrayList();

            if (selectItem.GetType().Name == "Page6")
            {
                customers = db.searchCustomer(((Page6)selectItem).inpSName.Text,
                    ((Page6)selectItem).inpFName.Text,
                    ((Page6)selectItem).inpMName.Text,
                    ((Page6)selectItem).inpPassport2.Text + " " + ((Page6)selectItem).inpPassport3.Text + " " +
                    ((Page6)selectItem).inpPassport4.Text + " " + ((Page6)selectItem).inpPassport5.Text,
                    ((Page6)selectItem).inpPhone.Text,
                    ((Page6)selectItem).inpAddress.Text);

                ((Page6)selectItem).dataGrid1.Items.Clear();

                if (customers != null)
                {
                    foreach (Customer suggest in customers)
                    {
                        ((Page6)selectItem).dataGrid1.Items.Add(suggest);
                    }
                }
            }
        }

        private void HomeButton(object sender, RoutedEventArgs e)
        {
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);
        }

        private void ribbonButton_percents_Click(object sender, RoutedEventArgs e)
        {
            // Show Percents List
            Page12 page = new Page12();
            if (pageStack.Children.Count > 1)
                pageStack.Children.RemoveAt(1);

            if (dictPercents == null)
            {
                dictPercents = new ArrayList();
                dictPercents = db.getPercents();
            }

            if (dictPercents != null)
            {
                foreach (dictPercents item in dictPercents)
                {
                    page.dataGrid1.Items.Add(item);
                }
            }

            pageStack.Children.Add(page);
            ribbonButton1_cancel.IsEnabled = false;
            ribbonButton1_finish.IsEnabled = false;
        }

        private void ribbonButton8_eng_Click(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = CultureInfo.CreateSpecificCulture("en");
        }

    }
}
