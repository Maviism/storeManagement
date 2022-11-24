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
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ISeries[] SeriesCollection { get; set; }

        public string[] Values { get; set; }


        public HomeViewModel()
        {
            DataTable dt = productModel.getAllProduct();
            TotalItem = dt.Rows.Count;
            OutOfStockProduct = productModel.getOutOfStockProduct();
            SoldoutItem = 0;
            TotalTransaction = transactionModel.getTotalTransaction(DateTime.Today.ToString("yyyy-MM-dd"));
            DailyIncome = transactionModel.getDailyIncome();

            //Chart
            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            YFormatter = value => value.ToString("C");
            Values = new string[] { "1", "2", "3", "4", "5", };

            SeriesCollection = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] {5,10,20,25}
                }
            };
            
        }
    }
}
