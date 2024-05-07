using StationeryStoreManagementSystem.BL;
using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            showColumnChart();
        }
        private void showColumnChart()
        {
            List<KeyValuePair<string, int>> SaleData = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Monday", 10),
                new KeyValuePair<string, int>("Tuesday", 20),
                new KeyValuePair<string, int>("Wednesday", 30),
                new KeyValuePair<string, int>("Thursday", 40),
                new KeyValuePair<string, int>("Friday", 50),
                new KeyValuePair<string, int>("Saturday", 60),
                new KeyValuePair<string, int>("Sunday", 70)
            };
            List<KeyValuePair<string, int>> ProfitData = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Monday", 5),
                new KeyValuePair<string, int>("Tuesday", 10),
                new KeyValuePair<string, int>("Wednesday", 15),
                new KeyValuePair<string, int>("Thursday", 20),
                new KeyValuePair<string, int>("Friday", 25),
                new KeyValuePair<string, int>("Saturday", 30),
                new KeyValuePair<string, int>("Sunday", 35)
            };
            WeeklySale.DataContext = SaleData;
            WeeklyProfit.DataContext = ProfitData;
            MonthlySale.DataContext = SaleData;
            MonthlyProfit.DataContext = ProfitData;
        }

        private void NotificationButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("From","From"),
                ("IsViewed","IsViewed"),
                ("Notification","Notification")
            };
            if (GlobalSettings.DisplayIds == true)
                bindings.Insert(0, ("Id", "Id"));
            ((Border)Parent).Child = new UI.ManageEntity("Manage Notifications",
                                                typeof(Notification).Name,
                                                NotificationDL.GetNotifications_View,
                                                bindings,
                                                new List<string> { "From" },
                                                typeof(NotificationForm),
                                                false,
                                                false,
                                                null,
                                                typeof(ViewNotification));
        }

        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
