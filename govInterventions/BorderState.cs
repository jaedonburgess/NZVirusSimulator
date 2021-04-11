using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator
{
    class BorderState
    {
        public static void Start()
        {
            Draw();
        }

        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle("Change Border State");
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------");
            // Change colour based on open or closed
            if (Simulation.bordersClosed)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine(" State: {0}", borderState(Simulation.bordersClosed));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Budget: ${0}", Simulation.budget);
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine("| 1: Close Borders ($2B + $5M/day)     |");
            Console.WriteLine("| 2: Open Borders                      |");
            Console.WriteLine("| 3: Exit                              |");
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine();
            ReadMenu();
        }

        // Method to read menu option
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
                    // Makes sure player doesn't spend too much money closing the borders
                    if (Simulation.bordersClosed)
                    {
                        Console.WriteLine("The borders are already closed! [Please wait...]");
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.bordersClosed = true; // Sets border state to closed
                        Simulation.budget -= 2000000000; // Costs 2 billion to close the borders
                        Draw();
                    }
                    break;
                case 2:
                    Simulation.bordersClosed = false; // Sets border state to open
                    Draw();
                    break;
                case 3:
                    Simulation.Start(false); // Re-run loop within simulation without running algorithm
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw();
                    break;
            }
        }

        // Get border state in OPEN or CLOSED format instead of TRUE or FALSE
        public static string borderState(bool closed)
        {
            // If closed, return closed. If open, return open
            if (closed)
            {
                return "CLOSED";
            }
            else
            {
                return "OPEN";
            }
        }
    }
}
