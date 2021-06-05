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

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for Selection.xaml
    /// </summary>
    public partial class Selection : Window
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void NextButtonPressed(object sender, RoutedEventArgs e)
        {
            if(rbRSA.IsChecked == true)
            {
                RSAkey rWindow = new RSAkey();
               // CryptoAlgorithm algo = CryptoAlgorithm.RSA;
                rWindow.Show();
            }
            else
            {
                AESkey aWindow = new AESkey();
               // CryptoAlgorithm algo = CryptoAlgorithm.AES;
                aWindow.Show();
            }
            Close();
        }
    }
}
