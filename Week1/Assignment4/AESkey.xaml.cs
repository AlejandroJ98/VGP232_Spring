using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for AESkey.xaml
    /// </summary>
    public partial class AESkey : Window
    {
        Crypto crypto = new Crypto();
        public AESkey()
        {
            InitializeComponent();
            crypto.Initialize(CryptoAlgorithm.AES);
        }

        private void ImportSharedKeypressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary File|*.bin";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPrivateKey(openFile.FileName);
            }
        }

        private void ImportIVPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary File|*.bin";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPublicKey(openFile.FileName);
            }
        }

        private void ExportSharedkeyPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary File|*.bin";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportSharedKey(saveFile.FileName);
            }
        }

        private void ExportIVPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary File|*.bin";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportIV(saveFile.FileName);
            }
        }

        private void NextPressed(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }
    }
}
