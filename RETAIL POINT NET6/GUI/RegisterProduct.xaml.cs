using RETAIL_POINT_NET6.Clases;
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
using System.Windows.Shapes;

namespace RETAIL_POINT_NET6.GUI
{
    /// <summary>
    /// Interaction logic for RegisterProduct.xaml
    /// </summary>
    public partial class RegisterProduct : Window
    {
        string _IdScanned = string.Empty;

        public RegisterProduct(string IdScanned)
        {
            
            InitializeComponent();
            
            _IdScanned = IdScanned;
            txtId.Text = _IdScanned;          
            txtName.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter && !string.IsNullOrEmpty(txtName.Text)) 
            {
                txtName.IsEnabled = false;
                txtDescription.IsEnabled = true;
                txtDescription.Focus();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(txtDescription.Text))
            {
                txtDescription.IsEnabled = false;
                txtPrice.IsEnabled = true;
                txtPrice.Focus();
            }
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(txtPrice.Text))
            {
                txtPrice.IsEnabled = false;
                btnRegister.IsEnabled = true;
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {          
            int Result = SQLFunctions.InsertProduct(txtId.Text, txtName.Text, txtDescription.Text, txtPrice.Text);
            DialogResult = true;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;   
        }
    }
}
