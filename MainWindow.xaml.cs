using StationeryStoreManagementSystem.BL;
using StationeryStoreManagementSystem.DL;
using StationeryStoreManagementSystem.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace StationeryStoreManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool currentTheme = true;
        private List<Button> AdminBtns = new List<Button>()
        {
            new Button() { Content = "Manage Suppliers" },
            new Button() { Content = "Manage Companies" },
            new Button() { Content = "Manage Categories" },
            new Button() { Content = "Manage Employees" },
            new Button() { Content = "Manage Products" },
            new Button() { Content = "Manage Shipments" },
            new Button() { Content = "Manage Customers" },
            new Button() { Content = "Manage Notifications" }
        };
        public MainWindow()
        {
            InitializeComponent();
            /*sideBar.Visibility = Visibility.Collapsed;
            Content.Child = new Login();*/
        }
        public void changeTheme(bool theme)
        {
            Collection<ResourceDictionary> dictionary = Resources.MergedDictionaries;
            dictionary.Clear();
            string path;
            if (theme == true)
                path = "/UI/Themes/DarkTheme.xaml";
            else
                path = "/UI/Themes/LightTheme.xaml";
            dictionary.Add(new ResourceDictionary() { Source = new Uri(path, UriKind.RelativeOrAbsolute) });
            currentTheme = theme;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeTheme(!currentTheme);
        }

        private void ManageSuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            // Content.Child = new UI.ManageSuppliers();
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Name","Name"),
                ("Contact","Contact"),
                ("Email","Email"),
                ("Street Address","StreetAddress"),
                ("Town","Town"),
                ("City","City"),
                ("Country","Country"),
                ("Postal Code","PostalCode")
             };
            Content.Child = new UI.ManageEntity("Manage Suppliers",
                                                typeof(Supplier).Name,
                                                SupplierDL.GetSuppliersView,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(SupplierForm),
                                                true,
                                                true,
                                                SupplierDL.DeleteSupplier);
        }

        private void ManageCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            // Content.Child = new UI.ManageCompanies();
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Company Name","Name")
             };
            Content.Child = new UI.ManageEntity("Manage Companies",
                                                typeof(Company).Name,
                                                CompanyDL.GetCompanies_View,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(CompanyForm),
                                                true,
                                                true,
                                                CompanyDL.DeleteCompany);
        }

        private void ManageCategoriesButton_Click(Object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Name","Name"),
                ("GST", "GST")
             };
            Content.Child = new UI.ManageEntity("Manage Categories",
                                                typeof(Category).Name,
                                                CategoryDL.GetCategories_View,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(CategoryForm),
                                                true,
                                                true,
                                                CategoryDL.DeleteCategory);
        }

        private void ManageEmployeesButton_Click(Object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Username","Username"),
                ("Name","Name"),
                ("CNIC","CNIC"),
                ("Contact","Contact"),
                ("Role","Role"),
                ("Gender","Gender")
            };
            Content.Child = new UI.ManageEntity("Manage Employees",
                                                typeof(Employee).Name,
                                                EmployeeDL.GetEmployeessView,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(EmployeeForm),
                                                true,
                                                true,
                                                EmployeeDL.DeleteEmployee);
        }

        private void ManageProductButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Name","Name"),
                ("Price","Price"),
                ("Retail Rs","RetailPrice"),
                ("Discount Rs","DiscountAmount"),
                ("Company","Company"),
                ("Category","Category"),
                ("No. Suppliers","[No. Suppliers]"),
                ("Q.ty","Stock")
             };
            Content.Child = new UI.ManageEntity("Manage Products",
                                                typeof(Product).Name,
                                                ProductDL.GetProducts_View,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(ProductForm),
                                                true,
                                                true,
                                                ProductDL.DeleteProduct);
        }

        private void ManageShipmentsButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("Name","Name"),
                ("Timestamp","AddedOn"),
             };
            Content.Child = new UI.ManageEntity("Manage Shipments",
                                                "Shipment",
                                                ShipmentDL.GetShipmentsView,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(ShipmentForm),
                                                true,
                                                false,
                                                null,
                                                typeof(ViewShipment));
        }
        private void ManageCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("CNIC","CNIC"),
                ("Name","Name"),
                ("Contact","Contact"),
                ("Gender","Gender")
            };
            Content.Child = new UI.ManageEntity("Manage Customers",
                                                typeof(Customer).Name,
                                                CustomerDL.GetCustomersView,
                                                bindings,
                                                new List<string> { "Name" },
                                                typeof(CustomerForm),
                                                true,
                                                true);
        }
        private void ManageNotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string, string)> bindings = new List<(string, string)>
            {
                ("From","From"),
                ("IsViewed","IsViewed"),
                ("Notification","Notification")
            };
            Content.Child = new UI.ManageEntity("Manage Notifications",
                                                typeof(Notification).Name,
                                                NotificationDL.GetNotifications_View,
                                                bindings,
                                                new List<string> { "From" },
                                                typeof(NotificationForm),
                                                true,
                                                false,
                                                null,
                                                typeof(ViewNotification));

        }
    }
}
