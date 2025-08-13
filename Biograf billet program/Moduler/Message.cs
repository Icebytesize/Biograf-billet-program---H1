using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf_billet_program.Moduler
{
    internal class Message
    {
        
        /// <summary>
        /// Metode til at skrive tekst til konsollen
        /// </summary>
        /// <param name="msg"></param>
        public static void PrintMessage(string msg)
        {
            Console.Write(msg);
        }

        /// <summary>
        /// Metode til at udskrive en fejlbesked til konsollen
        /// </summary>
        public static void ErrorMessage()
        {
            PrintMessage("Input ikke forstået, prøv igen");
        }

        /// <summary>
        /// Metode til at udskrive til konsollen hvis man prøver at booke et optaget sæde
        /// </summary>
        public static void OptagetPlads()
        {
            PrintMessage("Sæde allerede optaget, kan ikke vælges");  
        }

        /// <summary>
        /// Metode til at nulstille konsollen
        /// </summary>
        public static void ClearScreen()
        {
            Console.Clear();
        }


        /// <summary>
        /// Metode som viser hovedmenuen
        /// </summary>
        public static void HovedMenu()
        {
            if (Settings.IsLoggetIn) PrintMessage($"Brugernavn: {Settings.AktivBruger}\n");
            PrintMessage(
                "===================================\n" +
                "           Velkommen til           \n" +
                "Matthias' fiktive biograf franchise\n" +
                "         billet bestilling         \n" +
                "===================================\n" +
                "   Du har nu følgende muligheder   \n\n" +
                "1: Bestil billet\n");
            if (!Settings.IsLoggetIn) PrintMessage("2: Log in\n" +
                "3: Opret bruger\n");
            else if (Settings.IsLoggetIn) PrintMessage("2: Log ud\n" +
                "3: Se bookninger\n");
            if (Settings.IsAdmin) PrintMessage("4: Nulstil alle bookninger\n");
            PrintMessage("9: Afslut program\n\n" +
                "> ");
        }

        /// <summary>
        /// Metode til at udskrive hvilke byer man kan købe biletter til
        /// </summary>
        public static void VælgBy()
        {
            PrintMessage("Hvilken by vil du købe billet til?\n");
            for (int i = 0; i < Settings.Byer.Length; i++)
            {
                PrintMessage($"{i+1}: {Settings.Byer[i]}\n");
            }
            PrintMessage("\n> ");

        }

        /// <summary>
        /// Metode til at udskrive hvilke film man kan købe biletter til
        /// </summary>
        public static void VælgFilm()
        {
            PrintMessage("Hvilken film vil du gerne se?\n");
            for (int i = 0; i < Settings.Film.Length; i++)
            {
                PrintMessage($"{i + 1}: {Settings.Film[i]}\n");
            }
            PrintMessage("\n> ");

        }

        /// <summary>
        /// Metode til at udskrive hvilke tidspunkter man kan se filmen
        /// </summary>
        public static void VælgTidspunkt()
        {
            PrintMessage("Hornår vil du gerne se filmen?\n");
            for (int i = 0; i < Settings.Tidspunkt.Length; i++)
            {
                PrintMessage($"{i + 1}: {Settings.Tidspunkt[i]}\n");
            }
            PrintMessage("\n> ");

        }


        /// <summary>
        /// Metode til at vise den valgt sal, og hvilke sæder der er ledige, optaget og valgt
        /// </summary>
        public static void VisSal()
        {
            if (!Settings.IsLoggetIn) Settings.MidlertidigId = 100;
            PrintMessage($"   By: {Settings.Byer[Settings.ByInput - 1]}   Film: {Settings.Film[Settings.FilmInput-1]}   Klokken: {Settings.Tidspunkt[Settings.TidspunktInput-1]}\n\n");

            PrintMessage("         ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n" +
                "         ■                          Lærred                          ■\n" +
                "         ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");

            for (int i = 0; i < Settings.AntalRækker; i++)
            {
                PrintMessage("\n");
                for (int j = 0; j < Settings.AntalSæderPrRække; j++)
                {
                    int sædeNummer = (j + 1) + (i * Settings.AntalSæderPrRække);
                    string status = Settings.Pladser[Settings.ByInput - 1, Settings.FilmInput - 1, Settings.TidspunktInput - 1, i, j];

                    if (status == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green; //Fri plads
                    }
                    else if (status == Settings.BrugerId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; //Booket af logget in bruger
                    }
                    else if (!Settings.IsLoggetIn && status == Settings.MidlertidigId.ToString())
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; //Booket af midlertidig bruger
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    PrintMessage(sædeNummer.ToString("D3") + " ");
                    Console.ResetColor();
                }
            }
            PrintMessage ("\n\nIndtast 999 for at afslutte bestillingen");
            PrintMessage("\n\n> ");
        }
    }
}
