using System.Data;
using System.Data.SqlClient;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class ProductModel
    {

        private int _productNo;
        private string _productName;
        private decimal _price;
        private int _qty;

        public int ProductNo
        {
            get { return _productNo; }
            set { _productNo = value; }
        }
        public string ProductName 
        { 
            get { return _productName; }
            set { _productName = value; }
        }
        public decimal Price 
        {
            get { return _price; }
            set { _price = value; } 
        }
        public int Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }

        private SqlConnection conn = new DBHelper().Connection;

        public DataTable getAllProduct()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public DataTable getOutOfStockProduct()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Quantity < 1", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public void deleteProduct(int Product_no)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Products Where Product_no = " + Product_no, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success delete");
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not deleted " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateStockProduct(string product_name, int quantity)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity -= " + quantity + " WHERE Product_name = '" + product_name + "'", conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void updateStockProduct(string productNo, int quantity, int n)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Products SET Quantity -= " + quantity + " WHERE Product_no = '" + productNo + "'", conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


    }
}
