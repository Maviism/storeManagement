using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using storeManagement.Core;

namespace storeManagement.MVVM.Model
{
    internal class ProductModel
    {
        SqlConnection conn = new DBHelper().Connection;

        public DataTable getAllProduct()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            return dt;
        }

        public void deleteProduct(object Product_no)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Products Where Product_no = " + Product_no, conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Success delete");
                conn.Close();
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

        public void getSingleData(int Product_no)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE Product_no = " + Product_no, conn);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                //ShowProduct((IDataRecord)reader);
            }

            reader.Close();
            conn.Close();
        }

        //public void ConvertFromDbToVariable(IDataRecord dataRecord)
        //{
        //    Trace.WriteLine(String.Format("{0}, {1}", dataRecord[0], dataRecord[1]));
        //    Product_noInput.Text = Convert.ToString(dataRecord[0]);
        //    Product_nameInput.Text = Convert.ToString(dataRecord[1]);
        //    Product_qtyInput.Text = Convert.ToString(dataRecord[2]);
        //    Product_priceInput.Text = Convert.ToString(dataRecord[3]);

        //}

        public void editProduct()
        {
            conn.Open();

            // SqlCommand cmd = new SqlCommand("UPDATE Products SET Product_name = '" + Product_nameInput.Text + "', Price = " + Product_priceInput.Text + ", Quantity = " + Product_qtyInput.Text + " WHERE Product_no = " + Product_noInput.Text, conn);
            try
            {
                //cmd.ExecuteNonQuery();
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
