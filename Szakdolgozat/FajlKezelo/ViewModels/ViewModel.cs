using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.ComponentModel; // INotifyPropertyChanged megvalósításához kell.
using FajlKezelo.Models;
using System.Windows;
using System.Windows.Media;

namespace FajlKezelo.ViewModels
{
    /// <summary>
    /// ViewModel
    /// </summary>
    class ViewModel : INotifyPropertyChanged
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

        Models.Model MODEL; // Deklarálunk egy Model típusú változót. 
        Models.FajlMuveletek FAJLMUVELETEK;
        List<Models.FajlAdatok> fajlAdatok1;
        List<Models.FajlAdatok> fajlAdatok2;

        public ViewModel()
        {
            MODEL = new Models.Model(this); // Objektumosítjuk a Model osztályt
            FAJLMUVELETEK = new Models.FajlMuveletek(this);
            Load();
        }

        /// <summary>
        /// Betöltés alapértékek beállítása.
        /// </summary>
        public void Load()
        {
            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                KijeloltElem1 = "0";
            }
            else
            {
                KijeloltElem2 = "0";
            }

            BeallitasVezerlo.Instance.AktualisMappa1 = MODEL.EleresiUtFormazas(BeallitasVezerlo.Instance.AktualisMappa1);
            BeallitasVezerlo.Instance.AktualisMappa2 = MODEL.EleresiUtFormazas(BeallitasVezerlo.Instance.AktualisMappa2);
            AktualisMappa1 = BeallitasVezerlo.Instance.AktualisMappa1;
            AktualisMappa2 = BeallitasVezerlo.Instance.AktualisMappa2;

