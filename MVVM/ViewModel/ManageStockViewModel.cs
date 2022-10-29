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
using storeManagement.MVVM.Model;
using storeManagement.MVVM.View;

namespace storeManagement.MVVM.ViewModel
{
    internal class ManageStockViewModel : ObservableObject
    {

        public RelayCommand ButtonTest { get; set; }
        public RelayCommand AddItemBtnCommand { get; set; }
        public RelayCommand DeleteDataBtnCommand { get; set; }
        public RelayCommand UpdateDataBtnCommand { get; set; }
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

        ProductModel product = new ProductModel();

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=StoreManagement;Integrated Security = True;");

        private void UpdateData(object obj)
        {
            string name = obj.ToString();
            UpdateModalView updateModalView = new UpdateModalView(name);
            updateModalView.ShowDialog();
            Products = product.getAllProduct();
        }

        private void OpenInputModal()
        {
            InputModalView inputModal = new InputModalView();
            inputModal.ShowDialog();
            Products = product.getAllProduct();
        }

        public ManageStockViewModel()
        {
            Products = product.getAllProduct();

            RefreshDataCommand = new RelayCommand(o =>
            {
                Products = product.getAllProduct();
            });

            AddItemBtnCommand = new RelayCommand(o =>
            {
                OpenInputModal();
            });

            DeleteDataBtnCommand = new RelayCommand(o =>
            {
                product.deleteProduct(o);
                Products = product.getAllProduct();
            });

            UpdateDataBtnCommand = new RelayCommand(o =>
            {
                UpdateData(o);
            });

            ButtonTest = new RelayCommand(o =>
            {
                MessageBox.Show("Hello");
            });


        }
    }
}
