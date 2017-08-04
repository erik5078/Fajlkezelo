using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

using System.ComponentModel;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Másolás vezérlése
    /// </summary>
    class MasolasVezerlo
    {
        /// <summary>
        /// Összes fájl mérete.
        /// </summary>
        private long osszMeret;

        /// <summary>
        /// Hol tart a másolás az összes fájl szemszögéből.
        /// </summary>
        private long tartMeret;

        /// <summary>
        /// Fájlok mérete egyenként listában.
        /// </summary>
        private List<long> meret;

        /// <summary>
        /// Mappák listája.
        /// </summary>
        private List<string> mappak;

        /// <summary>
        /// Fájlok listája.
        /// </summary>
        private List<string> fajlok;

        /// <summary>
        /// Fájlok neve és relatív útvonala.
        /// </summary>
        private List<string> fajlNevek;

        /// <summary>
        /// Kijelölt elemek.
        /// </summary>
        private List<string> regiEleresiUt;

        /// <summary>
        /// Cél
        /// </summary>
        private string ujEleresiUt;

        /// <summary>
        /// Másolás ablaának megnyitásához kell.
        /// </summary>
        View.MasolasWindow masolasWindow;

        /// <summary>
        /// A másolás viewmodeljének meghívásához kell.
        /// </summary>
        private ViewModels.VMMasolas VM;

        /// <summary>
        /// Fájlok száma
        /// </summary>
        private int fileDb;

        /// <summary>
        /// Már átmásolt fálok száma.
        /// </summary>
        private int tartDb;

        public MasolasVezerlo()
        {
            osszMeret = 0;
            tartMeret = 0;
            meret = new List<long>();
            mappak = new List<string>();
            fajlok = new List<string>();
            fajlNevek = new List<string>();
            regiEleresiUt = new List<string>();
            ujEleresiUt = "";
            fileDb = 0;
            tartDb = 0;
        }

        public MasolasVezerlo(ViewModels.VMMasolas vm)
        {
            VM = vm;
            osszMeret = 0;
            tartMeret = 0;
            meret = new List<long>();
            mappak = new List<string>();
            fajlok = new List<string>();
            fajlNevek = new List<string>();
            regiEleresiUt = new List<string>();
            ujEleresiUt = "";
            fileDb = 0;
            tartDb = 0;
        }

        /// <summary>
        /// Elérési utak formázása, másolás ablakának megnyitása.
        /// </summary>
        /// <param name="regiEleresiUt">Forrás</param>
        /// <param name="ujEleresiUt">Cél</param>
        public void MasolasAblakMegnyit(List<string> regiEleresiUt, string ujEleresiUt)
        {
            this.regiEleresiUt = regiEleresiUt;
            this.ujEleresiUt = ujEleresiUt;
            Models.FajlMuveletek fm = new FajlMuveletek();

            for (int i = 0; i < regiEleresiUt.Count; i++)
            {
                regiEleresiUt[i] = fm.EleresiUtFormazas(regiEleresiUt[i]);
                if (regiEleresiUt[i].Contains("./") || regiEleresiUt[i].EndsWith(".."))
                {
                    return;
                }
            }

            ujEleresiUt = fm.EleresiUtFormazas(ujEleresiUt);

            if (ujEleresiUt.Contains("./") || ujEleresiUt.EndsWith(".."))
            {
                return;
            }

            masolasWindow = new View.MasolasWindow(regiEleresiUt, ujEleresiUt);
            masolasWindow.DataContext = ViewModels.VMMasolas.Instance;
            masolasWindow.ShowDialog();
        }

        /// <summary>
        /// Másolás végrehajtása.
        /// </summary>
        /// <param name="regiEleresiUt">Forrás</param>
        /// <param name="ujEleresiUt">Cél</param>
        /// <param name="bw">BackgroundWorker példány, ide küldi a státuszinformációkat.</param>
        public void Masolas(List<string> regiEleresiUt, string ujEleresiUt, BackgroundWorker bw)
        {
            this.ujEleresiUt = ujEleresiUt;
            tartDb = 0;
            VM.FajlAdatMegjelenito(fileDb, 0, "", "");
            Felulir felulirStatusz = Felulir.Alaphelyzet;
            
            //A fájlok és mappák megkeresése, összméret kiszámolása
            for (int i = 0; i < regiEleresiUt.Count; i++)
            {
                if (Directory.Exists(regiEleresiUt[i]))
                {
                    string[] temp = regiEleresiUt[i].Split('/');

                    try
                    {
                        Directory.CreateDirectory(@ujEleresiUt + temp[temp.Length - 2]);
                    }
                    catch (Exception e)
                    {
                        
                    }

                    string[] temp2 = regiEleresiUt[i].Split('/');
                    string foMappa = "";

                    for (int j = 0; j < temp2.Length - 2; j++)
                    {
                        foMappa = foMappa + temp2[j] + "/";
                    }

                    MappaBejar(regiEleresiUt[i], foMappa);
                }
                else
                {
                    fajlok.Add(regiEleresiUt[i]);

                    string[] temp = regiEleresiUt[i].Split('/');
                    fajlNevek.Add(temp[temp.Length - 1]);

                    FileInfo f = new FileInfo(@regiEleresiUt[i]);
                    meret.Add(f.Length);
                    osszMeret += meret[meret.Count - 1];
                    fileDb++;
                }
            }

            //Mappák létrehozása
            for (int i = 0; i < mappak.Count; i++)
            {
                try
                {
                    Directory.CreateDirectory(@mappak[i]);
                }
                catch (Exception e)
                {

                }
            }

            //Fájlok másolása
            //Összes fájl mérete kilobyte-ban
            osszMeret = 0;

            for (int i = 0; i < meret.Count; i++)
            {
                osszMeret += meret[i];
            }

            long osszMaradekKiloBytes = osszMeret % 1024;
            long osszKiloBytes = osszMeret - osszMaradekKiloBytes;
            osszKiloBytes = osszKiloBytes / 1024;
            VM.FajlAdatMegjelenito(fileDb, 0, "", "");

            for (int i = 0; i < fajlok.Count; i++)
            {
                VM.FajlAdatMegjelenito(fileDb, tartDb, @fajlok[i], @ujEleresiUt + @fajlNevek[i]);
                bool letezik = false;

                if (felulirStatusz == Felulir.Kihagy || felulirStatusz == Felulir.Felulir)
                {
                    felulirStatusz = Felulir.Alaphelyzet;
                }

                if (File.Exists(@ujEleresiUt + @fajlNevek[i]))
                {
                    letezik = true;

                    if (felulirStatusz != Felulir.MindetFelulir && felulirStatusz != Felulir.MindetKihagy)
                    {
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            View.FelulirWindow felulirWindow = new View.FelulirWindow();
                            ViewModels.WMFelulir.Instance.SetFelulirLabel(@ujEleresiUt + @fajlNevek[i]);
                            felulirWindow.ShowDialog();
                            felulirStatusz = ViewModels.WMFelulir.Instance.FelulirProperty;
                            ViewModels.WMFelulir.Instance.ReInstance();
                        }));
                    }
                }

                if (felulirStatusz == Felulir.Megse)
                {
                    return;
                }

                BinaryReader reader = new BinaryReader(new FileStream(@fajlok[i], FileMode.Open), Encoding.ASCII);
                BinaryWriter writer;

                //ÖSSZES BYTE
                int nBytes = (int)reader.BaseStream.Length;
                int maradekBytes = nBytes % 1048576;
                int megaBytes = nBytes - maradekBytes;
                megaBytes = megaBytes / 1048576;
                int maradekBytesKiloByte = nBytes % 1024;
                int kiloBytes = nBytes - maradekBytesKiloByte;
                kiloBytes = kiloBytes / 1024;

                int tart = 0;
                int elozotart = 0;
                
                if (felulirStatusz == Felulir.Alaphelyzet || felulirStatusz == Felulir.Felulir || felulirStatusz == Felulir.MindetFelulir)
                {
                    writer = new BinaryWriter(new FileStream(@ujEleresiUt + @fajlNevek[i], FileMode.Create));
                    writer.Close();
                }

                writer = new BinaryWriter(new FileStream(@ujEleresiUt + @fajlNevek[i], FileMode.Append));

                if ((felulirStatusz == Felulir.Kihagy || felulirStatusz == Felulir.MindetKihagy) && letezik)
                {
                    tartMeret += kiloBytes;
                    double szazalekOsszesVege = 0;

                    if (osszKiloBytes != 0)
                    {
                        szazalekOsszesVege = Convert.ToDouble(tartMeret) / Convert.ToDouble(osszKiloBytes);
                    }

                    szazalekOsszesVege = Math.Round(szazalekOsszesVege);
                    ViewModels.VMMasolas.Instance.OsszesProgressbar(szazalekOsszesVege);
                    bw.ReportProgress(100);
                    tartDb++;
                    VM.FajlAdatMegjelenito(fileDb, tartDb, @fajlok[i], @ujEleresiUt + @fajlNevek[i]);

                    reader.Close();
                    writer.Close();
                }
                else
                {
                    for (int j = 0; j < megaBytes; j++)
                    {
                        writer.Write(reader.ReadBytes(1048576));

                        double szazalek = Convert.ToDouble(tart) / Convert.ToDouble(kiloBytes);
                        szazalek = szazalek * 100;
                        szazalek = Math.Round(szazalek);

                        double szazalekOsszes = Convert.ToDouble(tartMeret) / Convert.ToDouble(osszKiloBytes);
                        szazalekOsszes = szazalekOsszes * 100;
                        szazalekOsszes = Math.Round(szazalekOsszes);

                        if (bw.CancellationPending)
                        {
                            writer.Close();
                            reader.Close();
                            return;
                        }

                        if (elozotart != szazalek)
                        {
                            int temp = (int)szazalek;

                            ViewModels.VMMasolas.Instance.OsszesProgressbar(szazalekOsszes);
                            
                            bw.ReportProgress(temp);
                            elozotart = temp;
                        }

                        tart = tart + 1024;
                        tartMeret = tartMeret + 1024;
                    }

                    for (int j = 0; j < maradekBytes; j++)
                    {
                        writer.Write(reader.ReadByte());

                        if (maradekBytes % 1024 == 0)
                        {
                            tart++;
                        }
                    }

                    if (bw.CancellationPending)
                    {
                        writer.Close();
                        reader.Close();
                        return;
                    }


                    double szazalekOsszesVege = 0;

                    if (osszKiloBytes != 0)
                    {
                        szazalekOsszesVege = Convert.ToDouble(tartMeret) / Convert.ToDouble(osszKiloBytes);
                    }

                    szazalekOsszesVege = szazalekOsszesVege * 100;
                    szazalekOsszesVege = Math.Round(szazalekOsszesVege);

                    ViewModels.VMMasolas.Instance.OsszesProgressbar(szazalekOsszesVege);
                    bw.ReportProgress(100);
                    tartDb++;
                    VM.FajlAdatMegjelenito(fileDb, tartDb, @fajlok[i], @ujEleresiUt + @fajlNevek[i]);

                    writer.Close();
                    reader.Close();
                }

                if (felulirStatusz == Felulir.Kihagy || felulirStatusz == Felulir.Felulir)
                {
                    felulirStatusz = Felulir.Alaphelyzet;
                }
            }

            tartMeret = 0;
            osszMeret = 0;
            osszKiloBytes = 0;
            osszMaradekKiloBytes = 0;
        }

        /// <summary>
        /// Mappa bejárása és a talált fájlok adatainak mentése, ha mappát talál, újra meghívja magát (rekúrzív hívás).
        /// </summary>
        /// <param name="eleresiUt">Elérési út</param>
        /// <param name="foMappa">Eltárolja az eredeti mappát, ahonnan indul a bejárás</param>
        private void MappaBejar(string eleresiUt, string foMappa)
        {
            if (eleresiUt.Contains(".."))
            {
                return;
            }

            string[] fajl = Directory.GetFiles(eleresiUt);

            for (int i = 0; i < fajl.Length; i++)
            {
                FileInfo f = new FileInfo(fajl[i]);
                meret.Add(f.Length);
                osszMeret += meret[meret.Count - 1];
                fajlok.Add(fajl[i]);

                string temp = fajl[i].Remove(0, foMappa.Length);

                fajlNevek.Add(temp);
                fileDb++;
            }

            string[] mappa = Directory.GetDirectories(eleresiUt);

            for (int i = 0; i < mappa.Length; i++)
            {
                string eredeti = mappa[i];
                mappa[i] = mappa[i].Replace('\\', '/');
                mappa[i] = mappa[i].Replace(foMappa, ujEleresiUt);

                if (mappa[i] != "." && mappa[i] != "..")
                {
                    mappak.Add(mappa[i] + "/");
                    MappaBejar(eredeti + "/", foMappa);
                }
            }
        }
    }
}
