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
    /// Interaction logic for ManageSuppliers.xaml
    /// </summary>
    public partial class ManageSuppliers : UserControl
    {
        public ManageSuppliers()
        {
            InitializeComponent();
            var suppliers = SupplierDL.GetSuppliers();
            datagrid1.ItemsSource = suppliers;
        }
    }
}
