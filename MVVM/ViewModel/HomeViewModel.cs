using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public decimal DailyIncome { get; set; }

        public int SoldoutItem { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalItem { get; set; }

        SqlConnection conn = new DBHelper().Connection;

        public ISeries[] Series { get; set; } = new ISeries[]
        {
            new PieSeries<double> { Values = new List<double> { 2 }, InnerRadius = 50 },
            new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50 },
            new PieSeries<double> { Values = new List<double> { 1 }, InnerRadius = 50 },
            new PieSeries<double> { Values = new List<double> { 4 }, InnerRadius = 50 },
            new PieSeries<double> { Values = new List<double> { 3 }, InnerRadius = 50 }
        };

        public HomeViewModel()
        {
            DataTable dt = productModel.getAllProduct();
            TotalItem = dt.Rows.Count;
            TotalTransaction = transactionModel.getTotalTransaction(DateTime.Today.ToString("yyyy-MM-dd"));
            SoldoutItem = 0;
            DailyIncome = transactionModel.getDailyIncome();
        }
    }
}
