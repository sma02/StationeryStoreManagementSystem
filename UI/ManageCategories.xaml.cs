using StationeryStoreManagementSystem.DL;
using StationeryStoreManagementSystem.UI.Controls;
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
    /// Interaction logic for ManageCategories.xaml
    /// </summary>
    public partial class ManageCategories : UserControl
    {
        public ManageCategories()
        {
            InitializeComponent();
            var categories = CategoryDL.GetCategories();
            dg_categories.ItemsSource = categories.DefaultView;
            searchBar.SearchAttributes = new List<string>() { "Name" };
        }
        private void SearchBar_SearchRequested(object sender, EventArgs e)
        {
            string filterString = searchBar.FilterString;
            ((DataView)dg_categories.ItemsSource).RowFilter = filterString;
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.CategoryForm();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)dg_categories.ItemsSource).Table;
            int id = (int)table.DefaultView[dg_categories.SelectedIndex].Row.ItemArray[0];
            ((Border)Parent).Child = new UI.CategoryForm(id);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)dg_categories.ItemsSource).Table;
            int id = (int)table.DefaultView[dg_categories.SelectedIndex].Row.ItemArray[0];
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                CategoryDL.DeleteCategory(id);
            }
            ((Border)Parent).Child = new UI.ManageCategories();
        }
    }
}
