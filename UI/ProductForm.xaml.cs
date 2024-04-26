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
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : AbstractEntryForm
    {
        private Product product;
        private bool isEdit = false;
        public ProductForm(ManageEntity callingInstance, int id = -1) : base(callingInstance)
        {
            InitializeComponent();
            if (id != -1)
            {
                product = ProductDL.GetProduct(id);
            }
            var suppliersView = new ManageEntity(null
                               , typeof(Supplier)
                               , SupplierDL.GetSuppliersView
                               , new List<(string, string)>
            {
                ("Name", "Name"),
                ("Contact", "Contact")
            }, null, null, false, false);
            Grid.SetRow(suppliersView, 2);
            SuppliersGrid.Children.Add(suppliersView);
        }
    }
}