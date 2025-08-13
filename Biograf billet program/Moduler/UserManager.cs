using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Biograf_billet_program.Moduler
{
    internal class UserManager
    {
        private static string userFile = "brugere.txt";




        /// <summary>
        /// En metode til at oprette brugere og gemmen dem.
        /// </summary>
        public static void OpretBruger()
        {
            if (!File.Exists(userFile)) //Hvis fil ikke eksistere
            {
                File.WriteAllText(userFile, ""); //Opretter en ny fil
            }

            var linjer = File.ReadAllLines(userFile).ToList();

            Settings.brugerId = 1;
            if (linjer.Count > 0)
            {
                var sidsteId = linjer
                    .Select(l => int.Parse(l.Split(';')[0]))
                    .Max();
                Settings.brugerId = sidsteId + 1;
            }

            Message.PrintMessage("Indtast brugernavn: ");
            Settings.aktivBruger = Console.ReadLine();

            Message.PrintMessage("Indtast password: ");
            Settings.password = Console.ReadLine();

            Settings.isAdmin = false;

            Settings.opretBrugerMenu = true;
            while (Settings.opretBrugerMenu)
            {
                Settings.menuInput = 0;
                Message.ClearScreen();
                Message.PrintMessage($"Ønsker du at oprette denne bruger?" +
                    $"\nBrugernavn: {Settings.aktivBruger}" +
                    $"\nPassword:   {Settings.password}" +
                     "\n\n1: Opret" +
                     "\n2: Annuler" +
                     "\n\n> ");
                Settings.menuInput = Settings.SetInput();

                if (Settings.menuInput == 1) // Hvis ønsket, opretter og gemmer ny bruger
                {
                    Settings.nyBruger = $"{Settings.brugerId};{Settings.aktivBruger};{Settings.password};{Settings.isAdmin}";
                    linjer.Add(Settings.nyBruger);

                    File.WriteAllLines(userFile, linjer);

                    Settings.isLoggetIn = true;
                    Settings.nyBruger = null;
                    Settings.opretBrugerMenu = false;

                    Message.ClearScreen();
                    Message.PrintMessage($"Tilykke {Settings.aktivBruger} du er nu oprettet i systemet og vil blive sendt tilbage til hovedmenuen");
                    Console.ReadKey();
                }

                else if (Settings.menuInput == 2)
                {
                    Settings.aktivBruger = null;
                    Settings.password = null;
                    Settings.opretBrugerMenu = false;
                    Settings.brugerId = 0;

                    Message.ClearScreen();
                    Message.PrintMessage("Oprettelse annulleret, du vil nu blive sendt tilbage til hovedmenuen");
                    Console.ReadKey();

                }

                else
                {
                    Message.ClearScreen();
                    Message.ErrorMessage();
                    Console.ReadKey();
                }
            }
        }
        

        /// <summary>
        /// Metode til at logge ind på eksisterende bruger
        /// </summary>
        public static void LogIn()
        {
            if (!File.Exists(userFile))
            {
                Message.PrintMessage("Ikke forbundet til brugerregisteret, kontakt administrator");
                return;
            }

            Message.PrintMessage("Indtast dit\n" +
                "Brugernavn: ");
            Settings.forsøgtBrugernavn = Console.ReadLine();
            Message.PrintMessage("Password: ");
            Settings.forsøgtPassword = Console.ReadLine();

            var linjer = File.ReadAllLines(userFile);

            var bruger = linjer
                .Select(l => l.Split(';'))
                .FirstOrDefault(f => f[1] == Settings.forsøgtBrugernavn && f[2] == Settings.forsøgtPassword);

            if (bruger != null)
            {
                Settings.brugerId = int.Parse(bruger[0]);
                Settings.isAdmin = bool.Parse(bruger[3]);
                Settings.isLoggetIn = true;
                Settings.aktivBruger = Settings.forsøgtBrugernavn;
                Settings.forsøgtBrugernavn = null;
                Settings.forsøgtPassword = null;

                Message.ClearScreen();
                Message.PrintMessage($"Velkommen {Settings.aktivBruger} du er nu logget in");
                Console.ReadKey();

            }

            else
            {
                Message.ClearScreen();
                Message.PrintMessage("Brugernavn eller password forkert, du vil blive sendt tilbage til menuen nu.");
                Console.ReadKey();
            }

        }

        /// <summary>
        /// En metode til at logge ud og nulstille den aktive bruger
        /// </summary>
        public static void LogUd()
        {
            Settings.aktivBruger = null;
            Settings.password = null;
            Settings.isLoggetIn = false;
            Settings.brugerId = 0;
            Settings.isAdmin = false;

            Message.PrintMessage("Du er blevet logget af");
            Console.ReadKey();

        }
    }
}

