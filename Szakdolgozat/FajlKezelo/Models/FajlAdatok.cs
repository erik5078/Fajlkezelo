using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FajlKezelo.Models
{
    /// <summary>
    /// Fájl adatai
    /// </summary>
    class FajlAdatok
    {
        public string TeljesFajlnev { get; set; }
        public string FajlNev { get; set; }
        public string Kiterjesztes { get; set; }
        //Maximum 2 terabyte
        public string Meret { get; set; }
        public DateTime Datum { get; set; }
        public string Attr { get; set; }
        public bool isDir;
    }
}
