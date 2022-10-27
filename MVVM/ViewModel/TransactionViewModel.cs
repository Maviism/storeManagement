using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using storeManagement.Core;
using System.Windows;
using storeManagement.MVVM.Model;

namespace storeManagement.MVVM.ViewModel
{
    internal class TransactionViewModel : ObservableObject
    {
        public RelayCommand AddItemBtnCommand { get; set; }

        

        private DataTable _products;

        public DataTable Transaction;
        public DataTable Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        private string selectedItem;
        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }



        public void testMethod()
        {
            MessageBox.Show("viewmodel method");
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");

        public DataTable LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products ORDER BY Product_name ASC", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public void addTransation()
        {
        }

        public TransactionViewModel()
        {
            Products = LoadGrid();

            AddItemBtnCommand = new RelayCommand(o =>
            {
                MessageBox.Show(selectedItem);
            });
        }



    }
}
