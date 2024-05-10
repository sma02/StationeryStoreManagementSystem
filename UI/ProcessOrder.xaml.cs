using StationeryStoreManagementSystem.BL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
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
using System.Windows.Threading;
using WPFMediaKit;
using WPFMediaKit.DirectShow.Controls;
using ZXing;
using ZXing.Windows.Compatibility;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for ProcessOrder.xaml
    /// </summary>
    public partial class ProcessOrder : UserControl
    {
        DispatcherTimer cameraTimer = new DispatcherTimer();
        BarcodeReader codeReader = new BarcodeReader();
        Order order;
        int cooldown = 0;
        public ProcessOrder()
        {
            InitializeComponent();
           /*var writer = new BarcodeWriter
            { 
                Format = BarcodeFormat.CODE_128,
                Options = { Width = 400, Height = 100, Margin = 4 },
            };
            var barcodeImage = writer.Write("E0001S01");
            barcodeImage.Save("E0001S01.png");*/
            vce.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
              cameraTimer.IsEnabled = true;
              cameraTimer.Interval = new TimeSpan(200);  
              cameraTimer.Tick += CameraTimer_Tick;

            order = new Order();
            List<(string, string, bool)> bindings = new List<(string, string, bool)>
            {
                    ("Product Code","Code",true),
                    ("Product Name","Product.Name",true),
                    ("Company Name","Product.Company.Name",true),
                    ("Category", "Product.Category.Name", true),
                    ("Price", "UnitPrice", true),
                    ("Discount", "Discount", true),
                    ("Total", "TotalPrice", true),
                    ("Q.ty", "Quantity", false),
            };
            for (int i = bindings.Count - 1; i >= 0; i--)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = bindings[i].Item1;
                column.Binding = new Binding(bindings[i].Item2) { UpdateSourceTrigger=UpdateSourceTrigger.LostFocus};
                column.IsReadOnly = bindings[i].Item3;
                ProductDataGrid.Columns.Insert(0, column);
            }
            ProductDataGrid.AutoGenerateColumns = false;
            ProductDataGrid.ItemsSource = order.Products;
            ProductDataGrid.CanUserAddRows = false;

            DataContext = order;
        }

        private void CameraTimer_Tick(object? sender, EventArgs e)
        {
            if (cooldown > 0)
            {
                cooldown--;
                return;
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)vce.ActualWidth, (int)vce.ActualHeight, 96, 96, PixelFormats.Default);
            vce.Measure(vce.RenderSize);
            vce.Arrange(new Rect(vce.RenderSize));
            bmp.Render(vce);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                Bitmap btiMap = new Bitmap(ms);
                var result = codeReader.Decode(btiMap);
                if (result != null)
                {
                    order.AddProduct(result.ToString(), 1);
                    cooldown = 5;
                    refreshData();
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ProductDataGrid.SelectedIndex;
            order.RemoveProduct(selectedIndex);
            refreshData();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            order.AddProduct(productIdField.Text, int.Parse(quantityField.Text));
            refreshData();
        }
                
        private void ProductDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            refreshData();
            
        }
        private void refreshData()
        {
            ProductDataGrid.ItemsSource = null;
            ProductDataGrid.ItemsSource = order.Products;
            totalLabel.TextData = order.GrandTotal.ToString();
            savedLabel.TextData = order.SavedTotal.ToString();
        }
    }
}
