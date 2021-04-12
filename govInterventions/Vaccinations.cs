using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator
{
    class Vaccinations
    {
        public static void Start()
        {
            Draw();
        }

        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle("Vaccinations");
            Console.WriteLine();
            Console.WriteLine(" -----------------------------------------------------");
            Console.WriteLine(" Vaccination Rate: {0}", Simulation.vaccinationRate);
            Console.WriteLine(" Vaccination Cost: ${0}/dose OR ${1} total", Simulation.vaccineCost, (Simulation.vaccineCost * 2) * Simulation.population);
            Console.WriteLine(" Daily Vaccine Expenses: {0}", Simulation.vaccineExpenses);
            Console.WriteLine(" Budget: ${0}", Simulation.budget);
            Console.WriteLine(" -----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine(" -----------------------------------------------------");
            Console.WriteLine("| 1: Purchase Vaccines                                |");
            Console.WriteLine("| 2: Vaccination Rate ($2M/25K doses) *ONLY INCREASE* |");
            Console.WriteLine("| 3: Exit                                             |");
            Console.WriteLine(" -----------------------------------------------------");
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

            // Checks through options to execute valid options and reset invalid options
            switch (option)
            {
                case 1:
                    // Only run if vaccinations have not started yet
                    if (Simulation.vaccinating)
                    {
                        Console.WriteLine("You are already vaccinating! [Please wait...]");
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.budget -= (Simulation.vaccineCost * 2) * Simulation.population; // Subtract total vaccine price from budget
                        Simulation.expenses -= Simulation.vaccineExpenses; // Remove previous vaccine expenses before adding new ones
                        Simulation.vaccineExpenses += 2000000; // 2 million per 25k vaccinations
                        Simulation.vaccinationRate += 25000; // 25k people vaccinated per day initially
                        Simulation.expenses += Simulation.vaccineExpenses; // Add updated vaccineExpenses to expenses
                        Simulation.vaccinating = true; // Start vaccinating population
                        Draw();
                    }
                    break;
                case 2:
                    // Don't run if vaccinationRate exceeds the population
                    if (Simulation.vaccinationRate > Simulation.population - 25000)
                    {
                        Console.WriteLine("That rate exceeds the population! [Please wait...]");
                        Thread.Sleep(2000);
                        Draw();
                    }
                    else
                    {
                        Simulation.expenses -= Simulation.vaccineExpenses; // Remove previous vaccine expenses before adding new ones
                        Simulation.vaccineExpenses += 2000000; // 2 million per 25k vaccinations
                        Simulation.vaccinationRate += 25000; // 25k people vaccinated per day initially
                        Simulation.expenses += Simulation.vaccineExpenses; // Add updated vaccineExpenses to expenses
                        Draw();
                    }
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
    }
}
