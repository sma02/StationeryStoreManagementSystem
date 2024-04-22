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
    public partial class CategoryForm : UserControl
    {
        private List<object> Args;
        public CategoryForm(List<object> args = null)
        {
            InitializeComponent();
            Args = args;
            if (args != null)
            {
                button.Content = "Update";
                title.Title = "Edit Category";
            }
            else
            {
                button.Content = "Add";
                title.Title = "Add Category";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Args != null)
            {

            }
            else
            {
                Category C = new Category(category_name.Text.ToString(), (double.Parse(category_gst.Text.ToString())));
                CategoryDL.InsertCategory(C);
                ((Border)Parent).Child = new UI.ManageCategories();
            }
        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.ManageCategories();
        }
    }
}
