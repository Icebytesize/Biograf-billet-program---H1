using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Biograf_billet_program.Moduler
{
    internal class ProgramState
    {
        public static void HovedMenu()
        {
            Settings.menuKøre = true;
            while (Settings.menuKøre)
            {
                Settings.menuInput = 0;
                Message.ClearScreen();
                Message.HovedMenu();
                Settings.menuInput = Settings.SetInput();
                Message.ClearScreen();

                if (Settings.menuInput == 1)
                {
                    // ProgramState.Bookning()
                }

                else if (Settings.menuInput == 2 && !Settings.isLoggetIn)
                {
                    UserManager.LogIn();
                }

                else if (Settings.menuInput == 2 && Settings.isLoggetIn)
                {
                    UserManager.LogUd();
                }

                else if (Settings.menuInput == 3 && !Settings.isLoggetIn)
                {
                    UserManager.OpretBruger();
                }

                else if (Settings.menuInput == 3 && Settings.isLoggetIn)
                {
                    // ProgramState.SeBookningere()
                }

                else if (Settings.menuInput == 4 && Settings.isAdmin)
                {
                    // ProgramState.NulStilAlleBookningere
                }

                else if (Settings.menuInput == 9)
                {
                    Message.PrintMessage("Program afslutte\nHav en god dag");
                    Console.ReadKey();
                    Settings.menuKøre = false;
                }

                else
                {
                    Message.ErrorMessage();
                    Console.ReadKey();
                }



            }

            
        }

     
    }
}
