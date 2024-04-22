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
    public partial class SupplierForm : UserControl
    {
        private Supplier supplier;
        public SupplierForm(int id=-1)
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
            }
            else
            {
                supplier = new Supplier();
            }
            DataContext = supplier;
        }
    }
}
