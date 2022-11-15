using AccountManager.Models;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace AccountManager.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;
        private readonly IBillsService billsService;
        private readonly IInfoService infoService;
        List<Bill> billsList = new List<Bill>();

        public MainWindowViewModel(MainWindow mainWindow,
                                    IUsersService usersService,
                                    IBillsService billsService,
                                    IInfoService infoService)
        {
            this.mainWindow = mainWindow;
            this.usersService = usersService;
            this.billsService = billsService;
            this.infoService = infoService;
            mainWindow.welcomeLabel.FontSize = 20;
            mainWindow.welcomeLabel.FontWeight = FontWeights.Bold;
            HomeBtn();
        }

        private void HomeBtn()
        {
            mainWindow.welcomeLabel.Visibility = Visibility.Visible;
            mainWindow.mainDataGrid.Visibility = Visibility.Hidden;
            mainWindow.addInfoBtn.Visibility = Visibility.Hidden;
            mainWindow.addBillBtn.Visibility = Visibility.Hidden;
            mainWindow.welcomeLabel.Content = $"Welcome to Bill Manager, {Environment.NewLine}have a good day.";
        }

        private void BillsBtn()
        {
            mainWindow.welcomeLabel.Visibility = Visibility.Hidden;
            mainWindow.mainDataGrid.Visibility = Visibility.Visible;
            mainWindow.addInfoBtn.Visibility = Visibility.Hidden;
            mainWindow.addBillBtn.Visibility = Visibility.Visible;
            billsList = billsService.GetBillsForUser(Settings.Default.UserName).ToList();
            mainWindow.mainDataGrid.ItemsSource = billsList;
        }

        private void InfoBtn()
        {
            mainWindow.welcomeLabel.Visibility = Visibility.Visible;
            mainWindow.mainDataGrid.Visibility = Visibility.Hidden;
            mainWindow.addInfoBtn.Visibility = Visibility.Visible;
            mainWindow.welcomeLabel.Content = infoService.GetInformationStringForUser(Settings.Default.UserName);
        }

        ContextMenu cxMenu = null;
        TextBox txtFN, txtLN, janTB, febTB, marTB, aprTB, mayTB, junTB, julTB, augTB, sepTB, octTB, novTB, decTB, nameTB;
        TextBlock txtId;

        private void AddBillBtn()
        {
            cxMenu = new ContextMenu();

            GenerateMonthTB(cxMenu);

            Button saveBillBtn = new Button();
            saveBillBtn.Content = "Save";
            cxMenu.Items.Add(saveBillBtn);
            cxMenu.IsOpen = true;
            saveBillBtn.Click += new RoutedEventHandler(saveBillBtnClick);
        }

        private void saveBillBtnClick(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill()
            {
                April = Double.Parse(aprTB.Text),
                August = Double.Parse(augTB.Text),
                December = Double.Parse(decTB.Text),
                February = Double.Parse(febTB.Text),
                January = Double.Parse(janTB.Text),
                July = Double.Parse(julTB.Text),
                June = Double.Parse(junTB.Text),
                March = Double.Parse(marTB.Text),
                May = Double.Parse(mayTB.Text),
                November = Double.Parse(novTB.Text),
                October = Double.Parse(octTB.Text),
                September = Double.Parse(sepTB.Text),
                BillName = nameTB.Text,
                UserId = usersService.GetUserIdByName(Settings.Default.UserName),
            };
            billsService.Add(bill);
        }

        private void mainDataGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            DependencyObject depObj = (DependencyObject)e.OriginalSource;

            while (
                (depObj != null) &&
                !(depObj is DataGridCell) &&
                !(depObj is DataGridColumnHeader))
            {
                depObj = VisualTreeHelper.GetParent(depObj);
            }

            if (depObj == null)
            {
                return;
            }

            if (depObj is DataGridCell)
            {
                while ((depObj != null) && !(depObj is DataGridRow))
                {
                    depObj = VisualTreeHelper.GetParent(depObj);
                }

                DataGridRow dgRow = depObj as DataGridRow;
                dgRow.ContextMenu = cxMenu;
            }
        }

        private void mainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cxMenu = new ContextMenu();

            foreach (Bill item in mainWindow.mainDataGrid.SelectedItems)
            {
                bill = new Bill
                {
                    Id = item.Id,
                    April = item.April,
                    August = item.August,
                    December = item.December,
                    February = item.February,
                    January = item.January,
                    July = item.July,
                    June = item.June,
                    March = item.March,
                    May = item.May,
                    November = item.November,
                    October = item.October,
                    September = item.September,
                    BillName = item.BillName,
                    BillsYear = item.BillsYear
                };
            }

            GenerateMonthTB(cxMenu);

            Button btnSave = new Button();
            btnSave.Content = "Save";
            cxMenu.Items.Add(btnSave);
            btnSave.Click += new RoutedEventHandler(SaveBillsBtn);
        }

        Bill bill = null;

        private void SaveBillsBtn(object sender, RoutedEventArgs e)
        {
            Bill bill = new Bill();

            foreach (Bill item in billsList)
            {
                if (item.Id == Convert.ToInt32(txtId.Text))
                {
                    item.April = Double.Parse(aprTB.Text == "" ? "0" : aprTB.Text);
                    item.August = Double.Parse(augTB.Text == "" ? "0" : augTB.Text);
                    item.December = Double.Parse(decTB.Text == "" ? "0" : decTB.Text);
                    item.February = Double.Parse(febTB.Text == "" ? "0" : febTB.Text);
                    item.January = Double.Parse(janTB.Text == "" ? "0" : janTB.Text);
                    item.July = Double.Parse(julTB.Text == "" ? "0" : julTB.Text); ;
                    item.June = Double.Parse(junTB.Text == "" ? "0" : junTB.Text);
                    item.March = Double.Parse(marTB.Text == "" ? "0" : marTB.Text);
                    item.May = Double.Parse(mayTB.Text == "" ? "0" : mayTB.Text);
                    item.November = Double.Parse(novTB.Text == "" ? "0" : novTB.Text);
                    item.October = Double.Parse(octTB.Text == "" ? "0" : octTB.Text);
                    item.September = Double.Parse(sepTB.Text == "" ? "0" : sepTB.Text);

                    billsService.Edit(item);
                }
            }

            mainWindow.mainDataGrid.SelectionChanged -= new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);

            mainWindow.mainDataGrid.ItemsSource = null;
            mainWindow.mainDataGrid.ItemsSource = billsList;
            mainWindow.mainDataGrid.SelectedIndex = -1;

            cxMenu.IsOpen = false;
            mainWindow.mainDataGrid.SelectionChanged += new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);
        }

        private void GenerateMonthTB(ContextMenu cxMenu)
        {
            var bill = new Bill();
            txtId = new TextBlock();
            txtId.Text = bill.Id.ToString();
            cxMenu.Items.Add(txtId);

            nameTB = new TextBox();
            nameTB.Text = "Name";
            cxMenu.Items.Add("NAME");
            cxMenu.Items.Add(nameTB);

            janTB = new TextBox();
            janTB.Text = "000";
            cxMenu.Items.Add("JANUARY");
            cxMenu.Items.Add(janTB);

            febTB = new TextBox();
            febTB.Text = "000";
            cxMenu.Items.Add("FEBRUARY");
            cxMenu.Items.Add(febTB);

            marTB = new TextBox();
            marTB.Text = "000";
            cxMenu.Items.Add("MARCH");
            cxMenu.Items.Add(marTB);

            aprTB = new TextBox();
            aprTB.Text = "000";
            cxMenu.Items.Add("APRIL");
            cxMenu.Items.Add(aprTB);

            mayTB = new TextBox();
            mayTB.Text = "000";
            cxMenu.Items.Add("MAY");
            cxMenu.Items.Add(mayTB);

            junTB = new TextBox();
            junTB.Text = "000";
            cxMenu.Items.Add("JUNE");
            cxMenu.Items.Add(junTB);

            julTB = new TextBox();
            julTB.Text = "000";
            cxMenu.Items.Add("JULY");
            cxMenu.Items.Add(julTB);

            augTB = new TextBox();
            augTB.Text = "000";
            cxMenu.Items.Add("AUGUST");
            cxMenu.Items.Add(augTB);

            sepTB = new TextBox();
            sepTB.Text = "000";
            cxMenu.Items.Add("SEPTEMBER");
            cxMenu.Items.Add(sepTB);

            octTB = new TextBox();
            octTB.Text = "000";
            cxMenu.Items.Add("OCTOBER");
            cxMenu.Items.Add(octTB);

            novTB = new TextBox();
            novTB.Text = "000";
            cxMenu.Items.Add("NOVEMBER");
            cxMenu.Items.Add(novTB);

            decTB = new TextBox();
            decTB.Text = "000";
            cxMenu.Items.Add("DECEMBER");
            cxMenu.Items.Add(decTB);
        }

        private void AddInfoBtn(object sender, RoutedEventArgs e)
        {
            cxMenu = new ContextMenu();
            DependencyObject depObj = (DependencyObject)e.OriginalSource;

            txtId = new TextBlock();
            txtId.Text = "Info: ";
            cxMenu.Items.Add(txtId);

            txtFN = new TextBox();
            txtFN.Text = "Info: ";
            cxMenu.Items.Add(txtFN);

            txtId = new TextBlock();
            txtId.Text = "Content: ";
            cxMenu.Items.Add(txtId);

            txtLN = new TextBox();
            txtLN.Text = "Content: ";
            cxMenu.Items.Add(txtLN);

            Button SaveInfoBtn = new Button();
            SaveInfoBtn.Content = "Save";
            cxMenu.Items.Add(SaveInfoBtn);
            cxMenu.IsOpen = true;
            SaveInfoBtn.Click += new RoutedEventHandler(SaveInfoBtnClick);
        }

        private void SaveInfoBtnClick(object sender, RoutedEventArgs e)
        {
            Information info = new Information()
            {
                Content = txtFN.Text,
                InformationName = txtLN.Text,
                UserId = usersService.GetUserIdByName(Settings.Default.UserName)
            };

            infoService.AddInformation(info);

            cxMenu.IsOpen = false;
        }
    }
}
