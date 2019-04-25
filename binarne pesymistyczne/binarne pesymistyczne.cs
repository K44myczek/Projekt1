using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekt_wyszukiwanie_binarne_pesymistyczne
{
    class Program
    {
        private static int iter = 10;
        private static int licznik;//licznik ilosci operacji
        private static int nr = 0;//wypisuje ktora tabela sie wykonuje
        public static int Binarne(int[] tabBin, int szukanaliczba, int tabdlugosc)//wyszukiwanie bez instrumentacji
        {
            int lewa = 0;
            int prawa = tabdlugosc - 1;
            int srodek = 0;
            while (lewa <= prawa)
            {
                srodek = (lewa + prawa) / 2;
                if (tabBin[srodek] == szukanaliczba)
                {
                    return srodek;
                }
                else if (tabBin[srodek] < szukanaliczba)
                {
                    lewa = srodek + 1;
                }
                else
                {
                    prawa = srodek - 1;
                }
            }
            return -1;
        }
        public static int Binarneinstrumentacja(int[] tableBin, int szukanaliczba, int tabdlugosc)//z instrumentacja
        {
            int lewa = 0;
            int prawa = tabdlugosc - 1;
            int srodek = 0;
            licznik = 0;
            while (lewa <= prawa)
            {
                licznik++;
                srodek = (lewa + prawa) / 2;
                if (tableBin[srodek] == szukanaliczba)
                {
                    licznik++;
                    return srodek;
                }
                else if (tableBin[srodek] < szukanaliczba)
                {
                    licznik++;
                    lewa = srodek + 1;
                }
                else
                {
                    licznik++;
                    prawa = srodek - 1;
                }
            }
            return -1;
        }
        static void Main(String[] args)
        {
            int indeks = 0;//indeks w ktorym znajduje sie szukana liczba
            long TimeElapsed = 0;
            long iterTimeElapsed;
            int szukana;//szukana liczba
            double ElapsedSeconds;
            Console.WriteLine("nr;rozmiar;szukana;indeks;ilosc operacji;czas;");
            for (int j = 5368709; j < 268435450; j += 5368709)
            {
                ++nr;
                int[] table = new int[j];
                for (int i = 0; i < table.Length; i++)
                {
                    table[i] = i + 1;
                }
                szukana = table.Length + 1;
                Array.Sort(table);
                for (int i = 0; i < iter + 2; ++i)
                {
                    long start = Stopwatch.GetTimestamp();//stoper start
                    indeks = Binarne(table, szukana, table.Length);//wyszukiwane z instrumentacja
                    long stop = Stopwatch.GetTimestamp();//stoper stop
                    iterTimeElapsed = stop - start;//czas obliczamy za pomoca roznicy
                    TimeElapsed = iterTimeElapsed;
                    indeks = Binarneinstrumentacja(table, szukana, table.Length);//wyszukiwane z instrumentacja
                }
                ElapsedSeconds = TimeElapsed * (1.0 / (iter * Stopwatch.Frequency));
                Console.WriteLine(nr + ";" + j + ";" + szukana + ";" + indeks + ";" + licznik + ";" + (ElapsedSeconds.ToString("F12")));
            }
		Console.ReadKey();
        }
    }
}