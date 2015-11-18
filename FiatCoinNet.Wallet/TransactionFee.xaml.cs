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
using System.Windows.Shapes;

namespace FiatCoinNet.WalletGui
{
    /// <summary>
    /// Interaction logic for TransactionFee.xaml
    /// </summary>
    public partial class TransactionFee : Window
    {
        public string transactionfee { get; set; }

        public TransactionFee(string text)
        {
            InitializeComponent();
            this.textBoxTransactionFee.Text = text;
            transactionfee = text;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)this.Owner;
            mainwindow.textBoxTrasactionFee.Text = this.textBoxTransactionFee.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = (MainWindow)this.Owner;
            mainwindow.textBoxTrasactionFee.Text = transactionfee;
            this.Close();
        }
    }
}
