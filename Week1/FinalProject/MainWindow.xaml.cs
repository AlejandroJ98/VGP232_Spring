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
using GunLib;
using TextureAtlasLib;

namespace FinalProject
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

        private void Loadpressed(object sender, RoutedEventArgs e)
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
                    lbWeapons.ItemsSource = weaponcollection;
                    weaponcollection.weapons = lbWeapons.ItemsSource as List<Weapon>;
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
            GunEditor editweapon = new GunEditor();
            editweapon.bSave.Visibility = Visibility.Hidden;
            editweapon.Title = "Add Weapon";
            editweapon.lbImage.Visibility = Visibility.Hidden;
            if (editweapon.ShowDialog() == true)
            {
                weaponcollection.Add(editweapon.MyWeapon);
                if (lbWeapons.ItemsSource == null)
                {
                    lbWeapons.ItemsSource = weaponcollection;
                }
                lbWeapons.Items.Refresh();
            }
        }

        private void EditPressed(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
            {
                return;
            }

            GunEditor editWeaponWindow = new GunEditor(lbWeapons.SelectedItem as Weapon);
            editWeaponWindow.lbTemp.Visibility = Visibility.Hidden;
            editWeaponWindow.MyWeapon = lbWeapons.SelectedItem as Weapon;

            if (editWeaponWindow.ShowDialog() == true)
            {
                weaponcollection[lbWeapons.SelectedIndex] = editWeaponWindow.MyWeapon;
                lbWeapons.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if (lbWeapons.SelectedIndex == -1)
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

        private void SortByEffectiveRange(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("EffectiveRange");
            lbWeapons.Items.Refresh();
        }

        private void SortbyCaliber(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("Caliber");
            lbWeapons.Items.Refresh();
        }

        private void SortbyDesignedYear(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("DesignedYear");
            lbWeapons.Items.Refresh();
        }

        private void SortbyCountry(object sender, RoutedEventArgs e)
        {
            weaponcollection.SortBy("Country");
            lbWeapons.Items.Refresh();
        }

        private void GetByType(object sender, SelectionChangedEventArgs e)
        {
            var type = Enum.Parse<weapontype>(cbShowType.SelectedItem.ToString());
            lbWeapons.ItemsSource = weaponcollection.GetAllWeaponsOfType(type);
            lbWeapons.Items.Refresh();
        }

        private void FilterBynameChanged(object sender, TextChangedEventArgs e)
        {
            string name = (sender as TextBox).Text;
            weaponcollection.weapons = weaponcollection.weapons.FindAll(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            lbWeapons.ItemsSource = weaponcollection.weapons;
            lbWeapons.Items.Refresh();
        }

        private void ShowAllPressed(object sender, RoutedEventArgs e)
        {
            lbWeapons.ItemsSource = weaponcollection;
            weaponcollection.weapons = lbWeapons.ItemsSource as List<Weapon>;
            lbWeapons.Items.Refresh();
        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CheckPressed(object sender, RoutedEventArgs e)
        {
            Inventory window = new Inventory();
            if(lbWeapons.Items != null)
            {
                window.lbwl.ItemsSource = weaponcollection;
                weaponcollection.weapons = window.lbwl.ItemsSource as List<Weapon>;
                window.lbwl.Items.Refresh();
            }
            window.Show();
        }

        private void NewPressed(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("You have unsaved changes.\nWould you like to save them?", "Warning", MessageBoxButton.YesNo))
            {
                SavePressed(this, null);
            }
            weaponcollection.Clear();
            lbWeapons.Items.Refresh();
        }
    }
}
