using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator
{
    class AlertLevel
    {
        public static void Start()
        {
            Draw();
        }

        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle("Change Alert Level");
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------");
            // Change colour based on open or closed
            if (Simulation.alertLevel == 2)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (Simulation.alertLevel == 3)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (Simulation.alertLevel == 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(" Alert Level: {0}", Simulation.alertLevel);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Budget: ${0}", Simulation.budget);
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" --------------------------------------");
            Console.WriteLine("| 1: Level 1 (No cost/day)             |");
            Console.WriteLine("| 2: Level 2 ($5M/day)                 |");
            Console.WriteLine("| 3: Level 3 ($15M/day)                |");
            Console.WriteLine("| 4: Level 4 ($25M/day)                |");
            Console.WriteLine("| 5: Exit                              |");
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
                    // Checks if country is already at the alert level set
                    if (Simulation.alertLevel == 1)
                    {
                        Console.WriteLine("The country is already at Alert Level {0}! [Please wait...]", Simulation.alertLevel);
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.alertLevel = 1;
                        Simulation.expenses -= Simulation.alertLevelExpenses; // Remove previous alertLevelExpenses from expenses
                        Simulation.alertLevelExpenses = 0; // Costs nothing to be in Level 1
                        Simulation.expenses += Simulation.alertLevelExpenses; // Add alertLevelExpenses to total expenses
                        Draw();
                    }
                    break;
                case 2:
                    if (Simulation.alertLevel == 2)
                    {
                        Console.WriteLine("The country is already at Alert Level {0}! [Please wait...]", Simulation.alertLevel);
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.alertLevel = 2;
                        Simulation.expenses -= Simulation.alertLevelExpenses; // Remove previous alertLevelExpenses from expenses
                        Simulation.alertLevelExpenses = 5000000; // Costs $5 million per day to be in Level 2
                        Simulation.expenses += Simulation.alertLevelExpenses; // Add alertLevelExpenses to total expenses
                        Draw();
                    }
                    break;
                case 3:
                    if (Simulation.alertLevel == 3)
                    {
                        Console.WriteLine("The country is already at Alert Level {0}! [Please wait...]", Simulation.alertLevel);
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.alertLevel = 3;
                        Simulation.expenses -= Simulation.alertLevelExpenses; // Remove previous alertLevelExpenses from expenses
                        Simulation.alertLevelExpenses = 15000000; // Costs $15 million per day to be in Level 3
                        Simulation.expenses += Simulation.alertLevelExpenses; // Add alertLevelExpenses to total expenses
                        Draw();
                    }
                    break;
                case 4:
                    if (Simulation.alertLevel == 4)
                    {
                        Console.WriteLine("The country is already at Alert Level {0}! [Please wait...]", Simulation.alertLevel);
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.alertLevel = 4;
                        Simulation.expenses -= Simulation.alertLevelExpenses; // Remove previous alertLevelExpenses from expenses
                        Simulation.alertLevelExpenses = 25000000; // Costs $25 million per day to be in Level 4
                        Simulation.expenses += Simulation.alertLevelExpenses; // Add alertLevelExpenses to total expenses
                        Draw();
                    }
                    break;
                case 5:
                    Simulation.Start(false); // Re-run loop within simulation without running algorithm
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
