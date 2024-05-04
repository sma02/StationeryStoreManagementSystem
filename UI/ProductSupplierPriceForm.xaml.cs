using StationeryStoreManagementSystem.BL;
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
    /// Interaction logic for ProductSupplierPriceForm.xaml
    /// </summary>
    public partial class ProductSupplierPriceForm : UserControl
    {
        private Stock stock;
        private ProductForm callingInstance;
        private bool isEdit = true;
        public ProductSupplierPriceForm(ProductForm callingInstance,Product product,int supplierId, string supplierName)
        {
            InitializeComponent();
            this.callingInstance = callingInstance;
            stock = product.Stocks.Where(x => x.Supplier.Id == supplierId).FirstOrDefault();
            if (stock == null)
            {
                stock = new Stock(new Supplier() { Name = supplierName }, product, 0, 0, 0, 0);
                isEdit = false;
            }
            else
                CancelButton.Visibility = Visibility.Collapsed;
            DataContext = stock;
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(isEdit==false) 
            { 
            callingInstance.product.Stocks.Add(stock);
            }
            ((Border)Parent).Child = callingInstance;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = callingInstance;
        }
    }
}
