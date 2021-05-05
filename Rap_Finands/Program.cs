using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Rap_Finands
{
    /**
    Dette BANK PROGRAM ER LAVET af Konrad Sommer! Copy(c) Right All rights reserveret 2020
    idé og udtænkt af Anne Dam for Voldum Bank I/S
    Rap Finands
    **/
    class Program
    {
        public const string reginummer = "4242";
        public static HashSet<string> kontonumre = new HashSet<string>();  // Will contain all account. Allows us to check for duplicates.
        public static string datafil = Directory.GetCurrentDirectory() + "/bank.json"; //Her ligger alt data i
        public static List<Konto> konti;
        static void Main(string[] args)
        {
            Console.WriteLine("Henter alt kontodata");

            Hent(); // Loads accounts into 'konti'
            if (konti.Count == 0)
            {
                Konto k = new Konto("Ejvind Møller");
                konti.Add(k);

                GemTrans(k, "Opsparing", 100);
                GemTrans(konti[0], "Vandt i klasselotteriet", 1000);
                GemTrans(konti[0], "Hævet til Petuniaer", -50);

                GemTilFil();
            }
            DosStart();

        }
        static void DosStart()
        {
            Console.WriteLine("Velkommen til Rap Finans af Konrad Sommer");
            Console.WriteLine("Hvad vil du gøre nu?");

            bool blivVedogved = true;
            while (blivVedogved)
            {
                Console.WriteLine("1. Opret ny konto");
                Console.WriteLine("2. Hæv/sæt ind");
                Console.WriteLine("3. Se en oversigt");
                Console.WriteLine("0. Afslut");

                Console.Write(">");
                string valg = Console.ReadLine();

                // Changed switch/case to switch on string input, as to contain all exception handling in the default case. 

                switch (valg)
                {
                    case "1":
                        DosOpretKonto();
                        break;
                    case "2":
                        DosOpretTransaktion(DosFindKonto());
                        break;
                    case "3":
                        DosUdskrivKonto(DosFindKonto());
                        break;
                    case "0":
                        blivVedogved = false;
                        break;
                    default:
                        Console.WriteLine("UGYLDIGT VALGT!!");
                        Console.ReadKey();
                        break;

                }
            }
            Console.Clear();
        }
        static Konto DosFindKonto()
        {
            for (int i = 1; i <= konti.Count; i++)
            {
                Konto konto = konti[i - 1];  // Store the current account to ease readability of the string.
                Console.WriteLine(i + ". " + konto.registreringsnr + " " + konto.kontonr + " ejes af " + konto.ejer);
            }
            Console.WriteLine($"Skriv et tal fra 1 til {konti.Count}, for at vælge en konto.");
            Console.Write(">");
            int tal = 0;
            while (true)
            {
                try
                {
                    tal = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Ugyldigt tal! Prøv igen: ");
                }
            }
            
            if (tal < 1 || tal > konti.Count)
            {
                Console.WriteLine("Ugyldigt valg");
                Console.Clear();
                return null;
            }
            return konti[tal - 1];
        }
        static void DosOpretTransaktion(Konto k)
        {
            Console.Write("Indtast transaktion beskrivelse: ");
            string tekst = Console.ReadLine();
            Console.Write("Indtast transaktion beløb: ");
            float amount = 0;
            try
            {
                amount = float.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Ugyldigt tal!");
                return;
            }
            
            if (GemTrans(k, tekst, amount))
            {
                Console.WriteLine("Transkationen blev gemt. Ny saldo på kontoen: " + FindSaldo(k));
                GemTilFil();
            }
            else
                Console.WriteLine("Transaktionen kunne ikke gemmes (Der var sikkert ikke penge nok på kontoen)");
        }
        static Konto DosOpretKonto()
        {
            Console.Write("Navn på kontoejer:");
            Konto k = new Konto(Console.ReadLine());
            
            Console.WriteLine("Konto oprettet!");
            konti.Add(k);
            GemTilFil();
            return k;
        }

        /*
        fed metode til at lave helt nye kontonumre ~Konrad
        */
        public static string LavEtKontoNummer()
        {
            Random tilfael = new Random();
            string nr = tilfael.Next(1, 9).ToString();
            for (int i = 1; i <= 9; i++)
            {
                nr = nr + tilfael.Next(0, 9).ToString();
                if (i == 3) nr = nr + " ";
                if (i == 6) nr = nr + " ";
            }
            if (kontonumre.Contains(nr)) return LavEtKontoNummer();  // Avoid generating duplicate numbers.
            return nr;
        }
        static void DosUdskrivKonto(Konto k)
        {
            Console.WriteLine("Konto for " + k.ejer + ": " + k.registreringsnr + " " + k.kontonr);
            Console.WriteLine("================");
            Console.WriteLine("Tekst\t\t\t\tBeløb\t\tSaldo");
            foreach (Transaktion t in k.transaktioner)
            {
                Console.Write(t.tekst + "\t\t\t\t");
                Console.Write(t.amount + "\t\t");
                Console.WriteLine(t.saldo);
            }
            Console.WriteLine("================\n");

        }

        public static bool GemTrans(Konto konto, string tekst, float beløb)
        {
            float saldo = FindSaldo(konto);
            if (saldo + beløb < 0) return false;
            Transaktion t = new Transaktion();
            t.tekst = tekst;
            t.amount = beløb;
            t.saldo = t.amount + saldo;
            t.dato = DateTime.Now;

            konto.transaktioner.Add(t);
            return true;
        }
        public static float FindSaldo(Konto k)
        {
            Transaktion seneste = new Transaktion();
            DateTime senesteDato = DateTime.MinValue;
            foreach (Transaktion t in k.transaktioner)
            {
                if (t.dato > senesteDato)
                {
                    senesteDato = t.dato;
                    seneste = t;
                }
            }
            return seneste.saldo;
        }
        public static void GemTilFil()
        {
            File.WriteAllText(datafil, JsonConvert.SerializeObject(konti));
            // Removed deletion of the .json file.
        }
        public static void Hent()
        {
            if (File.Exists(datafil))
            {
                string json = File.ReadAllText(datafil);
                konti = JsonConvert.DeserializeObject<List<Konto>>(json);
            }
            else
            {
                konti = new List<Konto>();
            }

            foreach (Konto konto in konti)  // Add accounts to a HashSet, such that we can check for duplicates.
                kontonumre.Add(konto.kontonr);
        }
    }
}
/** 
Koden er lavet til undervisningbrug på TECHCOLLEGE
Voldum Bank og nævnte personer er fiktive.
~Simon Hoxer Bønding
**/
