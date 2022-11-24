using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Documents;
using LiveCharts;
using LiveCharts.Wpf;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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
        public int SoldoutItem { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItem { get; set; }



        public HomeViewModel()
        {
            DataTable dt = productModel.getAllProduct();
            TotalItem = dt.Rows.Count;
            OutOfStockProduct = productModel.getOutOfStockProduct();
            SoldoutItem = 0;
            TotalTransaction = transactionModel.getTotalTransaction(DateTime.Today.ToString("yyyy-MM-dd"));
            DailyIncome = transactionModel.getDailyIncome();
            
        }
    }
}
