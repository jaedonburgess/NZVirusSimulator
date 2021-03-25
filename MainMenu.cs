using System;
using System.Threading;

namespace NZVirusSimulator 
{

    /*
    Using jbapps Menu Template
    (Jaedon Burgess) - 2021
    */

    class MainMenu
    {
        // Draw main menu interface
        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle();
            Console.WriteLine(" -----------------------");
            Console.WriteLine("| 1: Simulate           |");
            Console.WriteLine("| 2: Exit               |");
            Console.WriteLine(" -----------------------");
            Console.WriteLine();
            ReadMenu(); //Asks for menu input
        }

        //Method to read menu option
        public static void ReadMenu()
        {
            //Init variables
            int option = 0;

            //Read option
            Console.Write("Please enter an option: ");
            try
            {
                option = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error: Please enter a postive whole integer [Please wait...]");
                Thread.Sleep(2000);
                Draw();
            }

            //Checks through options to execute valid options and reset invalid options
            switch (option)
            {
                case 1:
                    Simulation.Start();
                    break;
                case 2:
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw();
                    break;
            }
        }
    }
}
