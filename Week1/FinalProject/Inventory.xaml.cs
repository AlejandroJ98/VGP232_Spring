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
using GunLib;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Window
    {
        public WeaponCollection weaponcollection { get; set; }
        public Inventory()
        {
            InitializeComponent();
            weaponcollection = new WeaponCollection();
            string[] weapon = Enum.GetNames(typeof(weapontype));
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {

        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {

        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Openpressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| Json Files |*.json | CSV Files|*.csv";

            if (openFile.ShowDialog() == true)
            {
                if (!weaponcollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    lbwl.ItemsSource = weaponcollection;
                    weaponcollection.weapons = lbwl.ItemsSource as List<Weapon>;
                    lbwl.Items.Refresh();
                }
            }
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {

        }
    }
}
