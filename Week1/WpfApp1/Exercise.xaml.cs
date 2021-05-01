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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Exercise.xaml
    /// </summary>
    public partial class Exercise : Window
    {
        private int count = 0;
        public Exercise()
        {
            InitializeComponent();
        }

        private void IncrementCountPressed(object sender, RoutedEventArgs e)
        {
            ++count;
            tbCounter.Text = string.Format("Count: {0}", count);
        }

        private void SayHello(object sender, RoutedEventArgs e)
        {
            tbHello.Text = string.Format("Count: {0}!", sender);
        }
    }
}
