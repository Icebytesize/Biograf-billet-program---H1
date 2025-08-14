using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf_billet_program.Moduler
{
    internal class Settings
    {
        public static string[] Byer = { "København", "Odense", "Århus", "Aalborg" };
        public static string[] Film = { "Mission Impossible: Dead Recokning", "Sinners", "Superman", "Fantastic Four: First Steps", "Wicked", "Sorry to bother you" };
        public static string[] Tidspunkt = { "08:00", "10:00", "12:00", "14:00", "16:00", "18:00", "20:00", "22:00" };
        public static bool IsLoggetIn, IsAdmin, MenuKøre, VælgPladserKøre, OpretBrugerMenu;
        public static string AktivBruger, Password, NyBruger, ForsøgtBrugernavn, ForsøgtPassword;
        public static int MenuInput, ByInput, FilmInput, TidspunktInput, SædeInput, BrugerId, MidlertidigId, AntalRækker = 12, AntalSæderPrRække = 20;


        public static string[,,,,] Pladser = new string[Byer.Length, Film.Length, Tidspunkt.Length, AntalRækker, AntalSæderPrRække]; //5D array Byer, Film, Tidspunkt, rækker, sæder
        
        /// <summary>
        /// En metode der retunere et input, kaldes på hvilken input type der gerne vil instilles
        /// </summary>
        /// <returns></returns>
        public static int SetInput()
        {
            int.TryParse(Console.ReadLine(), out int input);
            return input;
        }


        /// <summary>
        /// Metode der enden vælger eller fravælger sæder, samt fortæller hvis sædet i forvejen er optaget for en bruger der er logget in
        /// </summary>
        public static void SetLoggetInPlads()
        {
            int række = (SædeInput - 1) / AntalSæderPrRække; 
            int sæde = (SædeInput - 1 ) % AntalSæderPrRække;

            if (Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] != null && Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] == BrugerId.ToString())
            {
                Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] = null;
            }
            else if (Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] == null)
            {
                Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] = BrugerId.ToString();
            }
            else 
            {
                Message.ClearScreen();
                Message.OptagetPlads();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Metode der enden vælger eller fravælger sæder, samt fortæller hvis sædet i forvejen er optaget for en bruger der ikke er logget in
        /// </summary>
        public static void SetIkkeLoggetInPlads()
        {
            int række = (SædeInput - 1) / AntalSæderPrRække;
            int sæde = (SædeInput -1) % AntalSæderPrRække;

            if (Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] != null && Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] == MidlertidigId.ToString())
            {
                Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] = null;
            }
            else if (Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] == null)
            {
                Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, række, sæde] = MidlertidigId.ToString();
            }
            else
            {
                Message.ClearScreen();
                Message.OptagetPlads();
                Console.ReadKey();
            }
        }


        /// <summary>
        /// En metode der ændre en ikke logget ins bookninger til et ID nummer uden for registeret, sådan billet er booket, men det ikke ser ud som om den næste der er midlertigt har booket den
        /// </summary>
        public static void ÆndreMidlertidigBookninger()
        {
            for (int i = 0; i < AntalRækker; i++)
            {
                for(int j = 0; j < AntalSæderPrRække; j++)
                {
                    if (Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, i, j] == MidlertidigId.ToString())
                    {
                        Pladser[ByInput - 1, FilmInput - 1, TidspunktInput - 1, i, j] = "1000";
                    }
                }
            }
        }
    }
}
