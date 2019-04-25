using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekt_wyszukiwanie_binarne_średnie
{
    class Program2
    {
        private static int iter = 10;
        private static int licznik;//licznik ilosci wykonywanych operacji
        private static ulong dlugosc;//ilosc elementow
        private static ulong wynik;//suma elementow
        private static int nr = 0;//wypisuje kolejnosc tablic

        public static int Binarne(int[] tableBin, int szukana, int tabdlugosc)//bez instrumentacji
        {
            int lewo = 0;
            int prawo = tabdlugosc - 1;
            int srodek = 0;
            while (lewo <= prawo)
            {
                srodek = (lewo + prawo) / 2;
                if (tableBin[srodek] == szukana)
                {
                    return srodek;
                }
                else if (tableBin[srodek] < szukana)
                {
                    lewo = srodek + 1;
                }
                else
                {

                    prawo = srodek - 1;
                }
            }
            return -1;
        }
        public static int Binarneinstrumentacja(int[] tableBin, int szukana, int tabdlugosc)//z instrumentacja
        {
            int lewo = 0;
            int prawo = tabdlugosc - 1;
            int srodek = 0;
            wynik = 0;
            licznik = 0;
            dlugosc = 0;
            while (lewo <= prawo)
            {
                licznik++;
                srodek = (lewo + prawo) / 2;
                wynik += (ulong)licznik * (ulong)Math.Pow(2, licznik - 1);
                dlugosc += (ulong)Math.Pow(2, licznik - 1);
                if (tableBin[srodek] == szukana)
                {
                    licznik++;
                    wynik += (ulong)licznik * (ulong)Math.Pow(2, licznik - 1);
                    dlugosc += (ulong)Math.Pow(2, licznik - 1);
                    wynik = wynik / dlugosc;
                    return srodek;
                }
                else if (tableBin[srodek] < szukana)
                {
                    licznik++;
                    wynik += (ulong)licznik * (ulong)Math.Pow(2, licznik - 1);
                    dlugosc += (ulong)Math.Pow(2, licznik - 1);
                    lewo = srodek + 1;
                }
                else
                {
                    licznik++;
                    wynik += (ulong)licznik * (ulong)Math.Pow(2, licznik - 1);
                    dlugosc += (ulong)Math.Pow(2, licznik - 1);
                    prawo = srodek - 1;
                }
            }
            return -1;
        }
        static void Main()
        {
            int indeks = 0;//indeks w którym znajduje się szukana
            long TimeElapsed = 0;
            long iterTimeElapsed;
            int szukana;
            double ElapsedSeconds;
            Console.WriteLine("nr;rozmiar;szukana;indeks;ilosc operacji;czas;zlozonosc");
            for (int j = 5368709; j <= 268435450; j += 5368709)//maksymalny rozmiar tablicy to 2 do potegi 28, 50 punktow pomiarowych
            {
                ++nr;
                int[] table = new int[j];
                for (int i = 0; i < table.Length; i++)
                {
                    table[i] = i + 1;
                }
                szukana = table.Length - 1;
                Array.Sort(table);
                for (int i = 0; i < iter + 2; ++i)
                {
                    long start = Stopwatch.GetTimestamp();//stoper start
                    indeks = Binarne(table, szukana, table.Length);//bez instrumentacji
                    long stop = Stopwatch.GetTimestamp();//stoper stop
                    iterTimeElapsed = stop - start;//czas obliczamy za pomoca roznicy
                    TimeElapsed = iterTimeElapsed;
                    indeks = Binarneinstrumentacja(table, szukana, table.Length);//z instrumentacja
                }
                ElapsedSeconds = TimeElapsed * (1.0 / (iter * Stopwatch.Frequency));
                Console.WriteLine(nr + ";" + j + ";" + szukana + ";" + indeks + ";" + licznik + ";" + (ElapsedSeconds.ToString("F12")) + ";" + wynik);       
            }
            Console.ReadKey();
        }
    }
}