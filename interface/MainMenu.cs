﻿using System;
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
            Console.WriteLine("| 3: Load Presets       |");
            Console.WriteLine("| 4: Exit               |");
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
                    Console.WriteLine("This option is under construction [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw();
                    break;
                case 4:
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
