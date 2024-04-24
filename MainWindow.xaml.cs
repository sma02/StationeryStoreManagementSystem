using StationeryStoreManagementSystem.BL;
using StationeryStoreManagementSystem.DL;
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
        public MainWindow()
        {
            InitializeComponent();
            /*sideBar.Visibility = Visibility.Collapsed;
            Content.Child = new UI.Login();*/
       }
        public void changeTheme(bool theme)
        {
            Collection<ResourceDictionary> dictionary = Resources.MergedDictionaries;
            dictionary.Clear();
            string path;
            if(theme==true)
                path = "/UI/Themes/DarkTheme.xaml";
            else
                path = "/UI/Themes/LightTheme.xaml";
            dictionary.Add(new ResourceDictionary() { Source = new Uri(path,UriKind.RelativeOrAbsolute) });
            currentTheme = theme;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeTheme(!currentTheme);
        }

        private void ManageSuppliersButton_Click(object sender, RoutedEventArgs e)
        {
            Content.Child = new UI.ManageSuppliers();
        }

        private void ManageCompaniesButton_Click(object sender, RoutedEventArgs e)
        {
            Content.Child = new UI.ManageCompanies();
        }

        private void ManageCategoriesButton_Click(Object sender, RoutedEventArgs e)
        {
            Content.Child = new UI.ManageCategories();
        }
    }
}
