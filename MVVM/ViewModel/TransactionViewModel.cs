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
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using storeManagement.MVVM.View.Modal;

namespace storeManagement.MVVM.ViewModel
{
    internal class TransactionViewModel : ObservableObject
    {

        #region Variable Declare
        private decimal _cash;
        private decimal _change;
        private int _productQty = 1;
        private decimal _totalpriceTransaction;
        public int i { get; set; } //indexing new SProduct(_transactionlis) for easy to remove
        private ProductModel _sproduct;
        private ObservableCollection<ProductModel> _productList;
        private ObservableCollection<TransactionModel> _transactionList;

        public RelayCommand AddItemBtnCommand { get; set; }
        public RelayCommand DeleteItemBtnCommand { get; set; }
        public RelayCommand PaymentBtnCommand { get; set; }
        public int ProductQty
        {
            get { return _productQty; }
            set { _productQty = value; OnPropertyChanged(); }
        }
        public decimal TotalPriceTransaction 
        {
            get { return _totalpriceTransaction; } 
            set { _totalpriceTransaction = value;
                Change = Cash;
                OnPropertyChanged(); } 
        }
        public decimal Cash
        {
            get { return _cash; }
            set { _cash = value;
                Change = _cash;
                OnPropertyChanged(); }
        }
        public decimal Change
        {
            get { return _change - _totalpriceTransaction; }
            set { _change = value; OnPropertyChanged(); }
        }

        public ProductModel SProduct
        {
            get { return _sproduct; }
            set { _sproduct = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ProductModel> ProductList
        {
            get { return _productList; }
            set { _productList = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TransactionModel> TransactionList
        {
            get { return _transactionList; }
            set { _transactionList = value; OnPropertyChanged(); }
        }

        #endregion

        public void convertProductListToCollection()
        {
            DataTable products = new ProductModel().getAllProduct();
            ProductList = new ObservableCollection<ProductModel>();
            foreach (DataRow row in products.Rows)
            {
                var obj = new ProductModel()
                {
                    ProductNo = (int)row["Product_no"],
                    ProductName = (string)row["Product_name"],
                    Price = (decimal)row["Price"],
                    Qty = (int)row["Quantity"],
                };
                ProductList.Add(obj);
            }    
        }

        private void refreshAllData()
        {
            TotalPriceTransaction = 0;
            TransactionList.Clear();
            ProductQty = 1;
            Cash = 0;
            Change = 0;
        }
        
        public TransactionViewModel()
        {
            convertProductListToCollection();
            i = 0;
            TotalPriceTransaction = new Decimal(00);
            TransactionList = new ObservableCollection<TransactionModel>();

            AddItemBtnCommand = new RelayCommand(o =>
            {
                if(SProduct!= null)
                {
                    decimal totalPrice = SProduct.Price * ProductQty;
                    TransactionList.Add(new TransactionModel() 
                    { 
                        NoIndex = i,
                        ProductNo = SProduct.ProductNo,
                        Name = SProduct.ProductName,
                        Price = SProduct.Price,
                        Quantity = ProductQty,
                        TotalPrice = totalPrice,
                    });
                    TotalPriceTransaction += totalPrice;
                    ProductQty = 1;
                    i++;
                }
                else
                {
                    MessageBox.Show("you must select item first");
                }
            });

            DeleteItemBtnCommand = new RelayCommand(o =>
            {
                int index = Convert.ToUInt16(o);
                TransactionModel model = TransactionList.Where(i => i.NoIndex == index).FirstOrDefault();
                TotalPriceTransaction -= model.TotalPrice;
                TransactionList.Remove(model);
            });

            PaymentBtnCommand = new RelayCommand(o =>
            {
                int transactionId = new TransactionModel().getLastTransactionId();
                DetailTransactionModel detailTransactionModel = new DetailTransactionModel();
                if(SProduct != null)
                {
                    new TransactionModel().insertTransactionToDB(transactionId,TotalPriceTransaction);
                    foreach (TransactionModel obj in TransactionList)
                    {
                        detailTransactionModel.insertDetailTransactionToDB(transactionId, obj.Name, obj.Quantity, obj.TotalPrice);
                    }
                    refreshAllData();
                    MessageBox.Show("Payment successfully");
                }
                else
                {
                    MessageBox.Show("you must add item first");
                }
            });

        }



    }
}
