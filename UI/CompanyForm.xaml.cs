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
using StationeryStoreManagementSystem.BL;
using StationeryStoreManagementSystem.DL;
using StationeryStoreManagementSystem.UI.Components;

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for CompanyForm.xaml
    /// </summary>
    public partial class CompanyForm : UserControl
    {
        private Company C;
        public CompanyForm(int id = -1)
        {
            InitializeComponent();

            if (id!=-1)
            {
                button.Content = "Update";
                title.Title = "Edit Company";
                C = CompanyDL.GetCompany(id); 
            }
            else
            {
                button.Content = "Add";
                title.Title = "Add Company";
                C = new Company();
            }
            DataContext = C;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (C.Id != -1)
            {

            }
            else
            {
                Company C = new Company(company_name.Text.ToString());
                CompanyDL.InsertCompany(C);
                ((Border)Parent).Child = new UI.ManageCompanies();
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.ManageCompanies();
        }
    }
}
