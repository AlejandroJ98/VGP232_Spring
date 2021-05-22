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
using WeaponLib;

namespace Assignment2c
{
    /// <summary>
    /// Interaction logic for EditWeaponWindow.xaml
    /// </summary>
    public partial class EditWeaponWindow : Window
    {
        private Weapon myweapon;

        public Weapon MyWeapon
        {
            get { return myweapon; }
            set 
            {
                myweapon = value;
                DataContext = MyWeapon;//DataContext??
            }
        }

        public EditWeaponWindow()
        {
            InitializeComponent();

            string[] weapon = Enum.GetNames(typeof(weapontype));
            cbType.ItemsSource = weapon;

            MyWeapon = new Weapon();

            int[] rarity = { 1, 2, 3, 4, 5 };
            cbRarity.ItemsSource = rarity;

        }

        private void CancelPressed(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
