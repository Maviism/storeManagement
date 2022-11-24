using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for ManageStockView.xaml
    /// </summary>
    public partial class ManageStockView : UserControl
    {
        public ManageStockView()
        {
            InitializeComponent();
        }

        private void ExportProductToExcel(object sender, EventArgs e )
        {
            Products.SelectAllCells();
            Products.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, Products);
            //string resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string result = (string)Clipboard.GetData(DataFormats.Text);
            Products.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\Codes\c#\storeManagement\Exports\products.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Exporting data to products.xls");
        }

        private void ExportStockHistoryToExcel(object sender, EventArgs e)
        {
            Stock_history.SelectAllCells();
            Stock_history.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, Stock_history);
            string result = (string)Clipboard.GetData(DataFormats.Text);
            Stock_history.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\Codes\c#\storeManagement\Exports\Stock_history.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Exporting data to Stock_history.xls");
        }


    }
}
