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
using GunLib;
using Microsoft.Win32;
using TextureAtlasLib;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for GunEditor.xaml
    /// </summary>
    public partial class GunEditor : Window
    {
        private Weapon myweapon;

        public Weapon MyWeapon
        {
            get { return myweapon; }
            set
            {
                myweapon = value;
                DataContext = MyWeapon;
            }
        }
        public GunEditor()
        {
            InitializeComponent();

            string[] weapon = Enum.GetNames(typeof(weapontype));
            cbType.ItemsSource = weapon;

            MyWeapon = new Weapon();

            double[] caliber = { 7.62, 9, 13, 5.56, 357, 7.65, 0.44, 0.45, 0.36, 12, 0.52, 6.5, 7.92 };
            cbCaliber.ItemsSource = caliber;
        }

        public GunEditor(Weapon weapons)
        {
            InitializeComponent();

            string[] weapon = Enum.GetNames(typeof(weapontype));
            cbType.ItemsSource = weapon;

            MyWeapon = new Weapon();

            double[] caliber = { 7.62, 9, 13, 5.56, 357, 7.65, 0.44, 0.45, 0.36, 12, 0.52, 6.5, 7.92 };
            cbCaliber.ItemsSource = caliber;

            MyWeapon = weapons;
            Spritesheet spritesheet = new Spritesheet();
            spritesheet.InputPaths = new List<string>();
            DataContext = spritesheet;
            lbImage.ItemsSource = spritesheet.InputPaths;
            string filepath = "C:\\Users\\blackhand\\Desktop\\VGP232_Spring\\Week1\\FinalProject\\Guns\\" + MyWeapon.DesignedYear + ".jpg";
            spritesheet.InputPaths.Add(filepath);
            lbImage.Items.Refresh();
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

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            range.Text = new Random().Next(50, 2000).ToString();
            cbCaliber.SelectedIndex = new Random().Next(13);
            cbType.SelectedIndex = new Random().Next(Enum.GetNames(typeof(weapontype)).Length);
            year.Text = new Random().Next(1850, 2020).ToString();
        }

        
    }
}

