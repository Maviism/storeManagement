using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using storeManagement.Core;

namespace storeManagement.MVVM.ViewModel
{
    internal class ManageStockViewModel : ObservableObject
    {

        public RelayCommand ButtonTest { get; set; }
        public RelayCommand DeleteDataBtnCommand { get; set; }

        public RelayCommand RefreshDataCommand { get; set; }

        private DataTable _products;
        public DataTable Products 
        { 
            get { return _products; } 
            set { 
                _products = value;
                OnPropertyChanged();
            }
        }

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");

        public DataTable LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }


        private void DeleteData(object message)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Products Where Product_no = " + message, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success delete");
                conn.Close();
                Products = LoadGrid();
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

        public ManageStockViewModel()
        {
            Products = LoadGrid();

            RefreshDataCommand = new RelayCommand(o =>
            {
                Products = LoadGrid();
            });

            DeleteDataBtnCommand = new RelayCommand(o =>
            {
                DeleteData(o);
            });
            ButtonTest = new RelayCommand(o =>
            {
                MessageBox.Show("Hello");
            });
        }
    }
}
