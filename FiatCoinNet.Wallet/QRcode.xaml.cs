using System;
using FiatCoinNet.Domain;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using System.IO;
using Microsoft.Win32;

namespace FiatCoinNet.WalletGui
{
    /// <summary>
    /// Interaction logic for QRcode.xaml
    /// </summary>
    /// 
    public partial class QRcode : Window
    {
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        public QRcode(PaymentAccount account)
        {
            InitializeComponent();
            ReceiveURI(account);
        }

        private void ReceiveURI(PaymentAccount account)
        {
            string URI = "bitcoin:" + account.Address;
            QRImage.Source = createQRCode(URI, 150, 150);
            TextBlock_URI.Text = URI;
            TextBlock_Address.Text = account.Address;
            if(account.ReceiveAmount == 0)
            {
                TextBlock_Label_Amount.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlock_Amount.Text = Convert.ToString(account.ReceiveAmount);
            }
            if(account.ReceiveLabel == "")
            {
                TextBlock_Label_Label.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlock_Label.Text = account.ReceiveLabel;
            }
            if(account.ReceiveMessage == "")
            {
                TextBlock_Label_Message.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlock_Message.Text = account.ReceiveMessage;
            }
        }

        private ImageSource createQRCode(String content, int width, int height)
        {
            EncodingOptions options;
            BarcodeWriter write = null;
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 0
            };
            write = new BarcodeWriter();
            write.Format = BarcodeFormat.QR_CODE;
            write.Options = options;
            Bitmap bitmap = write.Write(content);
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
              ip, IntPtr.Zero, Int32Rect.Empty,
              System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)QRImage.Source));
            SaveFileDialog savefile = new SaveFileDialog();
            using (var stream = savefile.OpenFile())
            {
                encoder.Save(stream);
            }
        }
    }
}
