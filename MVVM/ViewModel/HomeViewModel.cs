using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.ViewModel
{
    internal class HomeViewModel:ObservableObject
    {
        private int _totalTransaction;
        public int TotalTransaction 
        { 
            get { return _totalTransaction; } 
            set { 
                _totalTransaction = value; 
                OnPropertyChanged();
            } 
        }
        public decimal TotalAmount { get; set; }

        SqlConnection conn = new DBHelper().Connection;
        public int getTotalTransaction()
        {
            int totalTransaction = 0;
            //SELECT * FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%2022-10-27%'

            string date = DateTime.Today.ToString("yyyy-MM-dd");
            string query = "SELECT * FROM Transactions WHERE CONVERT(VARCHAR(25), Created_at,126) LIKE '%" + date +"%' ";//Like date now
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                totalTransaction++;
            }
            reader.Close();
            conn.Close();
            return totalTransaction;
        }

        public HomeViewModel()
        {
            TotalTransaction = getTotalTransaction();
        }
    }
}
