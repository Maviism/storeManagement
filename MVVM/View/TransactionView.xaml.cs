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
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");
        string queryString = "SELECT * FROM Products WHERE Product_name = '";

        //TODO moving all method to viewModel
        public void refreshData()
        {
            ListTransaction.Items.Refresh();
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

        private void ReadSingleRow(IDataRecord dataRecord)
        {
            int ProductNo = Convert.ToInt32(dataRecord[0]);
            string ProductName = Convert.ToString(dataRecord[1]);
            int Price = Convert.ToInt32(dataRecord[3]);
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
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //https://learn.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqldatareader?view=dotnet-plat-ext-6.0

    }
}
