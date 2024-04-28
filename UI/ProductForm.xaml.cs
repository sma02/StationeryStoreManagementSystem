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

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : AbstractEntryForm
    {
        private Product product;
        private List<int> initialSupplierIds;
        private bool isEdit = false;
        public ProductForm(ManageEntity callingInstance, int id = -1) : base(callingInstance)
        {
            InitializeComponent();
            List<Company> companies = CompanyDL.GetCompanies();
            List<Category> categories = CategoryDL.GetCategories();
            CompanyField.ItemSource = companies;
            CompanyField.DisplayPathName = "Name";
            CategoryField.ItemSource = categories;
            CategoryField.DisplayPathName = "Name";
            List<(string, string)> bindings = new List<(string, string)> {
                ("Name","Name"),
                ("Contact","Contact"),
                ("Email","Email"),
                ("Street Address","StreetAddress"),
                ("Town","Town"),
                ("City","City"),
                ("Country","Country"),
                ("Postal Code","PostalCode")};
            List<string> searchAttributes = new List<string>() { "Name" };
            suppliersDataHandler2.SearchAttributes = searchAttributes;
            suppliersDataHandler1.IsDelete = true;
            suppliersDataHandler1.IsEdit = true;
            suppliersDataHandler2.IsSelect = true;
            suppliersDataHandler1.SetBindings(bindings);
            suppliersDataHandler2.SetBindings(bindings);
            DataTable table = SupplierDL.GetSuppliersView();
            DataTable table1 = table.Clone();
            suppliersDataHandler1.ItemSource = table1.DefaultView;
            suppliersDataHandler2.Refresh(table);
            suppliersDataHandler2.SelectButtonClicked += SuppliersDataHandler2_SelectButtonClicked;
            suppliersDataHandler1.DeleteButtonClicked += SuppliersDataHandler1_DeleteButtonClicked; ;
            if (id != -1)
            {
                titleBlock.Text = "Edit Product";
                ConfirmButton.Content = "Update";
                product = ProductDL.GetProduct(id);
                if (product.Company != null)
                    product.Company = companies.Find(x => x.Id == product.Company.Id);
                if (product.Category != null)
                    product.Category = categories.Find(x => x.Id == product.Category.Id);
                initialSupplierIds = product.Suppliers.Select(x => x.Id).ToList();
                List<int> indexes = new List<int>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (initialSupplierIds.Exists(x => x == (int)table.Rows[i].ItemArray[0]))
                    {
                        table1.Rows.Add(table.Rows[i].ItemArray);
                        indexes.Add(i);
                    }
                }
                foreach (int index in indexes)
                    table.Rows.RemoveAt(index);
                isEdit = true;
            }
            else
                product = new Product();

            DataContext = product;
        }

        private void SuppliersDataHandler1_DeleteButtonClicked(DataGrid dataGrid, int selectedIndex)
        {
            DataRow dataRow = ((DataRowView)dataGrid.SelectedItem).Row;
            ((DataView)suppliersDataHandler2.ItemSource).Table.Rows.Add(dataRow.ItemArray);
            ((DataView)suppliersDataHandler1.ItemSource).Table.Rows.Remove(dataRow);
        }

        private void SuppliersDataHandler2_SelectButtonClicked(DataGrid dataGrid, int selectedIndex)
        {
            DataRow dataRow = ((DataRowView)dataGrid.SelectedItem).Row;
            ((DataView)suppliersDataHandler1.ItemSource).Table.Rows.Add(dataRow.ItemArray);
            ((DataView)suppliersDataHandler2.ItemSource).Table.Rows.Remove(dataRow);
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            //  product.Save(!isEdit);
            List<Supplier> suppliers = new List<Supplier>();
          var rows =   ((DataView)suppliersDataHandler1.ItemSource).Table.Rows;
            foreach(DataRow row in rows)
            {
                suppliers.Add(new Supplier((int)row.ItemArray[0]));
            }
            product.Suppliers = suppliers;
            product.Save(!isEdit);
            NavigateCallingForm();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }
    }
}