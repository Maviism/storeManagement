using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using storeManagement.Core;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for InputModalView.xaml
    /// </summary>
    public partial class InputModalView : Window
    {
        public InputModalView()
        {
            InitializeComponent();
        }

        SqlConnection conn = new DBHelper().Connection;

        public bool isValid()
        {
            if (Product_noInput.Text == string.Empty)
            {
                MessageBox.Show("Product No is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (Product_nameInput.Text == string.Empty)
            {
                MessageBox.Show("Product name is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (Product_qtyInput.Text == string.Empty)
            {
                MessageBox.Show("Quantity is required", "Failed", MessageBoxButton.OK);
                return false;
            }
            if (Product_priceInput.Text == string.Empty)
            {
                MessageBox.Show("Product price is required", "Failed", MessageBoxButton.OK);
                return false;
            }

            return true;
        }

        public void clearForm()
        {
            Product_noInput.Clear();
            Product_nameInput.Clear();
            Product_qtyInput.Clear();
            Product_priceInput.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Products(Product_no, Product_name, Quantity, Price) VALUES (@Product_no, @Product_name, @Quantity, @Price)", conn);
                    cmd.Parameters.AddWithValue("@Product_no", Product_noInput.Text);
                    cmd.Parameters.AddWithValue("@Product_name", Product_nameInput.Text);
                    cmd.Parameters.AddWithValue("@Quantity", Product_qtyInput.Text);
                    cmd.Parameters.AddWithValue("@Price", Product_priceInput.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    clearForm();
                    MessageBox.Show("Successfully registerd");
                }
            }
            catch (SqlException ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
