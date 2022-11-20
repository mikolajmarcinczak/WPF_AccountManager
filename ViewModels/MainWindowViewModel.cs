using AccountManager.Models;
using AccountManager.Properties;
using AccountManager.Services.Implementations;
using AccountManager.Services.Interfaces;
using AccountManager.Utilities;
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
    public class MainWindowViewModel : ObservableObject
    {
        //Services
        private readonly IUsersService usersService;
        private readonly IBillsService billsService;
        private readonly IInfoService infoService;
        //List<Bill> billsList = new List<Bill>();

        //WindowProperties
        private MainWindowProperties _props;
        public MainWindowProperties WindowProperties
        {
            get => _props;
            set
            {
                _props = value;
                OnPropertyChanged(nameof(WindowProperties));
            }
        }

        //DataGrid
        private List<Bill> _billsList = null;
        public List<Bill> BillsList
        {
            get => _billsList;
            set
            {
                if (_billsList == value)
                {
                    return;
                }
                _billsList = value;
                OnPropertyChanged(nameof(BillsList));
            }
        }

        private Bill _selectedItem = null;
        public Bill SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                {
                    return;
                }
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        //Commands
        public RelayCommand HomeBtnCommand { get; }
        public RelayCommand BillsBtnCommand { get; }
        public RelayCommand InfoBtnCommand { get; }
        public RelayCommand AddBillBtnCommand { get; }
        public RelayCommand AddInfoBtnCommand { get; }
        //public RelayCommand

        public MainWindowViewModel()
        {
            WindowProperties.FontSize = 20;
            WindowProperties.FontWeight = FontWeights.Bold;

            usersService = new UsersService(new AccountManagerDBFirstContext());
            billsService = new BillsService(new AccountManagerDBFirstContext());
            infoService = new InfoService(new AccountManagerDBFirstContext());

            HomeBtnCommand = new RelayCommand(HomeBtn);
            BillsBtnCommand = new RelayCommand(BillsBtn);
            InfoBtnCommand = new RelayCommand(InfoBtn);
            AddBillBtnCommand = new RelayCommand(AddBillBtn);
            AddInfoBtnCommand = new RelayCommand(AddInfoBtn);
        }

        private void HomeBtn()
        {
            WindowProperties.WelcomeLabelVis = true;
            WindowProperties.MainDataGridVis = false;
            WindowProperties.AddInfoBtnVis = false;
            WindowProperties.AddBillBtnVis = false;
            WindowProperties.WelcomeMessage = $"Welcome to Bill Manager, {Environment.NewLine}have a good day.";
        }

        private void BillsBtn()
        {
            WindowProperties.WelcomeLabelVis = false;
            WindowProperties.MainDataGridVis = true;
            WindowProperties.AddInfoBtnVis = false;
            WindowProperties.AddBillBtnVis = true;
            BillsList = billsService.GetBillsForUser(Settings.Default.UserName).ToList();
            //mainWindow.mainDataGrid.ItemsSource = BillsList;
        }

        private void InfoBtn()
        {
            WindowProperties.WelcomeLabelVis = true;
            WindowProperties.MainDataGridVis = false;
            WindowProperties.AddInfoBtnVis = true;
            WindowProperties.AddBillBtnVis = false;
            WindowProperties.WelcomeMessage = infoService.GetInformationStringForUser(Settings.Default.UserName);
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

        private void AddInfoBtn()
        {
            cxMenu = new ContextMenu();

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

            foreach (Bill item in BillsList) //selecteditems;
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

            foreach (Bill item in BillsList)
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

            /*mainWindow.mainDataGrid.SelectionChanged -= new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);

            mainWindow.mainDataGrid.ItemsSource = null;
            mainWindow.mainDataGrid.ItemsSource = BillsList;
            mainWindow.mainDataGrid.SelectedIndex = -1;
            */
            cxMenu.IsOpen = false;
            //mainWindow.mainDataGrid.SelectionChanged += new SelectionChangedEventHandler(mainDataGrid_SelectionChanged);
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


    }
}
