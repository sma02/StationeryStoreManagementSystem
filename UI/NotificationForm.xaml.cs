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
    /// Interaction logic for NotificationForm.xaml
    /// </summary>
    public partial class NotificationForm : AbstractEntryForm
    {
        public NotificationForm(ManageEntity callingInstance, int id = -1) : base(callingInstance)
        {
            InitializeComponent();
            List<Cashier> employees = EmployeeDL.GetEmployees();
            employees.Insert(0, new Cashier {FirstName = "All",LastName="Employees"});
            EmployeeField.ItemSource = employees;
            EmployeeField.DisplayPathName = "Name";
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }
    }
}
