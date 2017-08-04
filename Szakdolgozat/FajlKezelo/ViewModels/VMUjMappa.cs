using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using System.ComponentModel; // INotifyPropertyChanged megvalósításához kell.
using FajlKezelo.Models;

namespace FajlKezelo.ViewModels
{
    /// <summary>
    /// Új mappa viewmodelje
    /// </summary>
    class VMUjMappa : INotifyPropertyChanged
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

        private Model model;

        public VMUjMappa()
        {
            model = Model.model;
        }

        private string ujMappa;

        /// <summary>
        /// Új mappa nevét bekérő textboxba beírt mappanevet tárolja.
        /// </summary>
        public string UjMappa
        {
            get
            {
                return ujMappa;
            }

            set
            {
                ujMappa = value;
                NotifyPropertyChange("UjMappa");
            }
        }

        private string _OK;

        /// <summary>
        /// Ok gomb. Meghívja a mappa létrehozására alkalmas osztály megfelelő metódusát.
        /// </summary>
        public string OK
        {
            get
            {
                return _OK;
            }

            set
            {
                _OK = value;
                string eleresiUt;

                if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                {
                    eleresiUt = BeallitasVezerlo.Instance.AktualisMappa1;
                }
                else
                {
                    eleresiUt = BeallitasVezerlo.Instance.AktualisMappa2;
                }

                FajlMuveletek fm = new FajlMuveletek();
                fm.MappaLetrehoz(eleresiUt, UjMappa);
                NotifyPropertyChange("OK");
            }
        }
    }
}
