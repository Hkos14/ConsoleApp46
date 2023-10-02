using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Monitorok
    {
        public string Gyarto { get; set; }
        public string Tipus { get; set; }
        public double Meret { get; set; }
        public int NettoAr { get; set; }
        public int Afa { get; }

        public double BruttoAr => NettoAr * 1.27;
        public Monitorok(string Sor)
        {
            string[] adat = Sor.Split(';');
            Gyarto = adat[0];
            Tipus = adat[1];
            Meret = double.Parse(adat[2]);
            NettoAr = int.Parse(adat[3]);
            Afa = 27;
        }
        public override string ToString()
        {
            return (" Gyártó:" + Gyarto + " | Típus:" + Tipus + " | Méret:" + Meret + " col |Nettó Ár " + NettoAr + "Ft");
        }



    }
}

