using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using storeManagement.MVVM.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class TransactionView : UserControl
    {
        List<TransactionModel> transaction = new List<TransactionModel>();

        private int _totalAmount = 000;

        public TransactionView()
        {
            InitializeComponent();
            ListTransaction.ItemsSource = transaction;
            TotalAmount.Content = _totalAmount;
            getLastTransactionId();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");
        string queryString = "SELECT * FROM Products WHERE Product_name = '";

        //TODO moving all method to viewModel
        public void refreshData()
        {
            ListTransaction.Items.Refresh();
            TotalAmount.Content = _totalAmount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addListTransaction(SelectedProduct.Text);
            refreshData();
        }


        private void addListTransaction(string name)
        {
            SqlCommand command = new SqlCommand(queryString + name + "'", conn);
            conn.Open();

            SqlDataReader reader = command.ExecuteReader(); 
            while(reader.Read()) // is possible just get single data without looping?
            {
                ReadSingleRow((IDataRecord)reader);                
            }

            reader.Close();
            conn.Close();
        }

        //https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-6.0
        private void ReadSingleRow(IDataRecord dataRecord)
        {
            int ProductNo = Convert.ToInt32(dataRecord[1]);
            string ProductName = Convert.ToString(dataRecord[2]);
            int Price = Convert.ToInt32(dataRecord[4]);
            int Qty = Convert.ToInt32(NumberTextBox.Text);
            int TotalPrice = Convert.ToInt32(NumberTextBox.Text) * Price ;

            transaction.Add(new TransactionModel( ProductNo, ProductName, Qty , Price, TotalPrice));

            UpdateTotalAmount(TotalPrice);
        }

        private void UpdateTotalAmount(int value)
        {
            _totalAmount += value;
            TotalAmount.Content = _totalAmount;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int convertInt(IDataRecord dataRecord)
        {
            int Transaction_no = Convert.ToInt32(dataRecord[0]);
            return Transaction_no;
        }

        private int getLastTransactionId()
        {
            string strQuery = "SELECT TOP 1 * FROM Transactions ORDER BY Transaction_id DESC";
            int Transaction_no = 1;
            SqlCommand command = new SqlCommand(strQuery, conn);
            conn.Open();
            SqlDataReader reader= command.ExecuteReader();
            while(reader.Read())
            {
                Transaction_no = convertInt((IDataRecord)reader) + 1;
            }
            reader.Close();
            conn.Close();

            return Transaction_no;
        }

        private void PaymentBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO get last transaction_id
            int transaction_id = getLastTransactionId();
            //TODO add transaction
            insertTransactionToDB(transaction_id, _totalAmount);

            foreach(var trs in transaction)
            {
                //TODO add detail transaction
                Trace.WriteLine(trs.Name + trs.Price);
                insertDetailTransactionToDB(transaction_id, trs.Name, trs.Quantity, trs.TotalPrice);
            }
            MessageBox.Show("Transaction succesfully");
            transaction.Clear();
            _totalAmount = 000;
            refreshData();
        }


        public void insertTransactionToDB(int transaction_id, decimal price)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Transactions(Transaction_id, Total_price) VALUES (@Transaction_id, @Total_price)", conn);
            cmd.Parameters.AddWithValue("@Transaction_id", transaction_id);
            cmd.Parameters.AddWithValue("@Total_price", price);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

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
