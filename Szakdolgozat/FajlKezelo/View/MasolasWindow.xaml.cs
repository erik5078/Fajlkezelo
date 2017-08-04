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
using System.ComponentModel;

namespace FajlKezelo.View
{
    /// <summary>
    /// Interaction logic for MasolasWindow.xaml
    /// </summary>
    public partial class MasolasWindow : Window
    {
        private ViewModels.VMMasolas VM;
        private List<string> regiEleresiUt;
        private string ujEleresiUt;
        private BackgroundWorker bw;

        public static MasolasWindow masolasWindow;

        public MasolasWindow(List<string> regiEleresiUt, string ujEleresiUt)
        {
            InitializeComponent();

            masolasWindow = this;
            VM = ViewModels.VMMasolas.Instance;

            this.regiEleresiUt = regiEleresiUt;
            this.ujEleresiUt = ujEleresiUt;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VM.Start(regiEleresiUt, ujEleresiUt);
        }

        private void megse_button_Click(object sender, RoutedEventArgs e)
        {
            VM.Megse();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VM.Megse();
        }
    }
}
