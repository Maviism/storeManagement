using System;
using System.Data;
using storeManagement.Core;
using storeManagement.MVVM.Model;
using storeManagement.MVVM.View;
using storeManagement.MVVM.View.Modal;

namespace storeManagement.MVVM.ViewModel
{
    internal class ManageStockViewModel : ObservableObject
    {

        #region All of RelayCommand Declaration
        public RelayCommand ButtonTest { get; set; }
        public RelayCommand AddItemBtnCommand { get; set; }
        public RelayCommand DeleteDataBtnCommand { get; set; }
        public RelayCommand UpdateDataBtnCommand { get; set; }
        public RelayCommand UpdateStockProductCommand { get; set; }
        public RelayCommand RefreshDataCommand { get; set; }
        public RelayCommand DeleteStockBtnCommand { get; set; }
        #endregion

        ProductModel product = new ProductModel();
        StockHistoryModel stockHistory = new StockHistoryModel(); 

        private DataTable _products;
        private DataTable _stocks;
        public DataTable Products 
        { 
            get { return _products; } 
            set { 
                _products = value;
                OnPropertyChanged();
            }
        }
        public DataTable StockHistory
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
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
            StockHistory = stockHistory.getAllStockHistory();
        }

        public ManageStockViewModel()
        {
            StockHistory = stockHistory.getAllStockHistory();
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
                product.deleteProduct(Convert.ToInt32(o));
                Products = product.getAllProduct();
            });

            UpdateDataBtnCommand = new RelayCommand(o =>
            {
                UpdateData(o);
            });

            UpdateStockProductCommand = new RelayCommand(o =>
            {
                UpdateStockModal updateStock = new UpdateStockModal(o.ToString());
                updateStock.ShowDialog();
                Products = product.getAllProduct();
                StockHistory = stockHistory.getAllStockHistory();
            });

            DeleteStockBtnCommand = new RelayCommand(o =>
            {
                stockHistory.deleteStockHistory(Convert.ToInt32(o.ToString()));
                StockHistory = stockHistory.getAllStockHistory();
                Products = product.getAllProduct();
            });

        }
    }
}
