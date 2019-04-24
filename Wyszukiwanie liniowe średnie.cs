using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekt_wyszukiwanie_liniowe
{
    class Program
    {
        private static int iter = 10;
        private static int licznik;//licznik operacji
        private static int szukana;//szukana liczba
        private static long suma;//suma elementów tablicy
        static int wyszukiwanieliniowe(int[] tab, int num)//Wyszukiwanie przed instumentacją.
        {
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == num)
                {
                    return i;
                }
            }
            return -1;
        }
        private static int wyszukiwanieliniowepoinstumentacji(int[] tab, int num)//wyszukiwanie z instrumentacją.
        {
            licznik = 0;
            suma = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                ++licznik;
                suma += (long)tab[i];
                if (tab[i] == num)
                {
                    return i;
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            int indeks = 0;
            long TimeElapsed = 0;
            long iterTimeElapsed;
            double ElapsedSeconds;
            Console.WriteLine("nr;rozmiar;szukana;nr indeksu;ilosc operacji;czas;zlozonosc");
            int nr = 0;
            for (int j = 5368709; j <= 268435450; j += 5368709)//maksymalna wielkość tablicy to 2 do potegi 28, zwiększa się tak by uzyskać 50 pomiarów.
            {
                int[] table = new int[j];
                for (int i = 0; i < table.Length; i++)
                {
                    table[i] = i + 1;
                }
                for (int i = 0; i < iter + 2; i++)
                {
                    szukana = table.Length / 2;
                    long start = Stopwatch.GetTimestamp();//stoper start
                    indeks = wyszukiwanieliniowe(table, szukana);//liniowe bez instrumentacji
                    long stop = Stopwatch.GetTimestamp();//stoper stop
                    iterTimeElapsed = stop - start;//za pomocą róznicy obliczamy czas
                    TimeElapsed = iterTimeElapsed;
                    indeks = wyszukiwanieliniowepoinstumentacji(table, szukana);//liniowe z instumentacją
                }
                long wynik = suma / licznik;//zlozonosc
                ElapsedSeconds = TimeElapsed * (1.0 / (iter * Stopwatch.Frequency));
                Console.WriteLine(++nr + "; " + j + ";" + szukana + ";" + indeks + ";" + licznik + ":" + (ElapsedSeconds.ToString("F4")) + ";" + wynik);
            }
            Console.ReadKey();
        }
    }
}
