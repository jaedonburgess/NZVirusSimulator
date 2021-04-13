using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator
{
    class Simulation
    {
        /* 
         *  INIT GLOBAL VARIABLES
         */

        // Virus information
        public static string virusName = "SARS-CoV 2";
        public static double rValue = 2.25; // COVID-19 R-Value
        public static double workingRValue = rValue; // Changed to reduce transmissions
        public static double fatalityRate = 3.4; // 3.4%

        // Government information
        public static double budget = 5000000000; // Base budget of 5 billion
        public static double expenses = 0; // Total expenses
        public static double vaccinations = 0; // Total vaccination count
        public static double vaccinationRate = 0; // Number of vaccinations per day
        public static bool vaccinating = false; // If true, population is vaccinating
        public static int vaccineCost = 15; // Vaccine cost per dose
        public static double vaccineExpenses = 0; // Expenses specifically for vaccines
        public static int alertLevel = 1;
        public static double alertLevelExpenses = 0; // Expenses specifically for alert level changes
        public static double population = 4917000; // Population of New Zealand
        public static bool bordersClosed = false; // When the borders are open, max imported cases will increase
        public static bool isolationEnforced = false; // Boolean used to check if isolation is enforced (For Level 4 elimination)

        // Game information
        public static string finishSuccess = ""; // "money" - You ran out of money | "herd_infection" - Everyone is infected | "vaccinated" - Everyone is vaccinated (success)
        public static bool gameRunning = true; // Boolean used to stop the game when a result is given
        public static int day = 0;

        // Case/Death/Recovery information
        public static double importedCases = 0; // Total imported cases
        public static double newImportedCases = 0; // New imported cases
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
        public static double newDeaths = 0; // Variable to count new deaths for the day
        public static double deaths = 0; // Total death count
        public static double totalCases = 0; // Total case count
        public static double activeCases = 0; // Current active cases (cases removed from this variable after 14 days)
        public static double closedCases = 0; // Total closed cases (deaths and recoveries)
        public static double communityCases = 0; // Total community cases ever
        public static double newCommunityCases = 0; // New community cases for the day
        public static double recoveredCases = 0; // Total recovery count

        // Case delay and data arrays
        public static double[] casesOnDay = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // Used to calculate activeCases, recoveredCases, and deaths. New community cases are added to the [13] place in the array. They move down the array until 0. [0] place cases are subtracted from activeCases.
        public static double[] newCaseDelayArray = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // Used to add newCommunityCases only after 14 days due to infection taking place after 14 days.


        // Resets all variables back to default values
        public static void ResetDefaults()
        {
            virusName = "SARS-CoV 2";
            rValue = 2.25; // COVID-19 R-Value
            workingRValue = rValue; // Changed to reduce transmissions
            fatalityRate = 3.4; // 3.4%
            budget = 5000000000; // Base budget of 5 billion
            vaccinations = 0; // Total vaccination count
            vaccineCost = 15; // Vaccine cost per dose
            vaccinationRate = 0; // New vaccination count
            alertLevel = 1;
            population = 4917000; // Population of New Zealand
            bordersClosed = false; // When the borders are open, max imported cases will increase
            isolationEnforced = false; // ean used to check if isolation is enforced (For Level 4 elimination)
            finishSuccess = ""; // "money" - You ran out of money | "herd_infection" - Everyone is infected | "vaccinated" - Everyone is vaccinated (success)
            gameRunning = true; // ean used to stop the game when a result is given
            day = 0;
            importedCases = 0; // Total imported cases
            newImportedCases = 0; // New imported cases
            maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
            newDeaths = 0; // Variable to count new deaths for the day
            deaths = 0; // Total death count
            totalCases = 0; // Total case count
            activeCases = 0; // Current active cases (cases removed from this variable after 14 days)
            closedCases = 0; // Total closed cases (deaths and recoveries)
            communityCases = 0; // Total community cases ever
            newCommunityCases = 0; // New community cases for the day
            recoveredCases = 0; // Total recovery count
            
            // Reset arrays
            for (int i = 0; i <= 13; i++)
            {
                casesOnDay[i] = 0;
                newCaseDelayArray[i] = 0;
            }
        }

    // Start the simulation
    public static void Start(bool simulate)
        {
            // Run simulation while gameRunning is true
            while (gameRunning)
            {
                Draw(simulate);
                simulate = true; // Reset simulate bool if govIntervention method was used
            }

            // Clear console before final message
            Console.Clear();
            Scripts.DrawTitle("Simulation");

            // Depending on success a different message will be displayed
            if (finishSuccess == "vaccinated")
            {
                Console.WriteLine("Congratulations, you succesfully protected New Zealand by vaccination!");
            }
            else if (finishSuccess == "herd_infection")
            {
                Console.WriteLine("Congratulations, you failed! Everyone got '{0}'!", virusName);
            }
            else if (finishSuccess == "money")
            {
                Console.WriteLine("Congratulations, you failed! You ran out of money.");
            }
            else
            {
                Console.WriteLine("Simulation Canceled."); // Runs if sim gets cancelled by player
            }

            ResetDefaults(); // Reset game upon completion
            Scripts.MenuReturn(); // Returns to main menu after pressing enter
        }


        // Draw interface and results of simulation
        public static void Draw(bool simulate)
        {
            if (simulate) // If the draw function is asked to simulate, the simulator will run
            {
                Simulate(); // Start simulation
            }

            Console.Clear();
            Scripts.DrawTitle("Simulation");
            Console.WriteLine("Simulating Day {0}", day);
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine(virusName);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Total Cases: {0}", totalCases);
            Console.WriteLine("Total Deaths: {0}", deaths);
            Console.WriteLine("Closed Cases: {0}", closedCases);
            Console.WriteLine("Recovered Cases: {0}", recoveredCases);
            Console.WriteLine("Active Cases: {0}", activeCases);
            Console.WriteLine("Community Cases: {0}", communityCases);
            Console.WriteLine("Imported/Border Cases: {0}", importedCases);
            Console.WriteLine("--------------------------");
            Console.WriteLine("New Imported Cases: {0}", newImportedCases);
            Console.WriteLine("New Community Cases: {0}", newCommunityCases);
            Console.WriteLine("New Deaths: {0}", newDeaths);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Transmissibility: {0}", workingRValue);
            Console.WriteLine("Fatality Rate: {0}", fatalityRate);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Vaccinations: {0}", vaccinations);
            Console.WriteLine("Vaccination Rate: {0}", vaccinationRate);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Population: {0}", population);
            Console.WriteLine("Alert Level: {0}", alertLevel);
            Console.WriteLine("Borders Closed: {0}", bordersClosed);
            Console.WriteLine("Budget: ${0}", budget);
            Console.WriteLine("Expenses: ${0}", expenses);
            Console.WriteLine("--------------------------");
            // Run so that the simulation ends if the cases, budget, or vaccination count causes the game to end without running through the govOptions again
            if (gameRunning)
            {
                govOptions(); // Draw the government options/interventions to the screen
            }
        }

        public static void govOptions()
        {
            Console.WriteLine();
            Console.WriteLine(" --------------------------");
            Console.WriteLine("| Government Interventions |");
            Console.WriteLine("| 1: Border Condition      |");
            Console.WriteLine("| 2: Alert Level           |");
            Console.WriteLine("| 3: Vaccines              |");
            Console.WriteLine("| 4: Continue              |");
            Console.WriteLine("| 5: Cancel Simulation     |");
            Console.WriteLine(" --------------------------");

            /*
             * Options
             */

            // Variables
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

            // Act on the entered option
            switch (option)
            {
                case 1:
                    BorderState.Start(); // Run border state mini application
                    break;
                case 2:
                    AlertLevel.Start(); // Run alert level mini application
                    break;
                case 3:
                    if (day < 80)
                    {
                        Console.WriteLine("You cannot vaccinate until day 80! [Please Wait...]");
                        Thread.Sleep(2000);
                        Draw(false); // Re-draw information without simulating
                    }
                    else
                    {
                        Vaccinations.Start(); // Run vaccinations mini application
                    }
                    break;
                case 4:
                    break;
                case 5:
                    gameRunning = false;
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000); 
                    Draw(false); // Re-draw information without simulating
                    break;
            }
        }

        // Virus simulator algorithm
        public static void Simulate()
        {
            newCommunityCases = 0; // Reset new cases at the end of the simulated day

            // Enforce isolation to the fullest at level 4
            if (alertLevel == 4)
            {
                isolationEnforced = true; // No transmission possible (unless the rare possibility of a passenger randomly brings the virus in takes hold)
                workingRValue = 0; // Virus transmissibility is 0
            }
            else if (alertLevel == 3)
            {
                workingRValue = 0.1; // Reduces transimission to a significantly low point
            }
            else if (alertLevel == 2)
            {
                workingRValue = rValue / 2; // Halves the transmissibility of the virus
            }
            else
            {
                workingRValue = rValue; // Virus is as transimissible as possible
            }

            // Code to run if vaccinating
            if (vaccinating)
            {
                vaccinations += vaccinationRate; // Add vaccination rate to total vaccinations
            }

            // Array Stepper (Move delayed case counts/Move active case 'timer' array down a step)
            //---------------------------------------------------------------------------------------------------------

            // Move active cases down a step in the array
            for (int i = 1; i <= 13; i++) // Starts at 1 because there are no days before 0 so no point removing
            {
                // Init variable
                double movingValue = 0;

                // Set next day in array to moving variable (current day)
                movingValue = casesOnDay[i];
                casesOnDay[i - 1] = movingValue;
            }

            // Move new cases down a step in the array
            for (int i = 1; i <= 13; i++) // Starts at 1 because there are no days before 0 so no point removing
            {
                // Init variable
                double movingValue = 0;

                // Set next day in array to moving variable (current day)
                movingValue = newCaseDelayArray[i];
                newCaseDelayArray[i - 1] = movingValue;
            }

            //---------------------------------------------------------------------------------------------------------


            // Get daily imported cases with random number generator
            newImportedCases = Scripts.RandomNumber(maxImported);


            // Spread Calculator (How many newCommunityCases)
            //---------------------------------------------------------------------------------------------------------

            // Runs conditional statements to find new cases depending on border and/or isolation conditions
            /* For Each Ranges OR Iterating using a for
               https://stackoverflow.com/questions/38379400/c-sharp-int-type-in-foreach-statement */
            if (bordersClosed) // Runs when borders are closed
            {
                if (communityCases > 0) // Runs when community cases are greater than 0 while borders are closed
                {
                    if (isolationEnforced == false) // Runs when borders are closed but isolation is not enforced
                    {
                        for (int i = 1; i <= communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                        {
                            // For each community case, add cases to the delay (2 weeks until test shows positive)
                            newCaseDelayArray[13] += workingRValue;
                        }
                    }
                    else // No transmission when isolation enforced (Level 4)
                    {
                        newCaseDelayArray[13] = 0;
                    }
                }

                // 1 in 100 chance of having a community case outbreak with closed borders
                if (Scripts.RandomNumber(100) == 1)
                {
                    newCommunityCases++;
                }
            }
            else // Runs when borders are opened
            {
                if (isolationEnforced == false) // Runs when borders are open and isolation is not enforced ( < Level 4)
                {
                    for (int i = 1; i <= communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                    {
                        // For each community case, add cases to the delay (2 weeks until test shows positive)
                        newCaseDelayArray[13] += workingRValue;
                    }
                }
                else // No transmission when isolation enforced (Level 4)
                {
                    newCaseDelayArray[13] = 0;
                }

                newCommunityCases += newImportedCases; // Imported cases count as community cases with open borders
            }

            //---------------------------------------------------------------------------------------------------------

            // Increase maxImported cases possibility if borders are open
            maxImported++;

            // Add new community cases from delay array
            newCommunityCases += newCaseDelayArray[0];

            // Round case values down (Must happen after newCommunityCases and newImportedCases are calculated for rValue to work)
            // **Code from Microsoft Docs Math.Round (https://docs.microsoft.com/en-us/dotnet/api/system.math.round?view=net-5.0)**
            newImportedCases = Math.Round(newImportedCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person
            newCommunityCases = Math.Round(newCommunityCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person


            //---------------------------------------------------------------------------------------------------------
            /* Add new community cases, total cases, set casesOnDay[13] variable to today's cases, active cases, 
            closed cases, deaths, recoveries, population decrease and calculate budget changes */

            // Conditional statement that changes totalCases equation based on border status
            // (Because imported cases count as community cases when the borders are open, a different calculation is required for closed borders)
            if (bordersClosed)
            {
                totalCases += newImportedCases + newCommunityCases; // Sum for total cases
                activeCases += newImportedCases + newCommunityCases; // New cases for today get added onto activeCases
            }
            else
            {
                activeCases += newCommunityCases;
                totalCases += newCommunityCases;
            }

            communityCases += newCommunityCases; // Add new community cases to community case count
            casesOnDay[13] = newCommunityCases + newImportedCases; // Add new community cases and imported cases to new cases on this day
            activeCases -= casesOnDay[0]; // Remove cases from active cases if it has been 14 days
            importedCases += newImportedCases; // Adds newImportedCases to the total importedCases count
            closedCases += casesOnDay[0]; // Adds all cases that have finished their 14 days to closedCases variable
            newDeaths = casesOnDay[0] * fatalityRate / 100; // New deaths equal cases after 14 days multiplied by the fatality rate divided by 100 to get a calculatable percentage
            newDeaths = Math.Round(newDeaths, 0, MidpointRounding.ToNegativeInfinity);
            deaths += newDeaths; // Add new deaths to total deaths
            population -= newDeaths; // Remove newDeaths from population
            recoveredCases += casesOnDay[0] - newDeaths; // Recovered cases equal cases after 14 days minus the death toll of the day
            budget -= expenses;

            //---------------------------------------------------------------------------------------------------------

            // Check if game is over
            if (activeCases >= population)
            {
                gameRunning = false; // Stop simulation loop
                finishSuccess = "herd_infection"; // Player did not succeed (everyone infected)
            }
            else if (vaccinations >= population * 2) // *2 because 2 dose vaccine
            {
                gameRunning = false; // Stop simulation loop
                finishSuccess = "vaccinated"; // Player succeeded
            }
            else if (budget <= 0)
            {
                gameRunning = false; // Stop simulation loop
                finishSuccess = "money"; // Player failed (ran out of money)
            }
            else
            {
                day++; // Increment day if sim not finished
            }
        }
    }
}
