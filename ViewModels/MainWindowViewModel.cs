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

namespace AccountManager.ViewModels
{
    public class MainWindowViewModel
    {
        private readonly MainWindow mainWindow;
        private readonly IUsersService usersService;
        private readonly IBillsService billsService;
        private readonly IInfoService infoService;
        List<Bill> billsList = new List<Bill>();

        public MainWindowViewModel (MainWindow mainWindow,
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
            mainWindow.welcomeLabel.Content = infoService.GetInformationStringForUser(Setttings.Default.UserName);
        }

        ContextMenu cxMenu = null;
        TextBox txtFN, txtLN, janTB, febTB, marTB, aprTB, mayTB, junTB, julTB, augTB, sepTB, octTB, novTB, decTB, nameTB;
        TextBlock txtId;

        private void AddBillBtn()
        {
            cxMenu = new ContextMenu();

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
        }

        private void AddInfoBtn()
        {

        }
    }
}
