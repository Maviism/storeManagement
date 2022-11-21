using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class DetailTransactionModel
    {

        SqlConnection conn = new DBHelper().Connection;
        ProductModel product = new ProductModel();
        public void insertDetailTransactionToDB(int transaction_id, string product_name, int quantity, decimal price)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Detail_transaction(Transaction_id, Product_name, Quantity, Total_price) VALUES (@Transaction_id, @Product_name, @Quantity ,@Total_price)", conn);
            cmd.Parameters.AddWithValue("@Transaction_id", transaction_id);
            cmd.Parameters.AddWithValue("@Product_name", product_name);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Total_price", price);
            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                product.updateStockProduct(product_name, quantity);

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

        public DataTable getAllDetailTransaction()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Transactions", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public DataTable getDetailTransactionWithTransactionID(int transactionID)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Detail_transaction WHERE Transaction_id = " + transactionID, conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

    }
}
