using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

        public TransactionView()
        {
            InitializeComponent();
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
            this.Dispatcher.Invoke(() => {
                System.Drawing.Bitmap imgTes = (Bitmap)eventArgs.Frame.Clone();

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                MemoryStream ms = new MemoryStream();
                imgTes.Save(ms, ImageFormat.Bmp);
                imgBox.Source = BitmapFrame.Create(ms);
            });
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

                if(result != null)
                {
                    productNo.Text = result.ToString();
                }
            }

        }


    }
}
