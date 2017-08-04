using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel; // INotifyPropertyChanged megvalósításához kell.
using FajlKezelo.Models;

namespace FajlKezelo.ViewModels
{
    class WMFelulir
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

        private static ViewModels.WMFelulir instance;

        public static ViewModels.WMFelulir Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WMFelulir();
                }

                return instance;
            }
        }

        private WMFelulir()
        {
            felulir = Models.Felulir.Alaphelyzet;
            Felulir = "false";
            MindetFelulir = "false";
            Kihagy = "false";
            MindetKihagy = "false";
            Megse = "false";
        }

        /// <summary>
        /// Gombok értékeinek kinullázása az ablak eltünése után.
        /// </summary>
        public void ReInstance()
        {
            instance = new WMFelulir();
        }

        /// <summary>
        /// Felülírás kérdés szövege.
        /// </summary>
        /// <param name="fajlNev">Felülírandó fájl neve</param>
        public void SetFelulirLabel(string fajlNev)
        {
            if (fajlNev.Length > 40)
            {
                fajlNev = fajlNev.Substring(fajlNev.Length - 40, 40);
                fajlNev = "..." + fajlNev;
            }

            FelulirLabel = "Létezik a(z) " + fajlNev + ", felülírjam?";
        }

        private string _FelulirLabel;

        /// <summary>
        /// Felülírás kérdés szövege.
        /// </summary>
        public string FelulirLabel
        {
            get
            {
                return _FelulirLabel;
            }

            set
            {
                _FelulirLabel = value;
                NotifyPropertyChange("FelulirLabel");
            }
        }

        private string _Megse;

        /// <summary>
        /// Mégse gomb.
        /// </summary>
        public string Megse
        {
            get
            {
                return _Megse;
            }

            set
            {
                _Megse = value;
                NotifyPropertyChange("Megse");
                FelulirBeallit();
            }
        }

        private string _Felulir;

        /// <summary>
        /// Felülír
        /// </summary>
        public string Felulir
        {
            get
            {
                return _Felulir;
            }

            set
            {
                _Felulir = value;
                NotifyPropertyChange("Felulir");
                FelulirBeallit();
            }
        }

        private string _MindetFelulir;

        /// <summary>
        /// Mindet felülír (Ez esetben megjegyzi a többi felülirandóval és nem kérdezi többször.)
        /// </summary>
        public string MindetFelulir
        {
            get
            {
                return _MindetFelulir;
            }

            set
            {
                _MindetFelulir = value;
                NotifyPropertyChange("MindetFelulir");
                FelulirBeallit();
            }
        }

        private string _Kihagy;

        /// <summary>
        /// Nem írja felül.
        /// </summary>
        public string Kihagy
        {
            get
            {
                return _Kihagy;
            }

            set
            {
                _Kihagy = value;
                NotifyPropertyChange("Kihagy");
                FelulirBeallit();
            }
        }

        private string _MindetKihagy;

        /// <summary>
        /// Egyiket sem írja felül.
        /// </summary>
        public string MindetKihagy
        {
            get
            {
                return _MindetKihagy;
            }

            set
            {
                _MindetKihagy = value;
                NotifyPropertyChange("MindetKihagy");
                FelulirBeallit();
            }
        }

        /// <summary>
        /// Kívülről innen lehet elérni, hogy melyik gombra kattintott a felhasználó.
        /// </summary>
        private Felulir felulir;

        public Felulir FelulirProperty
        {
            get
            {
                return felulir;
            }
        }

        /// <summary>
        /// felülír property állítása.
        /// </summary>
        private void FelulirBeallit()
        {
            bool bezar = false;

            if (Felulir == "true")
            {
                felulir = Models.Felulir.Felulir;
                bezar = true;
            }
            else if (MindetFelulir == "true")
            {
                felulir = Models.Felulir.MindetFelulir;
                bezar = true;
            }
            else if (Kihagy == "true")
            {
                felulir = Models.Felulir.Kihagy;
                bezar = true;
            }
            else if (MindetKihagy == "true")
            {
                felulir = Models.Felulir.MindetKihagy;
                bezar = true;
            }
            else if (Megse == "true")
            {
                felulir = Models.Felulir.Megse;
                bezar = true;
            }
            else
            {
                felulir = Models.Felulir.Alaphelyzet;
            }

            if (bezar)
            {
                View.FelulirWindow.felulirWindow.Close();
            }
        }
    }
}
