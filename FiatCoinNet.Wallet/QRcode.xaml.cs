using System;
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

        public QRcode()
        {
            InitializeComponent();
            ReceiveURI();
        }

        private void ReceiveURI()
        {
            string Address = "1DtwuSjGUwsq3bRXDeB3GjjifGqUTV4AgT";
            string URI = "bitcoin:" + Address;
            createQRCode(Address, 150, 150);
            TextBlock_URI.Text = URI;
            TextBlock_Address.Text = Address;
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
    }
}
