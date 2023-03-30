using System;
using System.IO;

namespace VerzioniranjeDN1
{
    
    class Program
    {
        // PREVERI POT DATOTEKE
        public static string pridobiImeDatoteke(string potDoDatoteke)
        {
            // Preverimo ali obstaja datoteka v dani poti
            if (!File.Exists(potDoDatoteke))
            {
                return "";
            }

            return Path.GetFileName(potDoDatoteke);
        }

        // PREBERI DATOTEKO IN ZAPISI V POLJE
        public static int[] preberiDatoteko(string imeDatoteke)
        {
            char delilniZnak = ' ';
            int velikostPoljaA = 0;
            int indexPoljaA = 0;

            // PRESTEJEMO ST. ZAPISOV STEVIL V DATOTEKI
            try
            {
                using (StreamReader sr = new StreamReader(imeDatoteke))
                {
                    string vrstica;

                    while ((vrstica = sr.ReadLine()) != null)
                    {
                        string[] stevilaVrstice = vrstica.Split(delilniZnak);

                        velikostPoljaA += stevilaVrstice.Length;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Napaka pri branju iz datoteke: " + e.Message);
            }

            // kreiramo polje A velikosti ki smo jo prebrali iz datoteke
            int[] A = new int[velikostPoljaA];

            // NAPOLNIMO POLJE A
            try
            {
                using (StreamReader sr = new StreamReader(imeDatoteke))
                {
                    string vrstica;

                    while ((vrstica = sr.ReadLine()) != null)
                    {
                        // razdelimo vrstico datoteke po delimetru: ' '
                        string[] stevilaVrstice = vrstica.Split(delilniZnak);

                        foreach (string stevilo in stevilaVrstice)
                        {
                            A[indexPoljaA] = int.Parse(stevilo);
                            indexPoljaA++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Napaka pri branju iz datoteke: " + e.Message);
            }

            return A;
        }

        // IZPIS POLJA - KONZOLA
        public static void izpisPolja(int[] polje, string imePolja)
        {
            Console.Write(imePolja + ":");
            for (int i = 0; i < polje.Length; i++)
            {
                Console.Write(" " + polje[i].ToString());
            }
            Console.WriteLine("");
        }

        // RADIX SORT
        public static int[] RadixSort(int[] A)
        {
            // k predstavlja index bita v 8 bitnem stevilu
            for (int k = 0; k < 8; k++)
            {
                // Vzamemo k-ti bit od vsakega števila v polju A in ga shranimo v polje D
                int[] D = new int[A.Length];
                for (int i = 0; i < A.Length; i++)
                {
                    D[i] = (A[i] >> k) & 1;
                }

                // TODO - Implementacija Counting Sorta
            }

            return A;
        }

        // GLAVNI PROGRAM
        static void Main(string[] args)
        {
            string potDoDatoteke = args[0];
            string imeDatoteke = pridobiImeDatoteke(potDoDatoteke);

            if (imeDatoteke != "")
            {
                // OK
                int[] A = preberiDatoteko(imeDatoteke);

                //izpisPolja(A, "Neurejeno polje");

                RadixSort(A);

            }
            else
            {
                // ERR
                Console.WriteLine("Dana datoteka ne obstaja - preverite zapis poti");
            }
        }
    }
}
