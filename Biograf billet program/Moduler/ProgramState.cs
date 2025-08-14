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
            Settings.MenuKøre = true;
            while (Settings.MenuKøre)
            {
                Settings.MenuInput = 0;
                Message.ClearScreen();
                Message.HovedMenu();
                Settings.MenuInput = Settings.SetInput();
                Message.ClearScreen();

                if (Settings.MenuInput == 1)
                {
                    Bookning();
                }

                else if (Settings.MenuInput == 2 && !Settings.IsLoggetIn)
                {
                    UserManager.LogIn();
                }

                else if (Settings.MenuInput == 2 && Settings.IsLoggetIn)
                {
                    UserManager.LogUd();
                }

                else if (Settings.MenuInput == 3 && !Settings.IsLoggetIn)
                {
                    UserManager.OpretBruger();
                }

                else if (Settings.MenuInput == 3 && Settings.IsLoggetIn)
                {
                    UserManager.SeBookninger();
                }

                else if (Settings.MenuInput == 4 && Settings.IsAdmin)
                {
                    Message.ClearScreen();
                    Message.PrintMessage("Alle bookninger er nu nulstillet");
                    UserManager.NulstilAlleBookningere();
                    Console.ReadKey();
                    
                }

                else if (Settings.MenuInput == 9)
                {
                    Message.PrintMessage("Program afslutte\nHav en god dag");
                    Console.ReadKey();
                    Settings.MenuKøre = false;
                }

                else
                {
                    Message.ErrorMessage();
                    Console.ReadKey();
                }



            }

            
        }


        /// <summary>
        /// Den store metode til at bookning af 
        /// </summary>
        public static void Bookning() 
        {
            Settings.ByInput = 0;
            Settings.FilmInput = 0;
            Settings.TidspunktInput = 0;
            Message.VælgBy();
            Settings.ByInput = Settings.SetInput();

            if (Settings.ByInput > 0 && Settings.ByInput <= Settings.Byer.Length+1)
            {
                Message.ClearScreen();
                Message.VælgFilm();
                Settings.FilmInput = Settings.SetInput();

                if (Settings.FilmInput > 0 && Settings.FilmInput <= Settings.Film.Length + 1)
                {
                    Message.ClearScreen();
                    Message.VælgTidspunkt();
                    Settings.TidspunktInput = Settings.SetInput();

                    if (Settings.TidspunktInput > 0 && Settings.TidspunktInput <= Settings.Tidspunkt.Length + 1)
                    {
                        Message.ClearScreen();
                        VælgPladser();
                    }


                    else
                            {
                        Message.ClearScreen();
                        Message.ErrorMessage();
                        Console.ReadKey();
                    }
                }


                else
                {
                    Message.ClearScreen();
                    Message.ErrorMessage();
                    Console.ReadKey();
                }
            }
            else
            {
                Message.ClearScreen();
                Message.ErrorMessage();
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Den overovrnede metode der køre mens der bliver valgt pladser
        /// </summary>
        public static void VælgPladser()
        {
            Settings.VælgPladserKøre = true;

            while (Settings.VælgPladserKøre)
            {
                Message.ClearScreen();
                Message.VisSal();
                Settings.SædeInput = Settings.SetInput();
                if (Settings.SædeInput == 999)
                {
                    if (!Settings.IsLoggetIn) Settings.ÆndreMidlertidigBookninger();
                    UserManager.GemBookninger();
                    Settings.VælgPladserKøre = false;
                }
                else if (Settings.SædeInput > 0 && Settings.SædeInput <= 240)
                {
                    if (Settings.IsLoggetIn)
                    {
                        Settings.SetLoggetInPlads();
                    }
                    else if (!Settings.IsLoggetIn)
                    {
                        Settings.SetIkkeLoggetInPlads();
                    }
                }
                else
                {
                    Message.ClearScreen();
                    Message.ErrorMessage();
                    Console.ReadKey();
                }
            }
        }
    }
}
