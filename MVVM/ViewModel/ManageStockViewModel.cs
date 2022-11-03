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
using storeManagement.MVVM.View.Modal;

namespace storeManagement.MVVM.ViewModel
{
    internal class ManageStockViewModel : ObservableObject
    {

        #region All of RelayCommand
        public RelayCommand ButtonTest { get; set; }
        public RelayCommand AddItemBtnCommand { get; set; }
        public RelayCommand DeleteDataBtnCommand { get; set; }
        public RelayCommand UpdateDataBtnCommand { get; set; }
        public RelayCommand UpdateStockProductCommand { get; set; }
        public RelayCommand RefreshDataCommand { get; set; }
        #endregion

        private DataTable _products;
        ProductModel product = new ProductModel();
        public DataTable Products 
        { 
            get { return _products; } 
            set { 
                _products = value;
                OnPropertyChanged();
            }
        }

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

            UpdateStockProductCommand = new RelayCommand(o =>
            {
                UpdateStockModal updateStock = new UpdateStockModal();
                updateStock.ShowDialog();
                Products = product.getAllProduct();
            });


        }
    }
}
