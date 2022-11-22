using System;
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
using storeManagement.MVVM.Model;
using System.Diagnostics;
using System.Text.RegularExpressions;
using storeManagement.Core;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using ZXing.Windows.Compatibility;
using ZXing;
using System.Windows.Threading;
using storeManagement.MVVM.ViewModel;
using System.Data;
using System.Media;

namespace storeManagement.MVVM.View
{
    /// <summary>
    /// Interaction logic for TransactionView.xaml
    /// </summary>
    public partial class TransactionView : UserControl
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;
        DispatcherTimer timer = new DispatcherTimer();
        public bool isScannerOn = false;
        static string path = "D:\\Codes\\c#\\storeManagement\\beep_sound.wav";
        SoundPlayer soundEffect = new SoundPlayer(path);

        private TransactionViewModel viewModel = new TransactionViewModel();

        public TransactionView()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filterInfo in filterInfoCollection)
                scannerDevice.Items.Add(filterInfo.Name);
            scannerDevice.SelectedIndex = 0;
        }

        private void StopScanner(object sender, EventArgs e)
        {
            if(captureDevice != null && captureDevice.IsRunning)
            {
                captureDevice.SignalToStop();
                captureDevice = null;
            }
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnToggleScanner_Click(object sender, EventArgs e)
        {
            if (!isScannerOn)
            {
                captureDevice = new VideoCaptureDevice(filterInfoCollection[scannerDevice.SelectedIndex].MonikerString);
                captureDevice.NewFrame += CaptureDevice_NewFrame;
                captureDevice.Start();
                btnScanner.Content = "Stop";
                btnScanner.Background = new SolidColorBrush(Colors.Red);
                isScannerOn = true;
                timer.Start();
            }
            else
            {
                if (captureDevice != null && captureDevice.IsRunning)
                {
                    timer.Stop();
                    captureDevice.SignalToStop();
                    captureDevice = null;
                    btnScanner.Content = "Start";
                    isScannerOn = false;
                    btnScanner.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#3667FD");

                }
            }
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {

            this.Dispatcher.Invoke(() => {
                Bitmap imgTes = (Bitmap)eventArgs.Frame.Clone();

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                imgTes.Save(ms, ImageFormat.Bmp);
                imgBox.Source = BitmapFrame.Create(ms);
            });
            }
            catch (TaskCanceledException ex)
            {
                captureDevice.SignalToStop();//prevent error if user close windows directly when camera on
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ImageSource img = imgBox.Source;
            BitmapSource bmp = (BitmapSource)img;

            if(img != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader()
                {
                    Options =
                    {
                        TryHarder = true,
                    },
                    AutoRotate = true,
                    TryInverted = true

                };
                Result result = barcodeReader.Decode(bmp);
                //check if there is the same value
                //add to transactionlist
                if(result != null)
                {
                    string temp = result.ToString();
                    bool isNumeric = int.TryParse(temp, out int n);
                    productNo.Text = temp;
                    if (isNumeric)
                    {
                        addScannedProductToTransactionList(Convert.ToInt32(temp));
                        qtyScanner.Text = "1";
                    }
                }
            }
        }



        private void addScannedProductToTransactionList(int temp)
        {
            foreach (ProductModel row in viewModel.ProductList)
            {
                if (row.ProductNo == Convert.ToInt32(temp))
                {
                    decimal TotalPrice = row.Price * Convert.ToInt32(qtyScanner.Text);
                    viewModel.TransactionList.Add(new TransactionModel()
                    {
                        NoIndex = viewModel.i,
                        ProductNo = row.ProductNo,
                        Name = row.ProductName,
                        Price = row.Price,
                        Quantity = Convert.ToInt32(qtyScanner.Text),
                        TotalPrice = TotalPrice,
                    });
                    soundEffect.Play();
                    viewModel.TotalPriceTransaction += TotalPrice;
                    viewModel.i++;
                }
            }
        }


    }
}
