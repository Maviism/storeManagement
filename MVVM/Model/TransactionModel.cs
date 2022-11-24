using System;
using System.Data.SqlClient;
using System.Data;
using storeManagement.Core;


namespace storeManagement.MVVM.Model
{
    internal class TransactionModel
    {
        #region Properties
        // how if use inheritance from productmodel?
        public int NoIndex { get; set; }
        public int TransactionId { get; set; }
        public int ProductNo { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DataTable DetailTransaction { get; set; }
        #endregion

        SqlConnection conn = new DBHelper().Connection;
        
        #region Methods
        public DataTable getAllTransaction()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Transactions", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }
        

        public int getLastTransactionId()
        {
            string strQuery = "SELECT TOP 1 * FROM Transactions ORDER BY Transaction_id DESC";
            int Transaction_no = 1; //declaration transaction number; if there is not yet transaction in db
            SqlCommand command = new SqlCommand(strQuery, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Transaction_no = convertInt((IDataRecord)reader) + 1;
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

        private decimal convertDecimal(IDataRecord dataRecord)
        {
            decimal total_price = Convert.ToDecimal(dataRecord[1]);
            return total_price;
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

        public int getTotalTransaction(string value)//format yyyy-MM-dd
        {
            int totalTransaction = 0;

            string date = value;
            string query = "SELECT * FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%" + date + "%' ";//Like date now
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                totalTransaction++;
            }
            reader.Close();
            conn.Close();
            return totalTransaction;
        }

        public decimal getDailyIncome()
        {
            decimal totalDailyIncome = 0;
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            string strQuery = "SELECT * FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%" + date + "%' ";
            int Transaction_no = 1; //declaration transaction number; if there is not yet transaction in db
            SqlCommand command = new SqlCommand(strQuery, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                totalDailyIncome += convertDecimal((IDataRecord)reader);
            }
            reader.Close();
            conn.Close();

            return totalDailyIncome;
        }
        #endregion 

    }
}
