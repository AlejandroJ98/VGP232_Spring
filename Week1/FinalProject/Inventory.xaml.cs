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
            if (weaponcollection.Count <= 2)
            {
                weaponcollection.Add(lbwl.SelectedItem as Weapon);
                lbin.ItemsSource = weaponcollection;
                lbin.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Warning: You can only add Three Guns in the INVENTORY!");
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbin.SelectedIndex == -1)
            {
                return;
            }

            weaponcollection.RemoveAt(lbin.SelectedIndex);
            lbin.Items.Refresh();
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
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML files |*.xml| Json Files |*.json | CSV Files|*.csv";

            if (saveFile.ShowDialog() == true)
            {
                if (!weaponcollection.Save(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        //private void EditPressed(object sender, RoutedEventArgs e)
        //{
        //    if (lbwl.SelectedIndex == -1)
        //    {
        //        return;
        //    }

        //    GunEditor editWeaponWindow = new GunEditor(lbwl.SelectedItem as Weapon);
        //    editWeaponWindow.lbTemp.Visibility = Visibility.Hidden;
        //    editWeaponWindow.MyWeapon = lbwl.SelectedItem as Weapon;

        //    if (editWeaponWindow.ShowDialog() == true)
        //    {
        //        weaponcollection = lbwl.ItemsSource as WeaponCollection;
        //        weaponcollection[lbwl.SelectedIndex] = editWeaponWindow.MyWeapon;
        //        lbwl.Items.Refresh();
        //    }
        //}
    }
}
