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
using TextureAtlasLib;

namespace Real_Assignment_3
{
    public partial class MainWindow : Window
    {
        Spritesheet mySpriteSheet { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mySpriteSheet = new Spritesheet();
            mySpriteSheet.InputPaths = new List<string>();
            DataContext = mySpriteSheet;
            lbImages.ItemsSource = mySpriteSheet.InputPaths;
        }

        private void Openpressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML files |*.xml";
            if (openFile.ShowDialog() == true)
            {
                if (mySpriteSheet.LoadXML(openFile.FileName) != true)
                {
                    MessageBox.Show("Unable to load file.");
                }
                else
                {
                    mySpriteSheet.InputPaths.AddRange(mySpriteSheet.InputPaths);
                    lbImages.Items.Refresh();
                    tbOutputDir.Text = mySpriteSheet.OutputDirectory;
                   
                    tbOutput.Text = mySpriteSheet.OutputFile;
                    tbColumns.Text = mySpriteSheet.Columns.ToString();
                }
            }
        }

        private void AddPressed(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PNG Images| *.png";
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == true)
            {
                foreach(string imagePath in openFile.FileNames)
                {
                    mySpriteSheet.InputPaths.Add(imagePath);
                }
                lbImages.Items.Refresh();
            }
        }

        private void RemovePressed(object sender, RoutedEventArgs e)
        {
            if(lbImages.SelectedIndex == -1)
            {
                return;
            }
            mySpriteSheet.InputPaths.RemoveAt(lbImages.SelectedIndex);
            lbImages.Items.Refresh();
        }

        private void GeneratePressed(object sender, RoutedEventArgs e)
        {
            mySpriteSheet.IncludeMetaData = false;
            mySpriteSheet.OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            mySpriteSheet.OutputFile = "spriteSheet.png";

            try
            {
                mySpriteSheet.Generate(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void BrowsePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog browsePath = new SaveFileDialog();
            browsePath.Filter = "PNG Images| *.png";
            browsePath.DefaultExt = ".png";
            browsePath.InitialDirectory = @"C:\Users\blackhand\Downloads\";
            
            if (browsePath.ShowDialog() == true)
            {
                tbOutputDir.Text = browsePath.FileName;
                tbOutput.Text = System.IO.Path.GetFileName(browsePath.FileName);
            }
        }

        private void NewPressed(object sender, RoutedEventArgs e)
        {
            tbOutputDir.Text = @"C:\Users\blackhand\Downloads\";
            tbOutput.Text = string.Empty;
            tbColumns.Text = "0";
            if (MessageBoxResult.Yes == MessageBox.Show("You have unsaved changes.\nWould you like to save them?", "Warning", MessageBoxButton.YesNo))
            {
                SavePressed(this, null);
            }
            mySpriteSheet.InputPaths.Clear();
            lbImages.Items.Refresh();
        }

        private void SavePressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = tbOutputDir.Text;
            saveFile.Filter = "XML Images| *.xml";
            if (saveFile.ShowDialog() == true)
            {
                if (!mySpriteSheet.SaveAsXML(saveFile.FileName))
                {
                    if(System.IO.Directory.Exists(saveFile.FileName))
                    {
                        
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(saveFile.FileName);
                    }
                    tbOutputDir.Text = saveFile.FileName;
                }
            }
        }

        private void SaveAsPressed(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Images| *.xml";
            if (saveFile.ShowDialog() == true)
            {
                if (!mySpriteSheet.SaveAsXML(saveFile.FileName))
                {
                    MessageBox.Show("Unable to save file.");
                }
            }
        }

        private void ExitPressed(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
