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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            ThemeCheckbox.IsChecked = GlobalSettings.CurrentTheme == GlobalSettings.Theme.Light;
        }

        private void ThemeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.CurrentTheme = GlobalSettings.Theme.Light;
        }

        private void ThemeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.CurrentTheme = GlobalSettings.Theme.Dark;
        }

        private void DisplayIdsCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.DisplayIds = true;
        }

        private void DisplayIdsCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.DisplayIds = false;
        }
    }
}
