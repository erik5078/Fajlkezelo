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
    /// Interaction logic for UjMappaWindow.xaml
    /// </summary>
    public partial class UjMappaWindow : Window
    {
        ViewModels.VMUjMappa VM;

        public UjMappaWindow()
        {
            InitializeComponent();

            VM = new ViewModels.VMUjMappa();
            this.DataContext = VM;
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            VM.OK = "true";
            this.Close();
        }

        private void Megse_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
