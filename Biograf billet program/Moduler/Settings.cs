using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biograf_billet_program.Moduler
{
    internal class Settings
    {

        public static bool isLoggetIn, isAdmin, menuKøre, bookinKøre, opretBrugerMenu;
        public static string aktivBruger, password, nyBruger, forsøgtBrugernavn, forsøgtPassword;
        public static int menuInput, byInput, filmInput, sædeInput, brugerId;


        /// <summary>
        /// En metode der retunere et input, kaldes på hvilken input type der gerne vil instilles
        /// </summary>
        /// <returns></returns>
        public static int SetInput()
        {
            int.TryParse(Console.ReadLine(), out int input);
            return input;
        }

    }
}
