using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for CategoryForm.xaml
    /// </summary>
    public partial class CategoryForm : AbstractEntryForm
    {
        private Category C;
        public CategoryForm(ManageEntity callingInstance,int id = -1):base(callingInstance)
        {
            InitializeComponent();

            if (id != -1)
            {
                button.Content = "Update";
                title.Title = "Edit Category";
                C = CategoryDL.GetCategory(id);
            }
            else
            {
                button.Content = "Add";
                title.Title = "Add Category";
                C = new Category();
            }
            C.Id = id;
            ME = entity;
            DataContext = C;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (C.Id != -1)
            {
                C.Save(false);
            }
            else
            {
                C.Save(true);
            }
            NavigateCallingForm();
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigateCallingForm();
        }
    }
}
