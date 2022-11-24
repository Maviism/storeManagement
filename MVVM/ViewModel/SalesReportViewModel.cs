using System;
using System.Collections.ObjectModel;
using System.Data;
using storeManagement.Core;
using storeManagement.MVVM.Model;

namespace storeManagement.MVVM.ViewModel
{
    internal class SalesReportViewModel : ObservableObject
    {

        private DataTable _transactionList;
        TransactionModel transactionModel = new TransactionModel();
        
        public ObservableCollection<TransactionModel> transactionModels { get; set; }

        public DataTable TransactionList
        {
            get { return _transactionList; }
            set { _transactionList = value;
                OnPropertyChanged();                
            }
        }

        public DataTable getDataTable()
        {
            DataSet ds = new DataSet();

            return new DataTable();
        }

        public void RefreshData()
        {
            TransactionList = transactionModel.getAllTransaction();
            transactionModels.Clear();
            foreach (DataRow row in TransactionList.Rows)
            {
                transactionModels.Add(
                    new TransactionModel()
                    {
                        TransactionId = Convert.ToInt32(row["Transaction_id"]),
                        TotalPrice = Convert.ToDecimal(row["Total_price"]),
                        CreatedAt = Convert.ToDateTime(row["Created_at"]),
                        DetailTransaction = new DetailTransactionModel().getDetailTransactionWithTransactionID(Convert.ToInt32(row["Transaction_id"]))
                    }
             );
            }
        }

        public SalesReportViewModel()
        {
            transactionModels = new ObservableCollection<TransactionModel>();
            TransactionList = transactionModel.getAllTransaction();
        }


    }
}
