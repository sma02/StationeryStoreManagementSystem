using Microsoft.VisualBasic;
using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ManageCompanies.xaml
    /// </summary>
    public partial class ManageCompanies : UserControl
    {
        public ManageCompanies()
        {
            InitializeComponent();
            var companies = CompanyDL.GetCompanies();
            dg_companies.ItemsSource = companies.DefaultView;
        }

        private void add_company_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.CompanyForm();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)dg_companies.ItemsSource).Table;
            int id = (int)table.DefaultView[dg_companies.SelectedIndex].Row.ItemArray[0];
            ((Border)Parent).Child = new UI.CompanyForm(id);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)dg_companies.ItemsSource).Table;
            int id = (int)table.DefaultView[dg_companies.SelectedIndex].Row.ItemArray[0];
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CompanyDL.DeleteCompany(id);   
            }
            ((Border)Parent).Child = new UI.ManageCompanies();
        }
    }
}
