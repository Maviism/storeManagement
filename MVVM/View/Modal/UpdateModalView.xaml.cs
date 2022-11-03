using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for UpdateModalView.xaml
    /// </summary>
    public partial class UpdateModalView : Window
    {
        string ProductNo;

        public UpdateModalView(string Id)
        {
            InitializeComponent();
            ProductNo = Id;
            getProduct();
        }

        // TODO Better to take all of item product from parent without hitting db again

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");

        public void getProduct()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Product_no = " + ProductNo, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                ShowProduct((IDataRecord)reader);
            }

            reader.Close();
            conn.Close();
        }

        public void ShowProduct(IDataRecord dataRecord)
        {
            Trace.WriteLine(String.Format("{0}, {1}", dataRecord[0], dataRecord[1]));
            Product_noInput.Text = Convert.ToString(dataRecord[1]);
            Product_nameInput.Text = Convert.ToString(dataRecord[2]);
            Product_qtyInput.Text = Convert.ToString(dataRecord[3]);
            Product_priceInput.Text = Convert.ToString(dataRecord[4]);

        }

        //TODO rename button function to easy understanding
        //If product number changable let add productid for foreign key
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE Products SET Product_name = '" + Product_nameInput.Text + "', Price = " + Product_priceInput.Text + ", Quantity = "+ Product_qtyInput.Text + " WHERE Product_no = "+ Product_noInput.Text, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data berhasil diubah");
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

    }
}
