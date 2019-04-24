using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Projekt_wyszukiwanie_liniowe_pesymistyczne
{
    class Program
    {
        private static int iter = 10;
        private static int licznik;//licznik ilosci operacji
        private static int szukana;//szukana liczba
        static int wyszukiwanieliniowe(int[] tab, int num)//bez instumentcji
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
        private static int wyszukiwanieliniowepoinstumentacji(int[] tab, int num)//z instrumentacja
        {
            licznik = 0;
            for (int i = 0; i < tab.Length; i++)
            {
                ++licznik;

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
            Console.WriteLine("nr;rozmiar;szukana;nr indeksu;ilosc operacji;czas");
            int nr = 0;
            for (int j = 5368709; j <= 268435450; j += 5368709)//maksymalny rozmiar tablicy to 2 do potegi 28, program wykonuje 50 pomiarow
            {
                int[] table = new int[j];
                szukana = table.Length + 1;
                for (int i = 0; i < table.Length; i++)
                {
                    table[i] = i + 1;
                }
                for (int i = 0; i < iter + 2; i++)
                {
                    long start = Stopwatch.GetTimestamp();//stoper start
                    indeks = wyszukiwanieliniowe(table, szukana);//wyszukiwanie bez instrumentacji
                    long stop = Stopwatch.GetTimestamp();//stoper stop
                    iterTimeElapsed = stop - start;//czas obliczamy poprzez roznice
                    TimeElapsed = iterTimeElapsed;
                    indeks = wyszukiwanieliniowepoinstumentacji(table, szukana);//z instrumentacja
                }
                ElapsedSeconds = TimeElapsed * (1.0 / (iter * Stopwatch.Frequency));
                Console.WriteLine(++nr + "; " + j + ";" + szukana + ";" + indeks + ";" + licznik + ":" + (ElapsedSeconds.ToString("F4")));
            }
            Console.ReadKey();
        }
    }
}

