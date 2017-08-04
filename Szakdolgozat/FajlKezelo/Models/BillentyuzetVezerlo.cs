using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Billentyűvezérlés
    /// </summary>
    class BillentyuzetVezerlo
    {
        private static BillentyuzetVezerlo _instance;

        /// <summary>
        /// Singleton
        /// </summary>
        public static BillentyuzetVezerlo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BillentyuzetVezerlo();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Singleton privát konstruktora
        /// </summary>
        private BillentyuzetVezerlo()
        {

        }

        /// <summary>
        /// Model
        /// </summary>
        public Model MODEL;

        /// <summary>
        /// Billentyűleütés kezelése
        /// </summary>
        /// <param name="e"></param>
        public void BillentyuMegnyomas(KeyEventArgs e)
        {
            bool shift = false;
            bool control = false;

            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                control = true;
            }

            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                shift = true;
            }

            BillentyuMegnyomas(shift, control, e.Key.ToString());
            e.Handled = true;
        }

        /// <summary>
        /// Billentyűleütés kezelése
        /// </summary>
        /// <param name="shift">Shift gomb kezelése</param>
        /// <param name="control">Control gomb kezelése</param>
        /// <param name="e">esemény kezelése</param>
        public void BillentyuMegnyomas(bool shift, bool control, string e)
        {
            switch (e)
            {
                //ctrl + A: összes kijelölése
                case "A":
                    if (control)
                    {
                        MODEL.CtrlA();
                    }

                    break;
                case "Up":
                    if (shift)
                    {
                        MODEL.Nyilak(true, 0);
                    }
                    else
                    {
                        MODEL.Nyilak(false, 0);
                    }

                    break;
                case "Down":
                    if (shift)
                    {
                        MODEL.Nyilak(true, 1);
                    }
                    else
                    {
                        MODEL.Nyilak(false, 1);
                    }

                    break;
                //ctrl + R: Frissítés.
                case "R":
                    if (control)
                    {
                        MODEL.CtrlR();
                    }

                    break;
                //Enter: Tallózásnál belép a mappába vagy megnyitja a fájlt.
                case "Return":
                    MODEL.EnterGomb();
                    break;
                //delete: A kijelölt fájl lomtárba küldése, ha a számítógépen engedélyezve van a lomtár
                //ctrl + delete: Fájl végleges törlése
                case "Delete":
                    if (shift)
                    {
                        MODEL.ShiftDelete();
                    }
                    else
                    {
                        MODEL.Delete();
                    }

                    break;
                //Space: Többszörös kijelölésnél hasznos, kijelöli az aktuális fájlt, vagy törli a kijelölést.
                case "Space":
                    MODEL.Space();
                    break;
                //F5: Másolás.
                case "F5":
                    MODEL.Masolas(false);
                    break;
                //F6: Áthelyezés
                case "F6":
                    MODEL.Masolas(true);
                    break;
                //F7: Új mappa.
                case "F7":
                    MODEL.F7();
                    break;
                //Minden egyéb billentyű.
                default:
                    break;
            }
        }
    }
}
