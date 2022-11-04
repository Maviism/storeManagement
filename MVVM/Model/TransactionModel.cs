using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using storeManagement.Core;
using System.Diagnostics;

namespace storeManagement.MVVM.Model
{
    internal class TransactionModel
    {
        SqlConnection conn = new DBHelper().Connection;
        public int NoIndex { get; set; }
        public int ProductNo { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public int getLastTransactionId()
        {
            string strQuery = "SELECT TOP 1 * FROM Transactions ORDER BY Transaction_id DESC";
            int Transaction_no = 1;
            SqlCommand command = new SqlCommand(strQuery, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Transaction_no = convertInt((IDataRecord)reader) + 1;
                //Transaction_no = reader.GetInt32(0) + 1;
            }
            reader.Close();
            conn.Close();

            return Transaction_no;
        }

        private int convertInt(IDataRecord dataRecord)
        {
            int Transaction_no = Convert.ToInt32(dataRecord[0]);
            return Transaction_no;
        }

        public void insertTransactionToDB(int transaction_id, decimal price)
        {
            string query = "INSERT INTO Transactions(Transaction_id, Total_price) VALUES (@Transaction_id, @Total_price)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Transaction_id", transaction_id);
            cmd.Parameters.AddWithValue("@Total_price", price);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


    }
}
