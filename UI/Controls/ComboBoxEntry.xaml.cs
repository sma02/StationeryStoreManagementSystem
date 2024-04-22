using Microsoft.Data.SqlClient;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StationeryStoreManagementSystem.UI.Controls
{
    /// <summary>
    /// Interaction logic for ComboBoxEntry.xaml
    /// </summary>
    public partial class ComboBoxEntry : UserControl
    {
        public string LabelText
        {
            get => TextBlockLabel.Text;
            set
            {
                TextBlockLabel.Text = value;
            }
        }
        public bool ReadOnly { get; set; }
        public List<string> Items
        {
            get => (List<string>)ComboBox1.ItemsSource;
            set
            {
                ComboBox1.ItemsSource = value;
            }
        }


        public List<string> ItemSource
        {
            get { return (List<string>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource",
                                        typeof(List<string>),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public SqlDataReader ItemsRead
        {
            set
            {
                List<string> items = new List<string>();
                while (value.Read())
                {
                    items.Add(value.GetString(0));
                }
                value.Close();
                Items = items;
            }
        }
        public string SelectedValue
        {
            get { return (string)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue",
                                        typeof(string),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
                                        typeof(object),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public string DisplayPathName
        {
            get { return (string)GetValue(DisplayPathNameProperty); }
            set { SetValue(DisplayPathNameProperty, value); }
        }

        public static readonly DependencyProperty DisplayPathNameProperty =
            DependencyProperty.Register("DisplayPathName",
                                        typeof(string),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string ItemSourcePath
        {
            get { return (string)GetValue(ItemSourcePathProperty); }
            set { SetValue(ItemSourcePathProperty, value); }
        }

        public static readonly DependencyProperty ItemSourcePathProperty =
            DependencyProperty.Register("ItemSourcePath",
                                        typeof(string),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
                                        typeof(string),
                                        typeof(ComboBoxEntry),
                                        new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public ComboBoxEntry()
        {
            InitializeComponent();
        }
    }
}
