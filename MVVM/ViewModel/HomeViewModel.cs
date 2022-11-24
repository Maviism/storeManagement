using System;
using System.Data;
using storeManagement.Core;
using storeManagement.MVVM.Model;

namespace storeManagement.MVVM.ViewModel
{
    internal class HomeViewModel:ObservableObject
    {
        TransactionModel transactionModel = new TransactionModel();
        ProductModel productModel = new ProductModel();
        
        private int _totalTransaction;
        public int TotalTransaction 
        { 
            get { return _totalTransaction; } 
            set { 
                _totalTransaction = value; 
                OnPropertyChanged();
            } 
        }

        public DataTable OutOfStockProduct { get; set; }
        public decimal DailyIncome { get; set; }
        public int TotalItem { get; set; }



        public HomeViewModel()
        {
            DataTable dt = productModel.getAllProduct();
            TotalItem = dt.Rows.Count;
            OutOfStockProduct = productModel.getOutOfStockProduct();
            TotalTransaction = transactionModel.getTotalTransaction(DateTime.Today.ToString("yyyy-MM-dd"));
            DailyIncome = transactionModel.getDailyIncome();
            
        }
    }
}
