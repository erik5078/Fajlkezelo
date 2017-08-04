using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Config fájl kezelése
    /// </summary>
    class BeallitasVezerlo
    {
        private static BeallitasVezerlo _instance;

        /// <summary>
        /// Singleton
        /// </summary>
        public static BeallitasVezerlo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BeallitasVezerlo();
                }

                return _instance;
            }
        }

        private BeallitasVezerlo()
        {
            Beolvasas();
        }

        /// <summary>
        /// Beállítások beolvasása
        /// </summary>
        private void Beolvasas()
        {
            List<string> sor = new List<string>();

            //Létezik-e a konfig fájl? Ha nem létrehozzuk az alapbeállításokkal.
            bool configExist = Directory.Exists("Config");

            if (configExist)
            {
                configExist = File.Exists("Config/config.txt");
            }

            if (!configExist)
            {
                ConfigLetrehozas();
            }

            StreamReader olvaso = new StreamReader("Config/config.txt", Encoding.UTF8);

            while (!olvaso.EndOfStream)
            {
                sor.Add(olvaso.ReadLine());
            }

            olvaso.Close();

            for (int i = 0; i < sor.Count; i++)
            {
                string[] temp = sor[i].Split('=');

                string valtozo = temp[0];
                string ertek = "";

                for (int j = 1; j < temp.Length; j++)
                {
                    ertek += temp[j];

                    if (j < temp.Length - 1)
                    {
                        ertek += "=";
                    }
                }

                Beallitas(valtozo, ertek);
            }
        }

        /// <summary>
        /// Config fájl létrehozása alapbeállításokkal
        /// </summary>
        private void ConfigLetrehozas()
        {
            if (!Directory.Exists("Config"))
            {
                Directory.CreateDirectory("Config");
            }

            StreamWriter iro = new StreamWriter("Config/config.txt", false, Encoding.UTF8);

            iro.WriteLine("AktualisMappa1=C://");
            iro.WriteLine("AktualisMappa2=C://");
            iro.WriteLine("AktualisAblak=0");

            iro.Close();
        }

        /// <summary>
        /// A config fájlból beolvasott adatok változóba helyezése
        /// </summary>
        /// <param name="valtozo">Változó neve</param>
        /// <param name="ertek">Változó értéke</param>
        private void Beallitas(string valtozo, string ertek)
        {
            switch (valtozo)
            {
                case "AktualisMappa1":
                    _aktualisMappa1 = ertek;
                    break;
                case "AktualisMappa2":
                    _aktualisMappa2 = ertek;
                    break;
                case "AktualisAblak":
                    _aktualisAblak = Convert.ToInt32(ertek);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Config fájl mentése
        /// </summary>
        private void Mentes()
        {
            StreamWriter iro = new StreamWriter("Config/config.txt", false, Encoding.UTF8);

            iro.WriteLine("AktualisMappa1=" + _aktualisMappa1);
            iro.WriteLine("AktualisMappa2=" + _aktualisMappa2);
            iro.WriteLine("AktualisAblak=" + _aktualisAblak);

            iro.Close();
        }

        private string _aktualisMappa1;

        /// <summary>
        /// Bal oldali fájlkezelő aktuális mappája
        /// </summary>
        public string AktualisMappa1
        {
            get
            {
                return _aktualisMappa1;
            }

            set
            {
                _aktualisMappa1 = value;
                Mentes();
            }
        }

        private string _aktualisMappa2;
        
        /// <summary>
        /// Jobboldali fájlkezelő aktuális mappája
        /// </summary>
        public string AktualisMappa2
        {
            get
            {
                return _aktualisMappa2;
            }

            set
            {
                _aktualisMappa2 = value;
                Mentes();
            }
        }

        private int _aktualisAblak;

        /// <summary>
        /// Kijelölt fájlkezelő
        /// 0 -> bal
        /// 1 -> jobb
        /// </summary>
        public int AktualisAblak
        {
            get
            {
                return _aktualisAblak;
            }

            set
            {
                _aktualisAblak = value;
                Mentes();
            }
        }
    }
}
