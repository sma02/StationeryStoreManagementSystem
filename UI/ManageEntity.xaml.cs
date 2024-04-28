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
    /// Interaction logic for ManageEntity.xaml
    /// </summary>
    public partial class ManageEntity : UserControl
    {

        private Type entryForm;
        private Delegate? deleteFunc;
        private Delegate getTable;
        public ManageEntity(string title, Type entity, Delegate getTable, List<(string, string)> bindings, List<string>? searchAttributes, Type? entryForm, bool isAdd, bool isEdit, Delegate? deleteFunc = null)
        {
            InitializeComponent();
            dataGridView.Addbutton.Content = $"Add {entity.Name}";
            dataGridView.SetBindings(bindings);
            dataGridView.SearchAttributes = searchAttributes;
            TitleBlock.Text = title;
            this.deleteFunc = deleteFunc;
            this.getTable = getTable;
            this.entryForm = entryForm;
            dataGridView.IsAdd = true;
            dataGridView.IsEdit = true;
            dataGridView.IsDelete = true;
            dataGridView.AddButtonClicked += DataGridView_AddButtonClicked;
            dataGridView.EditButtonClicked += DataGridView_EditButtonClicked;
            dataGridView.DeleteButtonClicked += DataGridView_DeleteButtonClicked;
            RefreshData();
        }

        private void DataGridView_DeleteButtonClicked(DataGrid dataGrid, int selectedIndex)
        {
            object[] id = { ((DataView)(dataGrid.ItemsSource))[selectedIndex].Row.ItemArray[0] };
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                deleteFunc.DynamicInvoke(id);
            }
            RefreshData();
        }

        private void DataGridView_EditButtonClicked(DataGrid dataGrid, int selectedIndex)
        {
            int id = (int)((DataView)(dataGrid.ItemsSource))[selectedIndex].Row.ItemArray[0];
            ((Border)Parent).Child = (UIElement)Activator.CreateInstance(entryForm, new object[] { this, id });
        }

        private void DataGridView_AddButtonClicked(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = (UIElement)Activator.CreateInstance(entryForm, new object[] { this, -1 });
        }
        public void RefreshData()
        {
            dataGridView.Refresh((DataTable)getTable.DynamicInvoke());
        }
    }
}