            AktualisMappa1Kijeloles = MODEL.GetAktualisMappaKijeloles(0);
            AktualisMappa2Kijeloles = MODEL.GetAktualisMappaKijeloles(1);
            fajlAdatok1 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa1);
            FajlLista1 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok1);
            fajlAdatok2 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa2);
            FajlLista2 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok2);
            MODEL.FajlLista1 = fajlAdatok1;
            MODEL.FajlLista2 = fajlAdatok2;

            _Meghajtok = MODEL.Meghajtok;
            _Meghajto1 = MODEL.GetMeghajtoInAktualisEleresiUt(0);
            _Meghajto2 = MODEL.GetMeghajtoInAktualisEleresiUt(1);
        }


        private ObservableCollection<Models.FajlAdatok> _FajlLista1;
        
        /// <summary>
        /// Bal oldali fájllista, amit a datagrid megjelenít. 
        /// </summary>
        public ObservableCollection<Models.FajlAdatok> FajlLista1
        {
            get
            {
                return _FajlLista1;
            }
            set
            {
                _FajlLista1 = value;
                NotifyPropertyChange("FajlLista1");
            }
        }

        private ObservableCollection<Models.FajlAdatok> _FajlLista2;
        
        /// <summary>
        /// Jobb oldali fájllista, amit a datagrid megjelenít. 
        /// </summary>
        public ObservableCollection<Models.FajlAdatok> FajlLista2
        {
            get
            {
                return _FajlLista2;
            }
            set
            {
                _FajlLista2 = value;
                NotifyPropertyChange("FajlLista2");
            }
        }

        private string _AktualisMappa1;
        
        /// <summary>
        /// Bal oldali fájlkezelőben az aktuális mappa elérési útvonalát tárolja és jeleníti meg.
        /// </summary>
        public string AktualisMappa1
        {
            get
            {
                return _AktualisMappa1;
            }

            set
            {
                _AktualisMappa1 = value;
                NotifyPropertyChange("AktualisMappa1");
            }
        }

        private string _AktualisMappa2;

        /// <summary>
        /// Jobb oldali fájlkezelőben az aktuális mappa elérési útvonalát tárolja és jeleníti meg.
        /// </summary>
        public string AktualisMappa2
        {
            get
            {
                return _AktualisMappa2;
            }

            set
            {
                _AktualisMappa2 = value;
                NotifyPropertyChange("AktualisMappa2");
            }
        }

        private string _AktualisMappa1Kijeloles;

        /// <summary>
        /// Bal oldali fájlkezelőben az aktuális mappa elérési útvonalát kiíró részét szinezi.
        /// </summary>
        public string AktualisMappa1Kijeloles
        {
            get
            {
                return _AktualisMappa1Kijeloles;
            }

            set
            {
                _AktualisMappa1Kijeloles = value;
                NotifyPropertyChange("AktualisMappa1Kijeloles");
            }
        }

        private string _AktualisMappa2Kijeloles;

        /// <summary>
        /// Jobb oldali fájlkezelőben az aktuális mappa elérési útvonalát kiíró részét szinezi.
        /// </summary>
        public string AktualisMappa2Kijeloles
        {
            get
            {
                return _AktualisMappa2Kijeloles;
            }

            set
            {
                _AktualisMappa2Kijeloles = value;
                NotifyPropertyChange("AktualisMappa2Kijeloles");
            }
        }

        /// <summary>
        /// Megnyitás
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="index"></param>
        public void Megnyitas(int ablak, int index)
        {
            if (MODEL.IsDir(ablak, index))
            {
                MappaValtas(ablak, index);
            }
            else
            {
                MODEL.FajlMegnyitas(ablak, index);
            }
        }

        /// <summary>
        /// Fájllista frissítése mindkét oldalt.
        /// </summary>
        public void Frissites()
        {
            fajlAdatok1 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa1);
            MODEL.FajlLista1 = fajlAdatok1;
            FajlLista1 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok1);
            MODEL.KijeloltElem1 = 0;

            fajlAdatok2 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa2);
            MODEL.FajlLista2 = fajlAdatok2;
            FajlLista2 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok2);
            MODEL.KijeloltElem2 = 0;

            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                KijeloltElem1 = "0";
            }
            else
            {
                KijeloltElem2 = "0";
            }
        }

        /// <summary>
        /// Belépés egy mappába.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="index">Index</param>
        public void MappaValtas(int ablak, int index)
        {
            if (ablak == 0)
            {
                ObservableCollection<Models.FajlAdatok> fajlLista = FajlLista1;
                BeallitasVezerlo.Instance.AktualisMappa1 = BeallitasVezerlo.Instance.AktualisMappa1 + fajlLista[index].FajlNev + "/";
                BeallitasVezerlo.Instance.AktualisMappa1 = MODEL.EleresiUtFormazas(BeallitasVezerlo.Instance.AktualisMappa1);
                fajlAdatok1 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa1);
                MODEL.FajlLista1 = fajlAdatok1;
                FajlLista1 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok1);
                AktualisMappa1 = BeallitasVezerlo.Instance.AktualisMappa1;
                MODEL.KijeloltElem1 = 0;
            }
            else
            {
                ObservableCollection<Models.FajlAdatok> fajlLista = FajlLista2;
                BeallitasVezerlo.Instance.AktualisMappa2 = BeallitasVezerlo.Instance.AktualisMappa2 + fajlLista[index].FajlNev + "/";
                BeallitasVezerlo.Instance.AktualisMappa2 = MODEL.EleresiUtFormazas(BeallitasVezerlo.Instance.AktualisMappa2);
                fajlAdatok2 = MODEL.GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa2);
                MODEL.FajlLista2 = fajlAdatok2;
                FajlLista2 = new ObservableCollection<Models.FajlAdatok>(fajlAdatok2);
                AktualisMappa2 = BeallitasVezerlo.Instance.AktualisMappa2;
                MODEL.KijeloltElem2 = 0;
            }
        }

        /// <summary>
        /// Aktuális ablak változtatása.
        /// </summary>
        /// <param name="ablak"></param>
        public void SetAktualisAblak(int ablak)
        {
            MODEL.SetAktualisAblak(ablak);

            if (ablak == 0)
            {
                AktualisMappa1Kijeloles = MODEL.GetAktualisMappaKijeloles(0);
                AktualisMappa2Kijeloles = MODEL.GetAktualisMappaKijeloles(1);
            }
            else
            {
                AktualisMappa1Kijeloles = MODEL.GetAktualisMappaKijeloles(0);
                AktualisMappa2Kijeloles = MODEL.GetAktualisMappaKijeloles(1);
            }
        }

        private string _KijeloltElem1;

        /// <summary>
        /// Kijelölt elem indexe bal oldalt.
        /// </summary>
        public string KijeloltElem1
        {
            get
            {
                return _KijeloltElem1;
            }

            set
            {
                _KijeloltElem1 = value;
                MODEL.KijeloltElem1 = Convert.ToInt32(_KijeloltElem1);
                SetAktualisAblak(0);
                NotifyPropertyChange("KijeloltElem1");
            }
        }

        private string _KijeloltElem2;

        /// <summary>
        /// Kijelölt elem indexe jobb oldalt.
        /// </summary>
        public string KijeloltElem2
        {
            get
            {
                return _KijeloltElem2;
            }

            set
            {
                _KijeloltElem2 = value;
                MODEL.KijeloltElem2 = Convert.ToInt32(_KijeloltElem2);
                SetAktualisAblak(1);
                NotifyPropertyChange("KijeloltElem2");
            }
        }

        private string _Hatter;

        /// <summary>
        /// Új mappa.
        /// </summary>
        public void F7()
        {
            View.UjMappaWindow um = new View.UjMappaWindow();
            um.ShowDialog();
            Frissites();
        }

        /// <summary>
        /// Frissítés.
        /// </summary>
        public void CtrlR()
        {
            Frissites();
        }

        /// <summary>
        /// Végleges törlés.
        /// </summary>
        public void ShiftDelete()
        {
            string fajlnev;

            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                fajlnev = FajlLista1[MODEL.KijeloltElem1].FajlNev;
            }
            else
            {
                fajlnev = FajlLista2[MODEL.KijeloltElem2].FajlNev;
            }
            
            MessageBoxResult valasz = MessageBox.Show("Tényleg törlöd a(z) " + fajlnev + "?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (valasz == MessageBoxResult.Yes)
            {
                MODEL.Torles(false);
            }
        }

        /// <summary>
        /// Lomtárba küldés.
        /// </summary>
        public void Delete()
        {
            MessageBoxResult valasz = MessageBox.Show("Tényleg törlöd a kijelölt fájl(oka)t?", "Biztos, hogy törli?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (valasz == MessageBoxResult.Yes)
            {
                MODEL.Torles(true);
            }
        }

        /// <summary>
        /// Elemkijelölés -> színmódosítár része.
        /// </summary>
        /// <param name="szin"></param>
        public void Elemkijelol(Brush szin)
        {
            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                MainWindow.main.SetItemSzin(0, Convert.ToInt32(KijeloltElem1), szin);
            }
            else
            {
                MainWindow.main.SetItemSzin(1, Convert.ToInt32(KijeloltElem2), szin);
            }
        }

        /// <summary>
        /// Összes elem kijelölése -> Színmódosítás része.
        /// </summary>
        /// <param name="ablak"></param>
        /// <param name="szin"></param>
        public void ElemKijelolOsszes(int ablak, Brush szin)
        {
            if (ablak == 0)
            {
                for (int i = 0; i < FajlLista1.Count; i++)
                {
                    MainWindow.main.SetItemSzin(0, i, szin);
                }
            }
            else
            {
                for (int i = 0; i < FajlLista2.Count; i++)
                {
                    MainWindow.main.SetItemSzin(1, i, szin);
                }
            }
        }

        private ObservableCollection<string> _Meghajtok;
        private string _Meghajto1;
        private string _Meghajto2;

        /// <summary>
        /// Meghajtólista
        /// </summary>
        public ObservableCollection<string> Meghajtok
        {
            get
            {
                return _Meghajtok;
            }
        }

        /// <summary>
        /// Bal oldali meghajtóválasztó
        /// </summary>
        public string Meghajto1
        {
            get
            {
                return _Meghajto1;
            }

            set
            {
                if (_Meghajto1 == value)
                {
                    return;
                }
                
                _Meghajto1 = value;
                NotifyPropertyChange("Meghajto1");
                MODEL.MeghajtoValtas(0, _Meghajto1);
            }
        }

        /// <summary>
        /// Jobb oldali meghajtóválasztó
        /// </summary>
        public string Meghajto2
        {
            get
            {
                return  _Meghajto2;
            }

            set
            {
                if (_Meghajto2 == value)
                {
                    return;
                }

                _Meghajto2 = value;
                NotifyPropertyChange("Meghajto2");
                MODEL.MeghajtoValtas(1, Meghajto2);
            }
        }
    }
}
