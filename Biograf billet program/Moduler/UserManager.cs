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
        private static string bookningFile = "bookning.txt";




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

            Settings.BrugerId = 1;
            if (linjer.Count > 0)
            {
                var sidsteId = linjer
                    .Select(l => int.Parse(l.Split(';')[0]))
                    .Max();
                Settings.BrugerId = sidsteId + 1;
            }

            Message.PrintMessage("Indtast brugernavn: ");
            Settings.AktivBruger = Console.ReadLine();

            Message.PrintMessage("Indtast password: ");
            Settings.Password = Console.ReadLine();

            Settings.IsAdmin = false;

            Settings.OpretBrugerMenu = true;
            while (Settings.OpretBrugerMenu)
            {
                Settings.MenuInput = 0;
                Message.ClearScreen();
                Message.PrintMessage($"Ønsker du at oprette denne bruger?" +
                    $"\nBrugernavn: {Settings.AktivBruger}" +
                    $"\nPassword:   {Settings.Password}" +
                     "\n\n1: Opret" +
                     "\n2: Annuler" +
                     "\n\n> ");
                Settings.MenuInput = Settings.SetInput();

                if (Settings.MenuInput == 1) // Hvis ønsket, opretter og gemmer ny bruger
                {
                    Settings.NyBruger = $"{Settings.BrugerId};{Settings.AktivBruger};{Settings.Password};{Settings.IsAdmin}";
                    linjer.Add(Settings.NyBruger);

                    File.WriteAllLines(userFile, linjer);

                    Settings.IsLoggetIn = true;
                    Settings.NyBruger = null;
                    Settings.OpretBrugerMenu = false;

                    Message.ClearScreen();
                    Message.PrintMessage($"Tilykke {Settings.AktivBruger} du er nu oprettet i systemet og vil blive sendt tilbage til hovedmenuen");
                    Console.ReadKey();
                }

                else if (Settings.MenuInput == 2)
                {
                    Settings.AktivBruger = null;
                    Settings.Password = null;
                    Settings.OpretBrugerMenu = false;
                    Settings.BrugerId = 0;

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
            Settings.ForsøgtBrugernavn = Console.ReadLine();
            Message.PrintMessage("Password: ");
            Settings.ForsøgtPassword = Console.ReadLine();

            var linjer = File.ReadAllLines(userFile);

            var bruger = linjer
                .Select(l => l.Split(';'))
                .FirstOrDefault(f => f[1] == Settings.ForsøgtBrugernavn && f[2] == Settings.ForsøgtPassword);

            if (bruger != null)
            {
                Settings.BrugerId = int.Parse(bruger[0]);
                Settings.IsAdmin = bool.Parse(bruger[3]);
                Settings.IsLoggetIn = true;
                Settings.AktivBruger = Settings.ForsøgtBrugernavn;
                Settings.ForsøgtBrugernavn = null;
                Settings.ForsøgtPassword = null;

                Message.ClearScreen();
                Message.PrintMessage($"Velkommen {Settings.AktivBruger} du er nu logget in");
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
            Settings.AktivBruger = null;
            Settings.Password = null;
            Settings.IsLoggetIn = false;
            Settings.BrugerId = 0;
            Settings.IsAdmin = false;

            Message.PrintMessage("Du er blevet logget af");
            Console.ReadKey();

        }

        /// <summary>
        /// Metode til at gemme bookninger til en fil
        /// </summary>
        public static void GemBookninger() 
        {
            if(!File.Exists(bookningFile))
            {
                File.WriteAllText(bookningFile, "");
            }

            using (StreamWriter sw = new StreamWriter(bookningFile))
            {
                for (int by = 0; by < Settings.Byer.Length; by++)
                {
                    for (int film = 0; film < Settings.Film.Length; film++)
                    {
                        for (int tid = 0; tid < Settings.Tidspunkt.Length; tid++)
                        {
                            for (int række = 0; række < Settings.AntalRækker; række++)
                            {
                                for (int sæde = 0; sæde < Settings.AntalSæderPrRække; sæde++)
                                {
                                    string booking = Settings.Pladser[by, film, tid, række, sæde];
                                    if (!string.IsNullOrEmpty(booking)) // kun gem bookede pladser
                                    {
                                        sw.WriteLine($"{by},{film},{tid},{række},{sæde},{booking}");
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Metode der indlæser tidligere bookninger fra en fil
        /// </summary>
        public static void IndlæsBookninger()
        {
            if (!File.Exists(bookningFile))
            {
                return;
            }

            string[] linjer = File.ReadAllLines(bookningFile);

            foreach (string linje in linjer)
            {
                string[] dele = linje.Split(',');

                if (dele.Length != 6) continue;

                int by = int.Parse(dele[0]);
                int film = int.Parse(dele[1]);
                int tid = int.Parse(dele[2]);
                int række = int.Parse(dele[3]);
                int sæde = int.Parse(dele[4]);
                string brugerId = dele[5];

                Settings.Pladser[by, film, tid, række, sæde] = brugerId;
            }
        }
    }
}

