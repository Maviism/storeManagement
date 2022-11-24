using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for SalesReportView.xaml
    /// </summary>
    public partial class SalesReportView : UserControl
    {
        public SalesReportView()
        {
            InitializeComponent();
        }

        private void ExportSalesReportToExcel(object sender, EventArgs e)
        {
            Sales_report.SelectAllCells();
            Sales_report.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, Sales_report);
            string result = (string)Clipboard.GetData(DataFormats.Text);
            Sales_report.UnselectAllCells();
            System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"D:\Codes\c#\storeManagement\Exports\Sales_report.xls");
            file1.WriteLine(result.Replace(',', ' '));
            file1.Close();

            MessageBox.Show("Exporting data to Sales_report.xls");
        }

    }
}
