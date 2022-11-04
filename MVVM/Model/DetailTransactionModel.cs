using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class DetailTransactionModel
    {

        SqlConnection conn = new DBHelper().Connection;
        public void insertDetailTransactionToDB(int transaction_id, string product_name, int quantity, decimal price)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Detail_transaction(Transaction_id, Product_name, Quantity, Total_price) VALUES (@Transaction_id, @Product_name, @Quantity ,@Total_price)", conn);
            cmd.Parameters.AddWithValue("@Transaction_id", transaction_id);
            cmd.Parameters.AddWithValue("@Product_name", product_name);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Total_price", price);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


    }
}
