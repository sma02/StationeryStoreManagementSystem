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

namespace StationeryStoreManagementSystem.UI.Controls
{
    /// <summary>
    /// Interaction logic for TextEntry.xaml
    /// </summary>
    public partial class TextEntry : UserControl
    {
        private int maxLength;
        private string initialData = null;
        public bool ReadOnly
        {
            get => TextBoxText.IsReadOnly;
            set
            {
                TextBoxText.IsReadOnly = value;
            }
        }
        public string LabelText
        {
            get => TextBlockLabel.Text;
            set
            {
                TextBlockLabel.Text = value;
            }
        }
        public object? Text
        {
            get
            {
                return TextBoxText.Text.Trim();
            }
            set
            {
                TextBoxText.Text = (string)value;
            }
        }
        public string InputAttribute { get; set; }
        public int MaxLength
        {
            get => maxLength;
            set
            {
                maxLength = value;
                TextBoxText.MaxLength = value;
            }
        }

        public TextEntry()
        {
            DataContext = this;
            InitializeComponent();
        }

    }
}
