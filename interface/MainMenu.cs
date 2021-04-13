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
            Scripts.DrawTitle("Main Menu");
            Console.WriteLine(" -----------------------");
            Console.WriteLine("| 1: Simulate           |");
            Console.WriteLine("| 2: View Settings      |");
            Console.WriteLine("| 3: Exit               |");
            Console.WriteLine(" -----------------------");
            Console.WriteLine();
            ReadMenu(); //Asks for menu input
        }

        //Method to read menu option
        public static void ReadMenu()
        {
            //Init variables
            int option = 0;

            // Read option
            while (option == 0)
            {
                Console.WriteLine("Please enter an option: ");
                option = Scripts.ReadInt();
                if (option == Scripts.intError)
                {
                    break;
                }
            }

            //Checks through options to execute valid options and reset invalid options
            switch (option)
            {
                case 1:
                    Simulation.Start(true); // Starts simulation application and runs algorithm (true)
                    break;
                case 2:
                    Settings.Start();
                    break;
                case 3:
                    // *EXITING A C# APPLICATION PROPERLY*
                    // https://stackoverflow.com/questions/12977924/how-to-properly-exit-a-c-sharp-application
                    Console.Clear();
                    Environment.Exit(0); // Error Code: 0 = Application ran successfully
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
