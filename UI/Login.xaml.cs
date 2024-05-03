using Microsoft.Data.SqlClient;
using StationeryStoreManagementSystem;
using System;
using System.Data;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using StationeryStoreManagementSystem.DL;

namespace StationeryStoreManagementSystem.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        string username;
        string password;
        public Login()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            username = username_tb.Text;
            password = password_tb.Text;

            if (IsValid() != null)
            {
                ((Border)Parent).Child = null;
                Utils.CurrentEmployee = EmployeeDL.GetEmployee(IsValid());
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
        }
        public int IsValid()
        {
            Utils.CloseReader();
            var conn = Configuration.getInstance().getConnection();
            string query = "EXEC CheckCredentials @Username, @Password, @UserId OUTPUT";
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Output;
            command.ExecuteNonQuery();
            int Id = Convert.ToInt32(command.Parameters["@UserId"].Value);
            return Id;
        }
    }
}
