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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using WeaponLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WeaponCollection weaponcollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            weaponcollection = new WeaponCollection();
            string[] weapon = Enum.GetNames(typeof(weapontype));
            cbShowType.ItemsSource = weapon;
        }

        private void LoadPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml| Json Files |*.json | CSV Files|*.csv";

            if(openFile.ShowDialog() == true)
            {
                if(!weaponcollection.Load(openFile.FileName))
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    lbWeapons.ItemsSource = weaponcollection;
                    lbWeapons.Items.Refresh();
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

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            EditWeaponWindow editweapon = new EditWeaponWindow();
            editweapon.bSave.Visibility = Visibility.Hidden;
            editweapon.Title = "Add Weapon";
            if(editweapon.ShowDialog() == true)
            {
               weaponcollection.Add(editweapon.MyWeapon);
                if(lbWeapons.ItemsSource == null)
                {
                    lbWeapons.ItemsSource = weaponcollection;
                }
                lbWeapons.Items.Refresh();

            }
        }

        private void EditPressed(object sender, RoutedEventArgs e)
        {
            if(lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            EditWeaponWindow editWeaponWindow = new EditWeaponWindow();
            editWeaponWindow.MyWeapon = lbWeapons.SelectedItem as Weapon;

            if(editWeaponWindow.ShowDialog() == true)
            {
                weaponcollection[lbWeapons.SelectedIndex] = editWeaponWindow.MyWeapon;
                lbWeapons.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if(lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            weaponcollection.RemoveAt(lbWeapons.SelectedIndex);
            lbWeapons.Items.Refresh();
        }

        private void SortByName(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("Name");
            lbWeapons.Items.Refresh();
        }

        private void SortByBaseAttack(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("BaseAttack");
            lbWeapons.Items.Refresh();
        }

        private void SortbyRarity(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("Rarity");
            lbWeapons.Items.Refresh();
        }

        private void SortbyPassive(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("Passive");
            lbWeapons.Items.Refresh();
        }

        private void SortbySecondaryStat(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("SecondaryStat");
            lbWeapons.Items.Refresh();
        }

        private void GetByType(object sender, SelectionChangedEventArgs e)
        {
            var type = Enum.Parse<weapontype>(cbShowType.SelectedItem.ToString());
            lbWeapons.ItemsSource = weaponcollection.GetAllWeaponsOfType(type);
            lbWeapons.Items.Refresh();
        }
    }
}
