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

using System.Collections.ObjectModel;

namespace FajlKezelo.View
{
    /// <summary>
    /// Interaction logic for FelulIrWindow.xaml
    /// </summary>
    public partial class FelulirWindow : Window
    {
        private ViewModels.WMFelulir VM;
        public static FelulirWindow felulirWindow;

        public FelulirWindow()
        {
            InitializeComponent();
            VM = ViewModels.WMFelulir.Instance;
            this.DataContext = VM;
            felulirWindow = this;
        }

        private void Felulir(object sender, RoutedEventArgs e)
        {
            VM.Felulir = "true";
        }

        private void MindetFelulir(object sender, RoutedEventArgs e)
        {
            VM.MindetFelulir = "true";
        }

        private void Kihagy(object sender, RoutedEventArgs e)
        {
            VM.Kihagy = "true";
        }

        private void MindetKihagy(object sender, RoutedEventArgs e)
        {
            VM.MindetKihagy = "true";
        }

        private void Megse(object sender, RoutedEventArgs e)
        {
            VM.Megse = "true";
        } 
    }
}
