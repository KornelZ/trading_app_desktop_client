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

namespace LGSA.Utility
{
    /// <summary>
    /// Interaction logic for RateWindow.xaml
    /// </summary>
    public partial class RateWindow : Window
    {
        private string _rating = "";
        public string Rating { get { return _rating; } set { _rating = value; } }
        public RateWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = sender as ComboBox;
            ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
            Label l = ci.Content as Label;
            Rating = l.Content.ToString();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Rating = "";
            this.Close();
        }
    }
}
