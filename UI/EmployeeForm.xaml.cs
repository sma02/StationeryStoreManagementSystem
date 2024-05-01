using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem.BL;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for EmployeeForm.xaml
    /// </summary>
    public partial class EmployeeForm : AbstractEntryForm
    {
        private Cashier cashier;
        public EmployeeForm(ManageEntity callingInstance, int id = -1) : base(callingInstance)
        {
            InitializeComponent();
            gender_cb.ItemSource = DataHandler.LookupData("Gender");
            gender_cb.DisplayPathName = "Value";
            role_cb.ItemSource = DataHandler.LookupData("Role");
            role_cb.DisplayPathName = "Value";
            city_cb.ItemSource = DataHandler.LookupData("CityPakistan");
            city_cb.DisplayPathName = "Value";
            if (id != -1)
            {
                cell1.Visibility = Visibility.Collapsed;
                cell2.Visibility = Visibility.Collapsed;
                ConfirmButton.Content = "Update";
                titleBlock.Title = "Edit Employee";
                cashier = (Cashier)EmployeeDL.GetEmployee(id);
                gender_cb.SelectedItem = ((Dictionary<int, string>)gender_cb.ItemSource).Where(x => x.Value == cashier.Gender).FirstOrDefault();
                role_cb.SelectedItem = DataHandler.LookupData("Role").Where(x => x.Value == cashier.DetermineRole(cashier)).FirstOrDefault();
                cashier.Id = id;
            }
            else
            {
                ResetButton.Visibility = Visibility.Collapsed;
                ConfirmButton.Content = "Add";
                titleBlock.Title = "Add Employee";
                cashier = new Cashier();
            }
            DataContext = cashier;
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (cashier.Id != -1)
            {
                cashier.Save(false);
            }
            else
            {
                cashier.Save(true);
            }
            NavigateCallingForm();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.CloseReader();
            var conn = Configuration.getInstance().getConnection();
            string query = "EXEC ResetPassword @UserId, @NewPassword OUTPUT";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@UserId", cashier.Id);
            command.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 5).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            string newPassword = command.Parameters["@NewPassword"].Value.ToString();
            MessageBox.Show("Password Reset to: " + newPassword);
        }
    }
}
