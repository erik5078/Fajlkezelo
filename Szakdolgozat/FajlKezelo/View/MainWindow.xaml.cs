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

using System.Collections.ObjectModel;

namespace FajlKezelo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.ViewModel VM;
        public static MainWindow main;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
            VM = new ViewModels.ViewModel();
            this.DataContext = VM;
        }

        private void AktualisMappa1_MouseEnter(object sender, MouseEventArgs e)
        {
            //VM.SetAktualisAblak(0);
        }

        private void AktualisMappa2_MouseEnter(object sender, MouseEventArgs e)
        {
            //VM.SetAktualisAblak(1);
        }

        private void AktualisMappa1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VM.SetAktualisAblak(0);
        }

        private void AktualisMappa2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VM.SetAktualisAblak(1);
        }

        private void FajlLista1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Kijelölés
            VM.SetAktualisAblak(0);

            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                VM.Megnyitas(0, dgr.GetIndex());
            }
        }

        private void FajlLista2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Kijelölés
            VM.SetAktualisAblak(1);

            if (sender != null)
            {
                DataGridRow dgr = sender as DataGridRow;
                VM.Megnyitas(1, dgr.GetIndex());

            }
        }

        //Billentyűzetvezérlő meghívása
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(e);
        }

        public void SetItemSzin(int ablak, int index, Brush szin)
        {
            if (ablak == 0)
            {
                DataGridRow dataGridRow = FajlLista1.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                
                if (dataGridRow != null)
                {
                    dataGridRow.Foreground = szin;
                }
            }
            else
            {
                DataGridRow dataGridRow = FajlLista2.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
                if (dataGridRow != null)
                {
                    dataGridRow.Foreground = szin;
                }
            }
        }

        //Billentyűzetvezérlő meghívása
        private void FajlLista1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(e);

            switch (e.Key)
            {
                case Key.Enter:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        //Billentyűzetvezérlő meghívása
        private void FajlLista2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(e);

            switch (e.Key)
            {
                case Key.Enter:
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        private void Kijelol_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, false, "Space");
        }

        private void Megnyitas_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, false, "Return");
        }

        private void Lomtarba_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, false, "Delete");
        }

        private void Torles_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(true, false, "Delete");
        }

        private void OsszesKijelol_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, true, "A");
        }

        private void Masolas_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, false, "F5");
        }

        private void Athelyezes_Click(object sender, RoutedEventArgs e)
        {
            Models.BillentyuzetVezerlo.Instance.BillentyuMegnyomas(false, false, "F6");
        }
    }
}
