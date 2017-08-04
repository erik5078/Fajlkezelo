using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Fájlműveletek kezelése
    /// </summary>
    class FajlMuveletek
    {
        ViewModels.ViewModel VM;

        public FajlMuveletek(ViewModels.ViewModel vm)
        {
            VM = vm;
        }

        public FajlMuveletek()
        {

        }

        /// <summary>
        /// Egy fájl vagy mappa másolásának előkészítése, átadja egy olyan eljárásnak, ami képes többet s kezelni.
        /// </summary>
        /// <param name="regiEleresiUt">Régi elérési út</param>
        /// <param name="ujEleresiUt">Új elérési út</param>
        public void Masolas(string regiEleresiUt, string ujEleresiUt, bool athelyezes)
        {
            if (regiEleresiUt.Contains("./") || ujEleresiUt.Contains("./") || regiEleresiUt.EndsWith("..") || ujEleresiUt.EndsWith(".."))
            {
                return;
            }

            regiEleresiUt = EleresiUtFormazas(regiEleresiUt);
            ujEleresiUt = EleresiUtFormazas(ujEleresiUt);

            List<string> adat = new List<string>();
            adat.Add(regiEleresiUt);
            Masolas(adat, ujEleresiUt, athelyezes);
        }

        /// <summary>
        /// Másolás adatinak előkészítése és a másolásvezérlő kezelése
        /// </summary>
        /// <param name="regiEleresiUt">A kijelölt fájlok/mappák listában.</param>
        /// <param name="ujEleresiUt">Új elérési út.</param>
        public void Masolas(List<string> regiEleresiUt, string ujEleresiUt, bool athelyezes)
        {
            if (regiEleresiUt.Count == 0)
            {
                return;
            }

            Models.MasolasVezerlo masolas = new Models.MasolasVezerlo();
            masolas.MasolasAblakMegnyit(regiEleresiUt, ujEleresiUt);

            if (athelyezes)
            {
                for (int i = 0; i < regiEleresiUt.Count; i++)
                {
                    Torles(regiEleresiUt[i], false);
                }
            }
        }

        /// <summary>
        /// A paraméterben megadott elérési út mappa-e.
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <returns></returns>
        public bool IsDir(string eleresiUt)
        {
            return Directory.Exists(eleresiUt);
        }

        /// <summary>
        /// Mappa létrehozása
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <param name="mappaNev">Mappa név</param>
        public void MappaLetrehoz(string eleresiUt, string mappaNev)
        {
            try
            {
                Directory.CreateDirectory(eleresiUt + mappaNev);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Nem sikerült a mappát létrehozni!");
            }
        }

        /// <summary>
        /// Fájl vagy mappa törlése. Meghívja a megfelelő metódust.
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <param name="lomtar">
        ///     false -> Végleges törlés.
        ///     true -> Lomtárba küldés
        /// </param>
        public void Torles(string eleresiUt, bool lomtar)
        {
            if (IsDir(eleresiUt))
            {
                Mappatorles(eleresiUt, lomtar);
            }
            else
            {
                FajlTorles(eleresiUt, lomtar);
            }
        }

        /// <summary>
        /// Mappatörlés
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <param name="lomtar">
        ///     false -> Végleges törlés.
        ///     true -> Lomtárba küldés
        /// </param>
        private void Mappatorles(string eleresiUt, bool lomtar)
        {
            string[] temp = eleresiUt.Split('/');

            if (temp[temp.Length - 1] == ".." || temp[temp.Length - 2] == "..")
            {
                return;
            }

            if (!Directory.Exists(eleresiUt))
            {
                System.Windows.MessageBox.Show("A mappa nem létezik vagy nincs jogosultsága törölni!");
                return;
            }
            
            if (lomtar)
            {
                try
                {
                    FileSystem.DeleteDirectory(eleresiUt, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("A mappa nem létezik vagy nincs jogosultsága törölni!");
                }
            }
            else
            {
                try
                {
                    Directory.Delete(eleresiUt, true);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("A mappa nem létezik vagy nincs jogosultsága törölni!");
                }
            }
        }

        /// <summary>
        /// Fájltörlés
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <param name="lomtar">
        ///     false -> Végleges törlés.
        ///     true -> Lomtárba küldés
        /// </param>
        private void FajlTorles(string eleresiUt, bool lomtar)
        {
            if (!File.Exists(eleresiUt))
            {
                System.Windows.MessageBox.Show("A fájl nem létezik vagy nincs jogosultsága törölni!");
                return;
            }

            if (lomtar)
            {
                try
                {
                    FileSystem.DeleteFile(eleresiUt, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("A fájl nem létezik vagy nincs jogosultsága törölni!");
                }
            }
            else
            {
                try
                {
                    File.Delete(eleresiUt);
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show("A fájl nem létezik vagy nincs jogosultsága törölni!");
                }
            }
        }

        /// <summary>
        /// Visszaadja a megadott elérési út egyszerűsített változatát -> nem lesz benne ./ és ../ rész.
        /// </summary>
        /// <param name="eleresiUt"></param>
        /// <returns></returns>
        public string EleresiUtFormazas(string eleresiUt)
        {
            string meghajto = "";

            if (eleresiUt.Length > 3)
            {
                meghajto = eleresiUt.Substring(0, 4);
            }
            else
            {
                meghajto = eleresiUt;
            }

            eleresiUt = eleresiUt.Replace(meghajto, "");

            if (eleresiUt.Length == 0)
            {
                return meghajto;
            }

            string[] temp = eleresiUt.Split('/');
            int vissza = 0;

            for (int i = temp.Length - 1; i >= 0; i--)
            {
                if (temp[i] == "..")
                {
                    temp[i] = "";
                    vissza++;
                }
                else if (temp[i] == ".")
                {
                    temp[i] = "";
                }
                else
                {
                    if (vissza > 0)
                    {
                        vissza--;
                        temp[i] = "";
                    }
                }
            }

            List<string> temp2 = new List<string>();

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != "")
                {
                    temp2.Add(temp[i]);
                }
            }

            eleresiUt = meghajto;

            for (int i = 0; i < temp2.Count; i++)
            {
                eleresiUt += temp2[i];

                if (i < temp2.Count - 1)
                {
                    eleresiUt += "/";
                }
            }
            
            if (IsDir(eleresiUt + "/"))
            {
                eleresiUt += "/";
            }


            return eleresiUt;
        }
    }
}
