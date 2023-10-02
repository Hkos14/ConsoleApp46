using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lehetőleg minden kiírást a főprogram végezzen el. Próbálj minél több kódot újrahasznosítani. Minden feladatot meg kell oldani hagyományosan, és azután, ha tudod, LINQ-val is.

            //1. Hozz létre egy osztályt a monitorok adatai számára. Olvasd be a fájl tartalmát.

            //2. Írd ki a monitorok összes adatát virtuális metódussal, soronként egy monitort a képernyőre. A kiírás így nézzen ki:

            //Gyártó: Samsung; Típus: S24D330H; Méret: 24 col; Nettó ár: 33000 Ft
            List<Monitorok> monitor = new List<Monitorok>();
            using StreamReader sr = new StreamReader(path: @"..\..\..\monitorok.txt");
            sr.ReadLine();
            foreach (var i in File.ReadAllLines(@"..\..\..\monitorok.txt"))
            {
                monitor.Add(new Monitorok(i));
            }
            foreach (var m in monitor)
            {
                Console.WriteLine(m.ToString());
            }

            //2. Tárold az osztálypéldányokban a bruttó árat is (ÁFA: 27%, konkrétan a 27-tel számolj, ne 0,27-tel vagy más megoldással.)

            //3. Tételezzük fel, hogy mindegyik monitorból 15 db van készleten, ez a nyitókészlet. Mekkora a nyitó raktárkészlet bruttó (tehát áfával növelt) értéke?
            //Írj egy metódust, ami meghívásakor kiszámolja a raktárkészlet aktuális bruttó értékét. A főprogram írja ki az értéket.

            double osszeg = 0;

            foreach (var item in monitor)
            {
                osszeg += item.NettoAr + item.NettoAr * item.Afa / 100 * 15;

            }
            Console.WriteLine($"A 15db ára áfával együtt:{osszeg} Ft");



            //4. Írd ki egy új fájlba, és a képernyőre az 50.000 Ft feletti nettó értékű monitorok összes adatát (a darabszámmal együtt) úgy,

            //hogy a szöveges adatok nagybetűsek legyenek, illetve az árak ezer forintba legyenek átszámítva.

            //Az ezer forintba átszámítást egy külön függvényben valósítsd meg.

            static double Atvalt(double nettoAr)
            {
                return nettoAr / 1000;
            }

            static void KiirM(List<Monitorok> monitorok)
            {
                List<Monitorok> dragaMonitorok = monitorok.Where(m => m.NettoAr > 50000).ToList();

                using (StreamWriter sw = new StreamWriter(@"..\..\..\monitorok.txt"))
                {
                    foreach (Monitorok monitor in dragaMonitorok)
                    {
                        sw.WriteLine($"Gyártó: {monitor.Gyarto.ToUpper()}; Típus: {monitor.Tipus.ToUpper()}; Méret: {monitor.Meret} ; Ár: {Atvalt(monitor.NettoAr)} Ft");
                        Console.WriteLine($"Gyártó: {monitor.Gyarto.ToUpper()}; Típus: {monitor.Tipus.ToUpper()}; Méret: {monitor.Meret} ; Ár: {Atvalt(monitor.NettoAr)} Ft");
                    }
                }
            }

            //5. Egy vevő keresi a HP EliteDisplay E242 monitort. Írd ki neki a képernyőre, hogy hány darab ilyen van a készleten.

            //Ha nincs a készleten, ajánlj neki egy olyan monitort, aminek az ára az átlaghoz fölülről közelít. Ehhez használd az átlagszámító függvényt (később lesz feladat).

                foreach (var i in monitor)
                {
                    if (true)
                    {
                        i.Gyarto = "HP EliteDisplay E242";
                        Console.WriteLine($"készleten {}");
                    }
                    else
                    {

                    }
                }
            
            //6. Egy újabb vevőt csak az ár érdekli. Írd ki neki a legolcsóbb monitor méretét, és árát.
            static void Legolcsobb(List<Monitorok> monitorok)
            {
                Monitorok legolcsobb = monitorok.OrderBy(m => m.NettoAr).First();
                Console.WriteLine($" Legolcsóbb monitor: Méret: {legolcsobb.Meret}; Nettó ár: {legolcsobb.NettoAr} Ft");
            }

            //7. A cég akciót hirdet. A 70.000 Ft fölötti árú Samsung monitorok bruttó árából 5%-ot elenged.

            //Írd ki, hogy mennyit veszítene a cég az akcióval, ha az összes akciós monitort akciósan eladná.
            static double Akcios(List<Monitorok> monitorok)
            {
                double akciosAr = monitorok.Where(m => m.Gyarto.ToLower() == "samsung" && m.BruttoAr > 70000).Sum(m => m.BruttoAr * 0.95);
                double veszteseg = monitorok.Where(m => m.Gyarto.ToLower() == "samsung" && m.BruttoAr > 70000).Sum(m => m.BruttoAr) - akciosAr;
                return veszteseg;
            }


            //8. Írd ki a képernyőre minden monitor esetén, hogy az adott monitor nettó ára a nettó átlag ár alatt van-e, vagy fölötte,

            //esetleg pontosan egyenlő az átlag árral. Ezt is a főprogram írja ki.

            Console.WriteLine($"Az átlag ár {monitor.Average(m => m.NettoAr)} ");
            double atlagar = monitor.Average(m => m.NettoAr);
            foreach (var i in monitor)
            {
                if (atlagar < i.NettoAr)
                {
                    Console.WriteLine($"A adott monitor {i.Gyarto} ; {i.Tipus} az átleg ár alatt van");
                }
                else if (atlagar > i.NettoAr)
                {
                    Console.WriteLine($"A adott monitor {i.Gyarto} ; {i.Tipus} az átleg ár felett van");
                }
                else
                {
                    Console.WriteLine($"A adott monitor {i.Gyarto} ; {i.Tipus} egyenlő az átlaggal ");
                }

            }

            //9. Modellezzük, hogy megrohamozták a vevők a boltot. 5 és 15 közötti random számú vásárló 1 vagy 2 random módon kiválasztott monitort vásárol,

            //ezzel csökkentve az eredeti készletet. Írd ki, hogy melyik monitorból mennyi maradt a boltban.

            //Vigyázz, hogy nulla darab alá ne mehessen a készlet. Ha az adott monitor éppen elfogyott, ajánlj neki egy másikat (lásd fent).
            static void Vasarlas(List<Monitorok> monitorok, int darab)
            {
                Random random = new Random();
                int vasarlokSzama = random.Next(5, 16);

                for (int i = 0; i < vasarlokSzama; i++)
                {
                    Vasarlas(monitorok, random.Next(1, 3));
                }
            }
            //10. Írd ki a képernyőre, hogy a vásárlások után van-e olyan monitor, amelyikből mindegyik elfogyott (igen/nem).


            //11. Írd ki a gyártókat abc sorrendben a képernyőre. Oldd meg úgy is, hogy a metódus írja ki, és úgy is, hogy a főprogram.

            //12. Csökkentsd a legdrágább monitor bruttó árát 10%-kal, írd ki ezt az értéket a képernyőre.


        }
    }
}
