using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class StockHistoryModel
    {
        int quantity;
        int productNo;

        private SqlConnection conn = new DBHelper().Connection;

        public DataTable getAllStockHistory()
        {
            SqlCommand cmd = new SqlCommand("SELECT Stock_history.* , Products.Product_name FROM Stock_history INNER JOIN Products ON Stock_history.Product_no = Products.Product_no", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }
        public void ConvertingProduct(IDataRecord dataRecord)
        {
            productNo = Convert.ToInt32(dataRecord[1]);
            quantity = Convert.ToInt32(dataRecord[2]);
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
        public void getSingleStokHistory(int stockHistoryId)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Stock_history WHERE Id = " + stockHistoryId, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ConvertingProduct((IDataRecord)reader);
            }

            reader.Close();
        }
        public void deleteStockHistory(int stockHistoryId)
        {
            conn.Open();
            getSingleStokHistory(stockHistoryId);
            SqlCommand cmd = new SqlCommand("DELETE FROM Stock_history Where Id = " + stockHistoryId, conn);
            try
            {
                cmd.ExecuteNonQuery();
                new ProductModel().updateStockProduct(productNo.ToString(), quantity, 0);
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
