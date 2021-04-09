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
        public static double rValue = 2.4; // COVID-19 R-Value
        public static double workingRValue = rValue; // Changed to reduce transmissions
        public static double fatalityRate = 3.4; // 3.4%

        // Government information
        public static double budget = 5000000000; // Base budget of 5 billion
        public static int passengersEntering = 300; // Used to determine how many passengers are entering NZ per day
        public static double vaccinations = 0; // Total vaccination count
        public static double newVaccinations = 0; // New vaccination count
        public static int alertLevel = 1;
        public static double population = 4917000; // Population of New Zealand
        public static bool bordersClosed = false; // When the borders are open, max imported cases will increase
        public static bool isolationEnforced = false; // Boolean used to check if isolation is enforced

        // Game information
        public static bool finishSuccess = false; // False = Fail (Everyone dead), True = Herd Immunity (Everyone vaccinated)
        public static bool gameRunning = true; // Boolean used to stop the game when a result is given
        public static int day = 0;

        // Case/Death/Recovery information
        public static double importedCases = 0;
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
            // Default variable values
            virusName = "SARS-CoV 2";
            rValue = 2.25; // COVID-19 R-Value
            workingRValue = 2.25; // Changed to reduce transmissions
            fatalityRate = 0.34; // 34%
            maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
            importedCases = Scripts.RandomNumber(maxImported);
            day = 0;
            budget = 5000000000; // Base budget of 5 billion
        }

        // Start the simulation
        public static void Start(bool simulate)
        {
            // Run simulation while gameRunning is true
            while (gameRunning)
            {
                Draw(simulate);

                if (gameRunning) // Makes sure the continue feature doesn't run again if the simulation wins or fails
                {
                    // When enter is pressed, the user continues on to the next day of the simulation
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------");
                    Console.WriteLine(" Press enter to continue to next day...");
                    Console.WriteLine("---------------------------------------");
                    Console.ReadLine();
                } 
                else
                {
                    break;
                }
            }

            // Clear console before final message
            Console.Clear();
            Scripts.DrawTitle("Simulation");

            // Depending on success a different message will be displayed (Will change to headline eventually)
            if (finishSuccess)
            {
                Console.WriteLine("Congratulations, you succeeded!");
            }
            else
            {
                Console.WriteLine("Congratulations, you failed! Everyone got '{0}'!", virusName);
            }


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
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("News: {0}", Headline());
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Total Cases: {0}", totalCases);
            Console.WriteLine("Total Deaths: {0}", deaths);
            Console.WriteLine("Closed Cases: {0}", closedCases);
            Console.WriteLine("Recovered Cases: {0}", recoveredCases);
            Console.WriteLine("Active Cases: {0}", activeCases);
            Console.WriteLine("Community Cases: {0}", totalCases);
            Console.WriteLine("--------------------------");
            Console.WriteLine("New Imported Cases: {0}", importedCases);
            Console.WriteLine("New Community Cases: {0}", newCommunityCases);
            Console.WriteLine("New Deaths: {0}", newDeaths);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Transmissibility: {0}", workingRValue);
            Console.WriteLine("Fatality Rate: {0}", fatalityRate);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Vaccinations: {0}", vaccinations);
            Console.WriteLine("New Vaccinations: {0}", newVaccinations);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Population: {0}", population);
            Console.WriteLine("Alert Level: {0}", alertLevel);
            Console.WriteLine("Borders Closed: {0}", bordersClosed);
            Console.WriteLine("Budget: ${0}", budget);
            Console.WriteLine("--------------------------");
            govOptions(); // Draw the government options/interventions to the screen
        }

        public static void govOptions()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine(" Government Interventions ");
            Console.WriteLine(" 1: Border Condition      ");
            Console.WriteLine(" 2: Isolation Enforcement ");
            Console.WriteLine(" 3: Alert Level           ");
            Console.WriteLine(" 4: Vaccines              ");
            Console.WriteLine(" 5: Continue              ");
            Console.WriteLine("--------------------------");

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
                    Scripts.DrawTitle("Simulation");
                    break;
                case 2:
                case 3:
                case 4:
                    Console.WriteLine("Error: This option is not available [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(false);
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000); 
                    Draw(false);
                    break;
            }
        }

        // Display headline based on case, death, vaccine or day information
        public static string Headline()
        {
            if (day == 0)
            {
                return "New Virus has MANIFESTED in Aotearoa";
            }

            return "Virus Situation Remains the Same";
        }

        // Virus simulator algorithm
        public static void Simulate()
        {
            newCommunityCases = 0; // Reset new cases at the end of the simulated day

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

            // Get daily imported cases with random number generator
            importedCases = Scripts.RandomNumber(maxImported);

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
                }

                // 1 in (passengersEntering) chance of having a community case outbreak with closed borders
                if (Scripts.RandomNumber(passengersEntering) == 1)
                {
                    newCommunityCases++;
                }
            }
            else // Runs when borders are opened
            {
                if (isolationEnforced == false) // Runs when borders are open and isolation is not enforced
                {
                    for (int i = 1; i <= communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                    {
                        // For each community case, add cases to the delay (2 weeks until test shows positive)
                        newCaseDelayArray[13] += workingRValue;
                    }
                }
                newCommunityCases += importedCases; // Imported cases count as community cases with open borders
                maxImported++; // Increase maxImported cases possibility if borders are open
            }

            // Add new community cases from delay array
            newCommunityCases += newCaseDelayArray[0];

            // Round case values down
            // **Code from Microsoft Docs Math.Round (https://docs.microsoft.com/en-us/dotnet/api/system.math.round?view=net-5.0)**
            importedCases = Math.Round(importedCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person
            newCommunityCases = Math.Round(newCommunityCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person

            /* Add new community cases, total cases, set casesOnDay[13] variable to today's cases, active cases, 
            closed cases, deaths, recoveries, and population decrease */
            communityCases += newCommunityCases; // Add new community cases to community case count

            // Conditional statement that changes totalCases equation based on border status
            if (bordersClosed)
            {
                totalCases = importedCases + communityCases; // Sum for total cases
            }
            else
            {
                totalCases = communityCases;
            }
            casesOnDay[13] = newCommunityCases; // Add community cases to new cases on this day
            activeCases += newCommunityCases; // New cases for today get added onto activeCases (Not using newCommunityCases because those only count for community cases and not border cases when borders are closed)
            activeCases -= casesOnDay[0]; // Remove cases from active cases if it has been 14 days
            closedCases += casesOnDay[0]; // Adds all cases that have finished their 14 days to closedCases variable
            newDeaths = casesOnDay[0] * fatalityRate / 100; // New deaths equal cases after 14 days multiplied by the fatality rate divided by 100 to get a calculatable percentage
            newDeaths = Math.Round(newDeaths, 0, MidpointRounding.ToNegativeInfinity);
            deaths += newDeaths; // Add new deaths to total deaths
            population -= newDeaths; // Remove newDeaths from population
            recoveredCases += casesOnDay[0] - newDeaths; // Recovered cases equal cases after 14 days minus the death toll of the day

            // Check if game is over
            if (activeCases >= population)
            {
                gameRunning = false; // Stop simulation loop
                finishSuccess = false; // Player did not succeed
            }
            else if (vaccinations >= population)
            {
                gameRunning = false; // Stop simulation loop
                finishSuccess = true; // Player succeeded
            }
            else
            {
                day++; // Increment day if sim not finished
            }
        }
    }
}
