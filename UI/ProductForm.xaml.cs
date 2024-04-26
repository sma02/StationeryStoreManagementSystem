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
        public ProductForm(ManageEntity callingInstance,int id = -1):base(callingInstance)
        {
            InitializeComponent();
            if(id!=-1)
            {
                product = ProductDL.GetProduct(id);
            }
            CompanyField.ItemSource = CompanyDL.GetCompanies();
            CompanyField.DisplayPathName = "Name";
            CategoryField.ItemSource = CategoryDL.GetCategories();
            CategoryField.DisplayPathName = "Name";
        }
    }
}
