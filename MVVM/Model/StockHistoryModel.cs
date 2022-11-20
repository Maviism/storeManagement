using System.Data;
using System.Data.SqlClient;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class StockHistoryModel
    {

        private SqlConnection conn = new DBHelper().Connection;

        int ProductNo;
        int Quantity;

        public DataTable getAllStockHistory()
        {
            SqlCommand cmd = new SqlCommand("SELECT Stock_history.Id ,Stock_history.Product_no, Products.Product_name, Stock_history.Quantity, Stock_history.Created_at FROM Stock_history INNER JOIN Products ON Stock_history.Product_no = Products.Product_no", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public void insertStockHistory(int productNo, int qty)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Stock_history(Product_no, Quantity) VALUES (@Product_no, @Quantity)", conn);
            cmd.Parameters.AddWithValue("@Product_no", productNo);
            cmd.Parameters.AddWithValue("@Quantity", qty);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Update stock successfully");
        }

        public void deleteStockHistory(string productId)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Stock_history Where Id = " + productId, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show(productId);
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

    }
}
