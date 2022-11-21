using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using storeManagement.Core;
using storeManagement.MVVM.Model;

namespace storeManagement.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        #region All of relayCommand Declaration
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TransactionViewCommand { get; set; }
        public RelayCommand ManageStockViewCommand { get; set; }
        public RelayCommand SalesReportViewCommand { get; set; }
        #endregion

        public HomeViewModel HomeVM { get; set; }
        public TransactionViewModel TransactionVM { get; set; }
        public ManageStockViewModel ManageStockVM { get; set; }
        public SalesReportViewModel SalesReportVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        ProductModel productModel = new ProductModel();
        TransactionModel transactionModel = new TransactionModel();
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            TransactionVM = new TransactionViewModel();
            ManageStockVM = new ManageStockViewModel();
            SalesReportVM = new SalesReportViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
                DataTable dt = productModel.getAllProduct();
                HomeVM.TotalItem = dt.Rows.Count;
                HomeVM.TotalTransaction = transactionModel.getTotalTransaction(DateTime.Today.ToString("yyyy-MM-dd"));
                HomeVM.DailyIncome = transactionModel.getDailyIncome();
            });
            
            TransactionViewCommand = new RelayCommand(o =>
            {
                CurrentView = TransactionVM;
            });

            ManageStockViewCommand = new RelayCommand(o =>
            {
                CurrentView = ManageStockVM;
                ManageStockVM.Products = productModel.getAllProduct();
                ManageStockVM.StockHistory = new StockHistoryModel().getAllStockHistory();
            });

            SalesReportViewCommand = new RelayCommand(o =>
            {
                CurrentView = SalesReportVM;

            });

        }

    }
}
