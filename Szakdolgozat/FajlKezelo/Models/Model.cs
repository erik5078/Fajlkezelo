using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;

using System.Collections.ObjectModel;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Model
    /// </summary>
    class Model
    {
        /// <summary>
        /// Baloldali fájlkezelőn kijelölt fájl/mappa indexe. -1 esetén nincs kijelölt elem.
        /// </summary>
        private int _KijeloltElem1;

        /// <summary>
        /// Jobboldali fájlkezelőn kijelölt fájl/mappa indexe. -1 esetén nincs kijelölt elem.
        /// </summary>
        private int _KijeloltElem2;

        /// <summary>
        /// Bal oldali fájlok adatai.
        /// </summary>
        public List<FajlAdatok> FajlLista1;

        /// <summary>
        /// Jobb oldali fájlok.
        /// </summary>
        public List<FajlAdatok> FajlLista2;

        /// <summary>
        /// Viewmodel példánya
        /// </summary>
        private ViewModels.ViewModel VM;

        /// <summary>
        /// Bal oldalt kijelölt elemek indexének listája többszörös kijelölés esetén.
        /// </summary>
        private List<int> _TobbszorosenKijeloltElemek1;

        /// <summary>
        /// Bal oldalt kijelölt elemek indexének listája többszörös kijelölés esetén.
        /// </summary>
        private List<int> _TobbszorosenKijeloltElemek2;

        private ObservableCollection<string> _Meghajtok;

        public Model(ViewModels.ViewModel vm)
        {
            _KijeloltElem1 = 0;
            _KijeloltElem2 = 0;
            FajlLista1 = new List<FajlAdatok>();
            FajlLista2 = new List<FajlAdatok>();
            VM = vm;
            BillentyuzetVezerlo.Instance.MODEL = this;
            _TobbszorosenKijeloltElemek1 = new List<int>();
            _TobbszorosenKijeloltElemek2 = new List<int>();
            _Meghajtok = new ObservableCollection<string>();
            MeghajtoFeltolt();
        }

        /// <summary>
        /// Lekérjük a meghatólistát
        /// </summary>
        private void MeghajtoFeltolt()
        {
            string[] meghajtok = Directory.GetLogicalDrives();

            for (int i = 0; i < meghajtok.Length; i++)
            {
                string[] temp = meghajtok[i].Split(':');
                meghajtok[i] = temp[0] + "://";
                _Meghajtok.Add(meghajtok[i]);
            }
        }

        public ObservableCollection<string> Meghajtok
        {
            get
            {
                return _Meghajtok;
            }
        }

        public string GetMeghajtoInAktualisEleresiUt(int ablak)
        {
            if (ablak == 0)
            {
                string[] temp = BeallitasVezerlo.Instance.AktualisMappa1.Split(':');
                temp[0] += "://";
                return temp[0];
            }
            else
            {
                string[] temp = BeallitasVezerlo.Instance.AktualisMappa2.Split(':');
                temp[0] += "://";
                return temp[0];
            }
        }

        /// <summary>
        /// Innen hívjuk a modelt.
        /// </summary>
        public static Model model;

        /// <summary>
        /// Baloldali fájlkezelőn kijelölt fájl/mappa indexe. -1 esetén nincs kijelölt elem.
        /// </summary>
        public int KijeloltElem1
        {
            get
            {
                return _KijeloltElem1;
            }

            set
            {
                _KijeloltElem1 = value;
            }
        }

        /// <summary>
        /// Jobboldali fájlkezelőn kijelölt fájl/mappa indexe. -1 esetén nincs kijelölt elem.
        /// </summary>
        public int KijeloltElem2
        {
            get
            {
                return _KijeloltElem2;
            }

            set
            {
                _KijeloltElem2 = value;
            }
        }

        /// <summary>
        /// Új elem kijelölése.
        /// </summary>
        /// <param name="ablak">0: baloldal 1: jobb oldal</param>
        /// <param name="elem">A kijelolendő elem indexe.</param>
        public void ElemKijeloles(int ablak, int elem)
        {
            if (ablak == 0)
            {
                _TobbszorosenKijeloltElemek1.Add(elem);
            }
            else
            {
                _TobbszorosenKijeloltElemek2.Add(elem);
            }
        }

        /// <summary>
        /// Kijelölés eltávolítása
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="elem">Kijelölés visszavonására ítélt elem indexe.</param>
        public void ElemKijelolesTorles(int ablak, int elem)
        {
            if (ablak == 0)
            {
                for (int i = 0; i < _TobbszorosenKijeloltElemek1.Count; i++)
                {
                    if (elem == _TobbszorosenKijeloltElemek1[i])
                    {
                        _TobbszorosenKijeloltElemek1.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _TobbszorosenKijeloltElemek2.Count; i++)
                {
                    if (elem == _TobbszorosenKijeloltElemek2[i])
                    {
                        _TobbszorosenKijeloltElemek2.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// Összes kijelölés visszavonása.
        /// </summary>
        /// <param name="ablak1">Baloldali kijelöléseket visszavonja-e.</param>
        /// <param name="ablak2">Jobboldali kijelöléseket visszavonja-e.</param>
        public void ElemKijelolesUrites(bool ablak1, bool ablak2)
        {
            if (ablak1)
            {
                _TobbszorosenKijeloltElemek1.Clear();
            }
            else
            {
                _TobbszorosenKijeloltElemek2.Clear();
            }
        }

        /// <summary>
        /// Az adott elem kivan-e jelölve.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="elem">Keresendő elem indexe.</param>
        /// <returns>false: nincs benne, true: benne van</returns>
        public bool KijeloltElemBenneVanE(int ablak, int elem)
        {
            if (ablak == 0)
            {
                for (int i = 0; i < _TobbszorosenKijeloltElemek1.Count; i++)
                {
                    if (_TobbszorosenKijeloltElemek1[i] == elem)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _TobbszorosenKijeloltElemek2.Count; i++)
                {
                    if (_TobbszorosenKijeloltElemek2[i] == elem)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Többszörös kijelölés listája üres-e.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <returns>false: nem üres, true: üres</returns>
        public bool TobbszorosKijelolesUresE(int ablak)
        {
            if (ablak == 0)
            {
                if (_TobbszorosenKijeloltElemek1.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (_TobbszorosenKijeloltElemek2.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Összes elem kijelölése
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        public void OsszesKijelol(int ablak)
        {
            if (ablak == 0)
            {
                ElemKijelolesUrites(true, false);

                for (int i = 0; i < FajlLista1.Count; i++)
                {
                    ElemKijeloles(0, i);
                }
            }
            else
            {
                ElemKijelolesUrites(false, true);

                for (int i = 0; i < FajlLista2.Count; i++)
                {
                    ElemKijeloles(1, i);
                }
            }

            VM.ElemKijelolOsszes(BeallitasVezerlo.Instance.AktualisAblak, Brushes.Red);
        }
        
        /// <summary>
        /// Bal oldalt kijelölt elemek indexének listája többszörös kijelölés esetén.
        /// </summary>
        public List<int> TobbszorosenKijeloltElemek1
        {
            get
            {
                return _TobbszorosenKijeloltElemek1;
            }
        }

        /// <summary>
        /// Jobb oldalt kijelölt elemek indexének listája többszörös kijelölés esetén.
        /// </summary>
        public List<int> TobbszorosenKijeloltElemek2
        {
            get
            {
                return _TobbszorosenKijeloltElemek2;
            }
        }

        /// <summary>
        /// Mappa-e.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="index">Kijelölt elem indexe.</param>
        /// <returns>false: fájl, true: mappa</returns>
        public bool IsDir(int ablak, int index)
        {
            if (ablak == 0)
            {
                string kiterjesztes = FajlLista1[index].Kiterjesztes;

                if (kiterjesztes != "")
                {
                    kiterjesztes = "." + kiterjesztes;
                }

                return IsDir(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[index].FajlNev + kiterjesztes);
            }
            else
            {
                string kiterjesztes = FajlLista2[index].Kiterjesztes;

                if (kiterjesztes != "")
                {
                    kiterjesztes = "." + kiterjesztes;
                }

                return IsDir(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[index].FajlNev + kiterjesztes);
            }
        }
        /// <summary>
        /// Mappa-e.
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        /// <returns>false: fájl, true: mappa</returns>
        public bool IsDir(string eleresiUt)
        {
            return Directory.Exists(eleresiUt);
        }

        /// <summary>
        /// Fájl megnyitása
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <param name="index">Fájl indexe.</param>
        public void FajlMegnyitas(int ablak, int index)
        {
            if (ablak == 0)
            {
                string kiterjesztes = FajlLista1[index].Kiterjesztes;

                if (kiterjesztes != "")
                {
                    kiterjesztes = "." + kiterjesztes;
                }

                FajlMegnyitas(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[index].FajlNev + kiterjesztes);
            }
            else
            {
                string kiterjesztes = FajlLista2[index].Kiterjesztes;

                if (kiterjesztes != "")
                {
                    kiterjesztes = "." + kiterjesztes;
                }

                FajlMegnyitas(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[index].FajlNev + kiterjesztes);
            }
        }

        /// <summary>
        /// Fájl megnyitása
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        public void FajlMegnyitas(string eleresiUt)
        {
            string[] temp = eleresiUt.Split('/');
            string teljesFajlNev = temp[temp.Length - 1];
            eleresiUt = "";

            for (int i = 0; i < temp.Length - 1; i++)
            {
                eleresiUt = eleresiUt + temp[i] + "/";
            }

            FajlMegnyitas(eleresiUt, teljesFajlNev);
        }

        /// <summary>
        /// Fájl megnyitása.
        /// </summary>
        /// <param name="eleresiUt">Elérési út fájlnév nélkül.</param>
        /// <param name="teljesFajlNev">Fájlnév</param>
        public void FajlMegnyitas(string eleresiUt, string teljesFajlNev)
        {
            string[] temp = teljesFajlNev.Split('.');
            string kiterjesztes = temp[temp.Length - 1];
            string fajlNev = "";

            for (int i = 0; i < temp.Length - 1; i++)
            {
                fajlNev += temp[i];

                if (i < temp.Length - 2)
                {
                    fajlNev += ".";
                }
            }

            FajlMegnyitas(eleresiUt, fajlNev, kiterjesztes);
        }

        /// <summary>
        /// Fájl megnyitása
        /// </summary>
        /// <param name="eleresiUt">Elérési út fájlnév nélkül.</param>
        /// <param name="fajlNev">Fájlnév kiterjesztés nélkül.</param>
        /// <param name="kiterjesztes">Kiterjesztés.</param>
        public void FajlMegnyitas(string eleresiUt, string fajlNev, string kiterjesztes)
        {
            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = eleresiUt + fajlNev + "." + kiterjesztes;
                p.Start();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("A fájl nem található vagy nincs jogosultsága a fájl megnyitásához.");
            }
        }

        /// <summary>
        /// Fájllista lekérdezése.
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        /// <returns>Talált fájlok listája adatokkal együtt.</returns>
        public List<FajlAdatok> GetFajlLista(string eleresiUt)
        {
            List<FajlAdatok> adatok = new List<FajlAdatok>();
            FajlAdatok vissza = GetMappaAdatok(eleresiUt + "..");
            adatok.Add(vissza);
            string[] mappak;
            bool ujra = false;

            do
            {
                ujra = false;

                if (!Directory.Exists(eleresiUt))
                {
                    ujra = true;

                    if (eleresiUt.Length <= 4)
                    {
                        System.Windows.MessageBox.Show("Az ön által választott meghajtót nem olvasható válasszon másikat!");
                        return new List<FajlAdatok>();
                    }

                    if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                    {
                        BeallitasVezerlo.Instance.AktualisMappa1 = Visszalep(BeallitasVezerlo.Instance.AktualisMappa1);
                        VM.AktualisMappa1 = BeallitasVezerlo.Instance.AktualisMappa1;
                        eleresiUt = BeallitasVezerlo.Instance.AktualisMappa1;
                    }
                    else
                    {
                        BeallitasVezerlo.Instance.AktualisMappa2 = Visszalep(BeallitasVezerlo.Instance.AktualisMappa2);
                        VM.AktualisMappa2 = BeallitasVezerlo.Instance.AktualisMappa2;
                        eleresiUt = BeallitasVezerlo.Instance.AktualisMappa2;
                    }
                }
            } while (ujra);

            try
            {
                mappak = Directory.GetDirectories(eleresiUt);
                int mappadb = mappak.Length;

                for (int i = 0; i < mappadb; i++)
                {
                    FajlAdatok adat = GetMappaAdatok(mappak[i]);
                    adatok.Add(adat);
                }
            }
            catch (Exception e)
            {
                if (eleresiUt.Length <= 4)
                {
                    System.Windows.MessageBox.Show("Az ön által választott meghajtót nem olvasható válasszon másikat!");
                    return new List<FajlAdatok>();
                }

                System.Windows.MessageBox.Show(e.Message);

                if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                {
                    BeallitasVezerlo.Instance.AktualisMappa1 = Visszalep(BeallitasVezerlo.Instance.AktualisMappa1);
                    VM.AktualisMappa1 = BeallitasVezerlo.Instance.AktualisMappa1;
                    return GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa1);
                }
                else
                {
                    BeallitasVezerlo.Instance.AktualisMappa2 = Visszalep(BeallitasVezerlo.Instance.AktualisMappa2);
                    VM.AktualisMappa2 = BeallitasVezerlo.Instance.AktualisMappa2;
                    return GetFajlLista(BeallitasVezerlo.Instance.AktualisMappa2);
                }
            }

            string[] fajlok = Directory.GetFiles(eleresiUt);
            int fajldb = fajlok.Length;

            for (int i = 0; i < fajldb; i++)
            {
                FajlAdatok adat = GetFajlAdatok(fajlok[i]);
                adatok.Add(adat);
            }

            return adatok;
        }

        /// <summary>
        /// Visszalépés tallózásnál.
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <returns>Elérési út visszázás után.</returns>
        private string Visszalep(string eleresiUt)
        {
            string[] temp = eleresiUt.Split('/');
            eleresiUt = "";

            for (int i = 0; i < temp.Length - 2; i++)
            {
                eleresiUt = eleresiUt + temp[i] + "/";
            }

            return eleresiUt;
        }

        /// <summary>
        /// Elérési út egyszerűsítése -> nem lesz benne ./ és ../
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        /// <returns>Formázott elérési út.</returns>
        public string EleresiUtFormazas(string eleresiUt)
        {
            FajlMuveletek fm = new FajlMuveletek();
            eleresiUt = fm.EleresiUtFormazas(eleresiUt);

            return eleresiUt;
        }

        /// <summary>
        /// Egy fájl adatai.
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        /// <returns>A fájl adatai.</returns>
        public FajlAdatok GetFajlAdatok(string eleresiUt)
        {
            string fajlNev = eleresiUt.Remove(0, eleresiUt.Count());

            string[] temp = eleresiUt.Split('/');
            fajlNev = temp[temp.Length - 1];

            eleresiUt = "";

            for (int i = 0; i < temp.Length - 1; i++)
            {
                eleresiUt += temp[i];
                eleresiUt += "/";
            }

            return GetFajlAdatok(eleresiUt, fajlNev);
        }

        /// <summary>
        /// Egy fájl adatai.
        /// </summary>
        /// <param name="eleresiUt">Elérési út fájlnév nélkül.</param>
        /// <param name="teljesFajlNev">Fájlnév.</param>
        /// <returns>A fájl adatai.</returns>
        public FajlAdatok GetFajlAdatok(string eleresiUt, string teljesFajlNev)
        {
            string fajlNev = "";
            string kiterjesztes = "";
            string[] temp = teljesFajlNev.Split('.');

            if (temp.Length > 1)
            {
                kiterjesztes = temp[temp.Length - 1];

                for (int i = 0; i < temp.Length - 1; i++)
                {
                    fajlNev += temp[i];

                    if (i < temp.Length - 2)
                    {
                        fajlNev += ".";
                    }
                }
            }
            else
            {
                fajlNev = teljesFajlNev;
            }

            FajlAdatok adat = GetFajlAdatok(eleresiUt, fajlNev, kiterjesztes);

            return adat;
        }

        /// <summary>
        /// Egy fájl adatai.
        /// </summary>
        /// <param name="eleresiUt">Elérési út fájlnév nélkül.</param>
        /// <param name="fajlNev">Fájlnév kiterjesztés nélkül.</param>
        /// <param name="kiterjesztes">Kiterjesztés.</param>
        /// <returns>A fájl adatai.</returns>
        public FajlAdatok GetFajlAdatok(string eleresiUt, string fajlNev, string kiterjesztes)
        {
            string[] temp = fajlNev.Split('.');

            FajlAdatok adat = new FajlAdatok();
            adat.FajlNev = fajlNev;
            adat.Kiterjesztes = kiterjesztes;
            adat.TeljesFajlnev = fajlNev + "." + kiterjesztes;

            FileInfo f = new FileInfo(eleresiUt + fajlNev + "." + kiterjesztes);

            adat.Meret = f.Length.ToString();
            adat.Datum = f.CreationTime;
            adat.Attr = "";
            adat.isDir = false;

            return adat;
        }

        /// <summary>
        /// Egy mappa adatai.
        /// </summary>
        /// <param name="eleresiUt">Elérési út.</param>
        /// <returns>A mappa adatai.</returns>
        public FajlAdatok GetMappaAdatok(string eleresiUt)
        {
            string mappaNev = eleresiUt.Remove(0, eleresiUt.Count());

            string[] temp = eleresiUt.Split('/');
            mappaNev = temp[temp.Length - 1];

            eleresiUt = "";

            for (int i = 0; i < temp.Length - 1; i++)
            {
                eleresiUt += temp[i];
                eleresiUt += "/";
            }

            return GetMappaAdatok(eleresiUt, mappaNev);
        }

        /// <summary>
        /// Egy mappa adatai.
        /// </summary>
        /// <param name="eleresiUt">Elérési út eleje. (utolsó rész nélkül)</param>
        /// <param name="mappaNev">Elérési út vége.</param>
        /// <returns>A mappa adatai.</returns>
        public FajlAdatok GetMappaAdatok(string eleresiUt, string mappaNev)
        {
            FajlAdatok adat = new FajlAdatok();
            adat.TeljesFajlnev = mappaNev;
            adat.FajlNev = mappaNev;
            adat.Kiterjesztes = "";
            adat.Meret = "<dir>";
            adat.Datum = Directory.GetCreationTime(eleresiUt + mappaNev);
            adat.Attr = "";
            adat.isDir = true;

            return adat;
        }

        /// <summary>
        /// Bal vagy jobb oldali fájlkezelőnél az elérési út színének lekérdezése.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        /// <returns>szín</returns>
        public string GetAktualisMappaKijeloles(int ablak)
        {
            string szin = "";

            if (ablak == 0)
            {
                if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                {
                    //sötét
                    szin = "#99B4D1";
                }
                else
                {
                    //világos
                    szin = "#BFCDDB";
                }
            }
            else
            {
                if (BeallitasVezerlo.Instance.AktualisAblak == 1)
                {
                    //sötét
                    szin = "#99B4D1";
                }
                else
                {
                    //világos
                    szin = "#BFCDDB";
                }
            }

            return szin;
        }

        /// <summary>
        /// Aktuális ablak beállítása.
        /// </summary>
        /// <param name="ablak">0: bal oldal, 1: jobb oldal</param>
        public void SetAktualisAblak(int ablak)
        {
            BeallitasVezerlo.Instance.AktualisAblak = ablak;
        }

        /// <summary>
        /// Entergombra reagálás, belépés a mappába, vagy fájlmegnyitás.
        /// </summary>
        public void EnterGomb()
        {
            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                ElemKijelolesUrites(true, false);
                VM.Megnyitas(0, KijeloltElem1);
            }
            else
            {
                ElemKijelolesUrites(false, true);
                VM.Megnyitas(1, KijeloltElem2);
            }
        }

        /// <summary>
        /// Új mappa
        /// </summary>
        public void F7()
        {
            VM.F7();
        }

        /// <summary>
        /// Frissítés.
        /// </summary>
        public void CtrlR()
        {
            VM.CtrlR();
        }

        /// <summary>
        /// Lomtárba küldés.
        /// </summary>
        public void Delete()
        {
            VM.Delete();
        }

        /// <summary>
        /// Törlés.
        /// </summary>
        public void ShiftDelete()
        {
            VM.ShiftDelete();
        }

        /// <summary>
        /// Adatok előkésztése törléshez.
        /// </summary>
        /// <param name="lomtar">false: végleges törlés, true: lomtárba küldés</param>
        public void Torles(bool lomtar)
        {
            FajlMuveletek fm = new FajlMuveletek(VM);

            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                if (TobbszorosenKijeloltElemek1.Count == 0)
                {
                    string kiterjesztes = "";

                    if (!FajlLista1[KijeloltElem1].isDir)
                    {
                        kiterjesztes = "." + FajlLista1[KijeloltElem1].Kiterjesztes;
                    }

                    fm.Torles(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[KijeloltElem1].FajlNev + kiterjesztes, lomtar);
                }
                else
                {
                    for (int i = 0; i < TobbszorosenKijeloltElemek1.Count; i++)
                    {
                        string kiterjesztes = "";

                        if (!FajlLista1[TobbszorosenKijeloltElemek1[i]].isDir)
                        {
                            kiterjesztes = "." + FajlLista1[TobbszorosenKijeloltElemek1[i]].Kiterjesztes;
                        }

                        fm.Torles(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[TobbszorosenKijeloltElemek1[i]].FajlNev + kiterjesztes, lomtar);
                    }
                }
            }
            else
            {
                if (TobbszorosenKijeloltElemek2.Count == 0)
                {
                    string kiterjesztes = "";

                    if (!FajlLista2[KijeloltElem2].isDir)
                    {
                        kiterjesztes = "." + FajlLista2[KijeloltElem2].Kiterjesztes;
                    }

                    fm.Torles(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[KijeloltElem2].FajlNev + kiterjesztes, lomtar);
                }
                else
                {
                    for (int i = 0; i < TobbszorosenKijeloltElemek2.Count; i++)
                    {
                        string kiterjesztes = "";

                        if (!FajlLista2[TobbszorosenKijeloltElemek2[i]].isDir)
                        {
                            kiterjesztes = "." + FajlLista2[TobbszorosenKijeloltElemek2[i]].Kiterjesztes;
                        }

                        fm.Torles(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[TobbszorosenKijeloltElemek2[i]].FajlNev + kiterjesztes, lomtar);
                    }
                }
            }

            VM.Frissites();
        }

        /// <summary>
        /// A kijelölt mappa/fájl felvétele a többszörös kijelölés listájába.
        /// </summary>
        public void Space()
        {
            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                if (KijeloltElemBenneVanE(0, KijeloltElem1))
                {
                    ElemKijelolesTorles(0, KijeloltElem1);
                    VM.Elemkijelol(Brushes.Black);
                }
                else
                {
                    ElemKijeloles(0, KijeloltElem1);
                    VM.Elemkijelol(Brushes.Red);
                }
            }
            else
            {
                if (KijeloltElemBenneVanE(1, KijeloltElem2))
                {
                    ElemKijelolesTorles(0, KijeloltElem2);
                    VM.Elemkijelol(Brushes.Black);
                }
                else
                {
                    ElemKijeloles(1, KijeloltElem2);
                    VM.Elemkijelol(Brushes.Red);
                }
            }
        }

        /// <summary>
        /// Másolás indítása.
        /// </summary>
        public void Masolas(bool athelyezes)
        {
            if (BeallitasVezerlo.Instance.AktualisAblak == 0)
            {
                if (TobbszorosKijelolesUresE(0))
                {
                    if (!FajlLista1[KijeloltElem1].TeljesFajlnev.Contains(".."))
                    {
                        FajlMuveletek fm = new FajlMuveletek(VM);
                        fm.Masolas(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[KijeloltElem1].TeljesFajlnev, BeallitasVezerlo.Instance.AktualisMappa2, athelyezes);
                    }
                }
                else
                {
                    List<string> eleresiUt = new List<string>();

                    for (int i = 0; i < TobbszorosenKijeloltElemek1.Count; i++)
                    {
                        if (!FajlLista1[TobbszorosenKijeloltElemek1[i]].TeljesFajlnev.Contains(".."))
                        {
                            eleresiUt.Add(BeallitasVezerlo.Instance.AktualisMappa1 + FajlLista1[TobbszorosenKijeloltElemek1[i]].TeljesFajlnev);
                        }
                    }

                    FajlMuveletek fm = new FajlMuveletek(VM);
                    fm.Masolas(eleresiUt, BeallitasVezerlo.Instance.AktualisMappa2, athelyezes);
                }
            }
            else
            {
                if (TobbszorosKijelolesUresE(1))
                {
                    if (!FajlLista2[KijeloltElem2].TeljesFajlnev.Contains(".."))
                    {
                        FajlMuveletek fm = new FajlMuveletek(VM);
                        fm.Masolas(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[KijeloltElem2].TeljesFajlnev, BeallitasVezerlo.Instance.AktualisMappa1, athelyezes);
                    }
                }
                else
                {
                    List<string> eleresiUt = new List<string>();

                    for (int i = 0; i < TobbszorosenKijeloltElemek2.Count; i++)
                    {
                        if (!FajlLista2[TobbszorosenKijeloltElemek2[i]].TeljesFajlnev.Contains(".."))
                        {
                            eleresiUt.Add(BeallitasVezerlo.Instance.AktualisMappa2 + FajlLista2[TobbszorosenKijeloltElemek2[i]].TeljesFajlnev);
                        }
                    }

                    FajlMuveletek fm = new FajlMuveletek(VM);
                    fm.Masolas(eleresiUt, BeallitasVezerlo.Instance.AktualisMappa1, athelyezes);
                }
            }

            VM.Frissites();
        }

        /// <summary>
        /// Összes kijelölése
        /// </summary>
        public void CtrlA()
        {
            OsszesKijelol(BeallitasVezerlo.Instance.AktualisAblak);
        }

        /// <summary>
        /// Nyilak gomb.
        /// </summary>
        /// <param name="shift">Shift gomb be van-e nyomva.</param>
        /// <param name="irany">0: felfelé nyil, 1: lefelé nyíl, 2: balra nyíl, 3: jobbra nyíl</param>
        public void Nyilak(bool shift, int irany)
        {
            if (shift)
            {
                Space();
            }
            
            if (irany == 0)
            {
                if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                {
                    if (KijeloltElem1 > 0)
                    {
                        KijeloltElem1--;
                        VM.KijeloltElem1 = KijeloltElem1.ToString();
                    }
                }
                else
                {
                    if (KijeloltElem2 > 0)
                    {
                        KijeloltElem2--;
                        VM.KijeloltElem2 = KijeloltElem2.ToString();
                    }
                }
            }
            else if (irany == 1)
            {
                if (BeallitasVezerlo.Instance.AktualisAblak == 0)
                {
                    if (KijeloltElem1 < FajlLista1.Count - 1)
                    {
                        KijeloltElem1++;
                        VM.KijeloltElem1 = KijeloltElem1.ToString();
                    }
                }
                else
                {
                    if (KijeloltElem2 < FajlLista2.Count - 1)
                    {
                        KijeloltElem2++;
                        VM.KijeloltElem2 = KijeloltElem2.ToString();
                    }
                }
            }
        }

        public void MeghajtoValtas(int ablak, string meghajto)
        {
            if (ablak == 0)
            {
                BeallitasVezerlo.Instance.AktualisAblak = 0;
                BeallitasVezerlo.Instance.AktualisMappa1 = meghajto;
                KijeloltElem1 = 0;
                ElemKijelolesUrites(true, false);
                VM.AktualisMappa1 = meghajto;
                FajlLista1 = GetFajlLista(meghajto);
                VM.FajlLista1 = new ObservableCollection<FajlAdatok>(FajlLista1);
            }
            else
            {
                BeallitasVezerlo.Instance.AktualisAblak = 1;
                BeallitasVezerlo.Instance.AktualisMappa2 = meghajto;
                KijeloltElem2 = 0;
                ElemKijelolesUrites(false, true);
                VM.AktualisMappa2 = meghajto;
                FajlLista2 = GetFajlLista(meghajto);
                VM.FajlLista2 = new ObservableCollection<FajlAdatok>(FajlLista2);
            }
        }
    }
}
