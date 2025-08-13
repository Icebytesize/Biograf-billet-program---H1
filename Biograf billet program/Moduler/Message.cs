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
            if (Settings.isLoggetIn) PrintMessage($"Brugernavn: {Settings.aktivBruger}\n");
            PrintMessage(
                "===================================\n" +
                "           Velkommen til           \n" +
                "Matthias' fiktive biograf franchise\n" +
                "         billet bestilling         \n" +
                "===================================\n" +
                "   Du har nu følgende muligheder   \n\n" +
                "1: Bestil billet\n");
            if (!Settings.isLoggetIn) PrintMessage("2: Log in\n" +
                "3: Opret bruger\n");
            else if (Settings.isLoggetIn) PrintMessage("2: Log ud\n" +
                "3: Se bookninger\n");
            if (Settings.isAdmin) PrintMessage("4: Nulstil alle bookninger\n");
            PrintMessage("9: Afslut program\n\n" +
                "> ");
        }


    }
}
