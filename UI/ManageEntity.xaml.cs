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

        private Type entity;
        private Type entryForm;
        private Delegate? deleteFunc;
        public ManageEntity(string title,Type entity,DataTable table,List<(string,string)> bindings,List<string>? searchAttributes,Type? entryForm,bool isEdit, Delegate? deleteFunc = null)
        {
            InitializeComponent();
            this.entity = entity;
            TitleBlock.Title = title;
            Addbutton.Content = $"Add {entity.Name}";
            for(int i = bindings.Count-1;i>=0;i--)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = bindings[i].Item1;
                column.Binding = new Binding(bindings[i].Item2);
                datagrid1.Columns.Insert(0, column);
            }
            this.deleteFunc = deleteFunc;
            this.entryForm = entryForm;
            if (isEdit ==true && deleteFunc != null)
                EditDeleteColumn.Visibility = Visibility.Visible;
            else if (isEdit == true)
                EditColumn.Visibility = Visibility.Visible;
            else if (deleteFunc != null)
                DeleteColumn.Visibility = Visibility.Visible;
            datagrid1.ItemsSource = table.DefaultView;
            searchBar.SearchAttributes = searchAttributes;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = (UIElement)Activator.CreateInstance(entryForm, new object[] { -1});
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)datagrid1.ItemsSource).Table;
            int id = (int)table.DefaultView[datagrid1.SelectedIndex].Row.ItemArray[0];
            ((Border)Parent).Child = (UIElement)Activator.CreateInstance(entryForm,new object[] { id });
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = ((DataView)datagrid1.ItemsSource).Table;
            object[] id = { (int)table.DefaultView[datagrid1.SelectedIndex].Row.ItemArray[0] };
            deleteFunc.DynamicInvoke(id);
        }

        private void SearchBar_SearchRequested(object sender, EventArgs e)
        {
            string filterString = searchBar.FilterString;
            ((DataView)datagrid1.ItemsSource).RowFilter = filterString;
        }
    }
}
