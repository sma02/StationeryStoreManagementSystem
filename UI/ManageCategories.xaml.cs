﻿using StationeryStoreManagementSystem.DL;
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
    /// Interaction logic for ManageCategories.xaml
    /// </summary>
    public partial class ManageCategories : UserControl
    {
        public ManageCategories()
        {
            InitializeComponent();
            var categories = CategoryDL.GetCategories();
            dg_categories.ItemsSource = categories;
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.CategoryForm();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            ((Border)Parent).Child = new UI.CategoryForm();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}