
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using RETAIL_POINT_NET6.Clases;

namespace RETAIL_POINT_NET6
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        DataTable UsersDt;


        public Login()
        {
            InitializeComponent();

            bool keyFile = ValidateKeyApp.ValidateKeyFile();
            if (!keyFile) Environment.Exit(0);

            GetUsers();
        }


        void GetUsers()
        {
            Tuple<int, DataTable> UsersDB = SQLFunctions.QueryUsers();
            UsersDt = UsersDB.Item2;

            foreach (DataRow Row in UsersDt.Rows)
            {
                cBoxUsers.Items.Add(Row[0].ToString());
            }


        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(cBoxUsers.Text))
            {
                Tuple<int, DataTable> result = SQLFunctions.QueryPasswordByUser(cBoxUsers.Text);


                string? Password = result.Item2.Rows[0]["Password"] as string;

                if (Password == txtPassword.Text) 
                {
                    new MainWindow(cBoxUsers.Text).Show();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("User or password wrong...");
                    txtPassword.Clear();
                }

                  

            
                
            }
        }


        #region PlaceHolder TextBox Query

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            RemoveText(sender, e);
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            AddText(sender, e);
        }

        void RemoveText(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Escribe tu contraseña...") txtPassword.Text = "";
        }

        void AddText(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text)) txtPassword.Text = "Escribe tu contraseña...";
        }


        #endregion

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}

