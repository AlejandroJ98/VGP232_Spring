using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for RSAkey.xaml
    /// </summary>
    public partial class RSAkey : Window
    {
        Crypto crypto = new Crypto();
        
        public RSAkey()
        {
            InitializeComponent();
            crypto.Initialize(CryptoAlgorithm.RSA);
        }



        private void ImportPrivateKeyPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Xml File|*.xml";
            if(openFile.ShowDialog() == true)
            {
                crypto.ImportPrivateKey(openFile.FileName);
            }
        }



        private void ExportPrivateKeyPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportPrivateKey(saveFile.FileName);
            }
        }

        private void NextPressed(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
        }

        private void ImportPublicKeyPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Xml File|*.xml";
            if (openFile.ShowDialog() == true)
            {
                crypto.ImportPublicKey(openFile.FileName);
            }
        }

        private void ExportPublicKeyPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Xml File|*.xml";
            if (saveFile.ShowDialog() == true)
            {
                crypto.ExportPublicKey(saveFile.FileName);
            }
        }
    }
}
