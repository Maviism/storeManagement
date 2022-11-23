using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
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
using System.Text.RegularExpressions;
using storeManagement.MVVM.Model;
using storeManagement.Core;

namespace storeManagement.MVVM.View.Modal
{
    /// <summary>
    /// Interaction logic for UpdateStockModal.xaml
    /// </summary>
    public partial class UpdateStockModal : Window
    {
        string ProductNo;
        StockHistoryModel stockHistoryModel = new StockHistoryModel();
        public UpdateStockModal(string productNo)
        {
            InitializeComponent();
            ProductNo = productNo;
            getProduct();
        }

        SqlConnection conn = new DBHelper().Connection;

        public void getProduct()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Product_no = " + ProductNo, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ShowProduct((IDataRecord)reader);
            }

            reader.Close();
            conn.Close();
        }

        public void ShowProduct(IDataRecord dataRecord)
        {
            Product_noInput.Text = Convert.ToString(dataRecord[1]);
            Product_nameInput.Text = Convert.ToString(dataRecord[2]);
            Product_currentInput.Text = Convert.ToString(dataRecord[3]);
        }
        private void Submit_buttonClick(object sender, RoutedEventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity += " + Product_qtyInput.Text + " WHERE Product_no = " + Product_noInput.Text, conn);
            try
            {
                cmd.ExecuteNonQuery();
                stockHistoryModel.insertStockHistory(Convert.ToInt32(Product_noInput.Text),Convert.ToInt32(Product_qtyInput.Text));
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            this.Close();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
