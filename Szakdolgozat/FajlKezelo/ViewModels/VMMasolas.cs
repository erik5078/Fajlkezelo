using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

using System.ComponentModel; // INotifyPropertyChanged megvalósításához kell.
using FajlKezelo.Models;

namespace FajlKezelo.ViewModels
{
    /// <summary>
    /// Másolás ablak viewmodelje.
    /// </summary>
    class VMMasolas
    {
        #region INotifyPropertyChanged implementációja.

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged implementációjának vége.

        private Models.MasolasVezerlo model;
        private BackgroundWorker bw;
        private List<string> regiEleresiUt;
        private string ujEleresiUt;
        private static VMMasolas instance = new VMMasolas();

        /// <summary>
        /// Singleton
        /// </summary>
        public static VMMasolas Instance
        {
            get
            {
                return instance;
            }

            set
            {
                if (instance == null)
                {
                    instance = new VMMasolas();
                }
            }
        }

        private VMMasolas()
        {
            model = new Models.MasolasVezerlo(this);
            SzazalekMutato = "0 %";
            Tart = "0";
        }

        /// <summary>
        /// Adatok kinullázása (új példány létrehozása a régi törlésével.)
        /// </summary>
        private void ReInstance()
        {
            instance = new VMMasolas();
        }

        private string _Tart;

        /// <summary>
        /// Progressbar értéke.
        /// </summary>
        public string Tart
        {
            get
            {
                return _Tart;
            }

            set
            {
                _Tart = value;
                NotifyPropertyChange("Tart");
            }
        }

        private string _SzazalekMutato;

        /// <summary>
        /// Kiírja, hány százaléknál tart a másolás.
        /// </summary>
        public string SzazalekMutato
        {
            get
            {
                return _SzazalekMutato;
            }

            set
            {
                _SzazalekMutato = value;
                NotifyPropertyChange("SzazalekMutato");
            }
        }

        private string _TartOsszes;

        /// <summary>
        /// Összes fájl progressbar értéke.
        /// </summary>
        public string TartOsszes
        {
            get
            {
                return _TartOsszes;
            }

            set
            {
                _TartOsszes = value;
                NotifyPropertyChange("TartOsszes");
            }
        }

        private string _SzazalekMutatoOsszes;

        /// <summary>
        /// Kiírja, hány százaléknál tart a másolás az összes fájl szemszögéből.
        /// </summary>
        public string SzazalekMutatoOsszes
        {
            get
            {
                return _SzazalekMutatoOsszes;
            }

            set
            {
                _SzazalekMutatoOsszes = value + " %";
                NotifyPropertyChange("SzazalekMutatoOsszes");
            }
        }

        private string _FileDb;

        /// <summary>
        /// Fájlok száma.
        /// </summary>
        public string FileDb
        {
            get
            {
                return _FileDb;
            }

            set
            {
                _FileDb = value;
                NotifyPropertyChange("FileDb");
            }
        }

        private string _Forras;

        /// <summary>
        /// Éppen másolás alatt lévő fájl forrása.
        /// </summary>
        public string Forras
        {
            get
            {
                return _Forras;
            }

            set
            {
                _Forras = value;
                NotifyPropertyChange("Forras");
            }
        }

        private string _Cel;

        /// <summary>
        /// Éppen másolás alatt lévő fájl célja.
        /// </summary>
        public string Cel
        {
            get
            {
                return _Cel;
            }

            set
            {
                _Cel = value;
                NotifyPropertyChange("Cel");
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        /// <param name="regiEleresiUt">Régi elérési út.</param>
        /// <param name="ujEleresiUt">Új elérési út.</param>
        public void Start(List<string> regiEleresiUt, string ujEleresiUt)
        {
            this.regiEleresiUt = regiEleresiUt;
            this.ujEleresiUt = ujEleresiUt;

            bw = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            bw.DoWork += bw_DoWork;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            model.Masolas(regiEleresiUt, ujEleresiUt, bw);
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Tart = e.ProgressPercentage.ToString();
            SzazalekMutato = e.ProgressPercentage.ToString();
            View.MasolasWindow.masolasWindow.szazalekMutato_label.Content = SzazalekMutato + " %";
            View.MasolasWindow.masolasWindow.Tart.Value = Convert.ToDouble(Tart);
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (View.MasolasWindow.masolasWindow != null)
            {
                View.MasolasWindow.masolasWindow.szazalekMutato_label.Content = "Kész";
                View.MasolasWindow.masolasWindow.szazalekMutatoOsszes_label.Content = "Kész";
            }

            View.MasolasWindow.masolasWindow.Close();
            ReInstance();
        }

        /// <summary>
        /// Összes fájl progressbarjának kezelése
        /// </summary>
        /// <param name="szazalek">Százalék érték</param>
        public void OsszesProgressbar(double szazalek)
        {
            if (double.IsPositiveInfinity(szazalek))
            {
                return;
            }

            TartOsszes = szazalek.ToString();
            SzazalekMutatoOsszes = szazalek.ToString();

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                try
                {
        View.MasolasWindow.masolasWindow.szazalekMutatoOsszes_label.Content = SzazalekMutatoOsszes;
                    View.MasolasWindow.masolasWindow.TartOsszes.Value = Convert.ToDouble(TartOsszes);
                }
                catch (Exception e)
                {

                }
            }));
        }

        /// <summary>
        /// Egyébb adatok megjelenítése.
        /// </summary>
        /// <param name="keszFileDb">A már átmásolt fájlok száma.</param>
        /// <param name="fileDb">Összes fájl száma</param>
        /// <param name="forras">Forrás</param>
        /// <param name="cel">Cél</param>
        public void FajlAdatMegjelenito(int keszFileDb, int fileDb, string forras, string cel)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                try
                {
                    FileDb = fileDb + " / " + keszFileDb;
                    Forras = "Forrás: " + forras;
                    Cel = "Cél: " + cel;
                    View.MasolasWindow.masolasWindow.fileDb_label.Content = FileDb;
                    View.MasolasWindow.masolasWindow.forras_label.Content = Forras;
                    View.MasolasWindow.masolasWindow.cel_label.Content = Cel;
                }
                catch (Exception e)
                {

                }
            }));
        }

        /// <summary>
        /// Mégse gomb.
        /// </summary>
        public void Megse()
        {
            bw.CancelAsync();

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                View.MasolasWindow.masolasWindow.Close();
                ReInstance();
            }));
        }
    }
}
