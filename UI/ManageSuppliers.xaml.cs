using StationeryStoreManagementSystem.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Xml.Linq;

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
            //SupplierDL.GetSuppliersView();
            var suppliers = SupplierDL.GetSuppliersView();
            datagrid1.ItemsSource = suppliers.DefaultView;
            searchBar.SearchAttributes = new List<string>() { "Name" };
        }
        private void SearchBar_SearchRequested(object sender, EventArgs e)
        {
            string filterString = searchBar.FilterString;
            ((DataView)datagrid1.ItemsSource).RowFilter = filterString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new SupplierForm();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)datagrid1.ItemsSource).Table;
            int id = (int)table.DefaultView[datagrid1.SelectedIndex].Row.ItemArray[0];
            ((Border)Parent).Child = new SupplierForm(id);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)datagrid1.ItemsSource).Table;
            int id = (int)table.DefaultView[datagrid1.SelectedIndex].Row.ItemArray[0];
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                SupplierDL.DeleteSupplier(id);
            }
            ((Border)Parent).Child = new UI.ManageSuppliers();
        }
    }
}
