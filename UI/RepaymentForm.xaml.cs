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
    /// Interaction logic for RepaymentForm.xaml
    /// </summary>
    public partial class RepaymentForm : AbstractEntryForm
    {
        public RepaymentForm(ManageEntity callingInstance, int id = -1) : base(callingInstance)
        {
            InitializeComponent();
        }
    }
}
