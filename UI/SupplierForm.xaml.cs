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
            CountryField.ItemSource = DataHandler.LookupData("Country");
            CountryField.DisplayPathName = "Value";
            if (id != -1)
            {
                titleBlock.Text = "Edit Supplier";
                ConfirmButton.Content = "Update";
                supplier = SupplierDL.GetSupplier(id);
                CountryField.SelectedValue = ((Dictionary<int, string>)CountryField.ItemSource).Where(x => x.Value == supplier.Country).Select(x=>x.Value).FirstOrDefault();
                if (CountryField.SelectedValue != null)
                {
                    CityField.ItemSource = DataHandler.LookupData($"City{CountryField.SelectedValue}");
                    CityField.DisplayPathName = "Value";
                    CityField.SelectedValue = ((Dictionary<int, string>)CityField.ItemSource).Where(x => x.Value == supplier.City).Select(x => x.Value).FirstOrDefault();
                }
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

        private void CountryField_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryField.SelectedValue != null)
            {
                CityField.ItemSource = DataHandler.LookupData($"City{CountryField.SelectedValue}");
                CityField.DisplayPathName = "Value";
            }
        }
    }
}
