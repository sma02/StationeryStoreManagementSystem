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
    /// Interaction logic for SupplierForm.xaml
    /// </summary>
    public partial class SupplierForm : AbstractEntryForm
    {
        private Supplier supplier;
        public SupplierForm(ManageEntity callingInstance, int id=-1):base(callingInstance)
        {
            InitializeComponent();
            CountryField.Items = new List<string>()
            {
                "Pakistan"
            };
            CityField.Items = new List<string>()
            {
                "Islamabad",
                "Karachi",
                "Lahore"
            };
            if (id != -1)
            {
                titleBlock.Text = "Edit Supplier";
                ConfirmButton.Content = "Update";
                supplier = SupplierDL.GetSupplier(id);
                isEdit = true;
            }
            else
            {
                supplier = new Supplier();
            }
            DataContext = supplier;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            supplier.Save(!isEdit);
            NavigateCallingForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }
    }
}
