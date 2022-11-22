using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Emgu.CV;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;
using ZXing.QrCode;
using System.Threading;

namespace storeManagement.MVVM.View.Modal
{
    /// <summary>
    /// Interaction logic for ScannerModal.xaml
    /// </summary>
    /// 
    
    public partial class ScannerModal : Window
    {

        public ScannerModal()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice captureDevice;
        DispatcherTimer timer = new DispatcherTimer();

        private void Window_Loaded(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);
            cboDevice.SelectedIndex = 0;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            this.Dispatcher.Invoke(() => { 
            
            System.Drawing.Bitmap imgTes = (Bitmap)eventArgs.Frame.Clone();

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            MemoryStream ms = new MemoryStream();
            imgTes.Save(ms, ImageFormat.Bmp);
            pictureBox.Source = BitmapFrame.Create(ms);
            });

        }

        private void Window_onClose(object sender, EventArgs e)
        {
            if (captureDevice.IsRunning && captureDevice != null)
            {
                captureDevice.SignalToStop();
                captureDevice = null;
            }
            timer.Stop();
                
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            ImageSource img = pictureBox.Source;
            BitmapSource bmp = (BitmapSource)img;

            
            if(pictureBox.Source != null)
            {

                BarcodeReader barcodereader = new BarcodeReader()
                {
                    Options =
                    {
                        TryHarder = true,
                    },
                    AutoRotate = true,
                    TryInverted = true
                    
                };
                QRCodeReader qrcode = new QRCodeReader();
                Result result = barcodereader.Decode(bmp);

                if(result != null)
                {
                    textQRCode.Text = result.ToString();
                }

            }
        }

    }
}
