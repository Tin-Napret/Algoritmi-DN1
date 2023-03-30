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

                CountingSort(D, A);
            }

            return A;
        }

        // COUNTING SORT
        public static void CountingSort(int[] D, int[] A)
        {
            int[] C = new int[2];
            foreach (int d in D)
            {
                C[d]++;
            }
            C[1] += C[0];

            int[] B = new int[A.Length];
            for (int i = A.Length - 1; i >= 0; i--)
            {
                int d = D[i];
                int j = C[d] - 1;
                B[j] = A[i];
                C[d]--;
            }

            // Popravimo vrstni red števil v polju A glede na indekse sortiranih bitov
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = B[i];
            }
        }

        // IZPIS POLJA - IZHODNA DATOTEKA
        public static void zapisivDatoteko(int[] A)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("out.txt"))
                {
                    foreach (int stevilo in A)
                    {
                        sw.Write(stevilo + " ");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Napaka pri zapisu v datoteko: " + e.Message);
            }
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

                //izpisPolja(A, "Urejeno polje");

                zapisivDatoteko(A); // zapisi v datoteko out.txt

            }
            else
            {
                // ERR
                Console.WriteLine("Dana datoteka ne obstaja - preverite zapis poti");
            }
        }
    }
}
