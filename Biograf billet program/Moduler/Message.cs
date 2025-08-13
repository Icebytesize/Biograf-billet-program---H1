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
            Console.WriteLine(msg);
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


    }
}
