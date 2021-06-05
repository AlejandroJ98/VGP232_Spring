using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace Assignment4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Crypto crypto;
        public MainWindow()
        {
            InitializeComponent();
            crypto = new Crypto();
            crypto.Initialize(CryptoAlgorithm.AES);
        }

        private void EncryptPressed(object sender, RoutedEventArgs e)
        {
            string plainText = tbPlainText.Text;
            plainText = plainText.Replace(' ', '+');
            int padding = plainText.Length % 4;
            if(padding != 0)
            {
                plainText += new string('+', 4 - padding);
            }

            byte[] data = Convert.FromBase64String(plainText);
            byte[] cypherData = crypto.Encrypt(data);
            string cypherText = Convert.ToBase64String(cypherData);
            tbCypherText.Text = cypherText;
        }

        private void DecryptPressed(object sender, RoutedEventArgs e)
        {
            string cypherText = tbCypherText.Text;
            byte[] cypherData = Convert.FromBase64String(cypherText);
            byte[] plainData = crypto.Decrypt(cypherData);
            string plainText = Convert.ToBase64String(plainData);
            plainText = plainText.Replace('+', ' ');
            tbDecypherText.Text = plainText;
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text files |*.txt";
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllText(saveFile.FileName, tbDecypherText.Text);
            }
        }

        private void LoadPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files |*.txt";
            if (openFile.ShowDialog() == true)
            {
                tbPlainText.Text = File.ReadAllText(openFile.FileName);
            }
        }
        private void Save1Pressed(object sender, RoutedEventArgs e)//save cypher
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Binary files |*.bin";

            string cypherText = tbCypherText.Text;
            byte[] cypherData = Convert.FromBase64String(cypherText);
            if (saveFile.ShowDialog() == true)
            {
                File.WriteAllBytes(saveFile.FileName, cypherData);
            }
        }

        private void Load2Pressed(object sender, RoutedEventArgs e)//load cypher
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Binary files |*.bin";

            if (openFile.ShowDialog() == true)
            {
                string cypherText = tbShowCypher.Text;
                byte[] cypherData = Convert.FromBase64String(cypherText);
                cypherData = File.ReadAllBytes(openFile.FileName);
                string Text = Convert.ToBase64String(cypherData);
                tbShowCypher.Text = Text;
            }
        }
    }
}
