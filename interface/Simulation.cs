using System;
using System.Collections.Generic;
using System.Text;

namespace NZVirusSimulator
{
    class Simulation
    {
        // Init global variables
        public static string virusName = "SARS-CoV 2";
        public static double rValue = 2.25; // COVID-19 R-Value
        public static double workingRValue = rValue; // Changed to reduce transmissions
        public static double fatalityRate = 3.4; // 3.4%
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
        public static double importedCases = 0;
        public static int day = 0;
        public static double budget = 5000000000; // Base budget of 5 billion
        public static int alertLevel = 1;
        public static double population = 4917000; // Population of New Zealand
        public static bool bordersClosed = false; // When the borders are open, max imported cases will increase
        public static bool finishSuccess = false; // False = Fail (Everyone dead), True = Herd Immunity (Everyone vaccinated)
        public static bool gameRunning = true;
        public static double newDeaths = 0;
        public static double deaths = 0;
        public static double totalCases = 0;
        public static double activeCases = 0;
        public static double closedCases = 0;
        public static double borderCases = 0;
        public static double communityCases = 0;
        public static double newCommunityCases = 0;
        public static double recoveredCases = 0;
        public static double vaccinations = 0;
        public static int passengersEntering = 300;
        public static bool isolationEnforced = false;
        public static int[] casesOnDay = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static int fourteenDayIncrement = 0;

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
        public static void Start()
        {
            // Run simulation while gameRunning is true
            while (gameRunning)
            {
                Draw();

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
        public static void Draw()
        {
            Simulate(); // Start simulation
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
            Console.WriteLine("--------------------------");
            Console.WriteLine("New Community Cases: {0}", newCommunityCases);
            Console.WriteLine("New Deaths: {0}", newDeaths);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Community Cases: {0}", totalCases);
            Console.WriteLine("Imported Cases: {0}", importedCases);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Transmissibility: {0}", workingRValue);
            Console.WriteLine("Fatality Rate: {0}", fatalityRate);
            Console.WriteLine("--------------------------");
            Console.WriteLine("Population: {0}", population);
            Console.WriteLine("Budget: ${0}", budget);
            Console.WriteLine("--------------------------");

        }

        public static string Headline()
        {
            if (day == 0)
            {
                return "New Virus has MANIFESTED in Aotearoa";
            }

            return "Virus Situation Remains the Same";
        }


        public static void Simulate()
        {

            // Move active cases down a step in the array
            for (int i = 1; i <= 13; i++) // Starts at 1 because there are no days before 0 so no point removing
            {
                // Init variable
                int movingValue = 0;

                // Set next day in array to moving variable (current day)
                movingValue = casesOnDay[i];
                casesOnDay[i - 1] = movingValue;
            }

            // For Each Ranges OR Iterating using a for
            // https://stackoverflow.com/questions/38379400/c-sharp-int-type-in-foreach-statement

            importedCases = Scripts.RandomNumber(maxImported); // Get daily imported cases

            if (bordersClosed) // Runs when borders are closed
            {
                if (communityCases > 0) // Runs when community cases are greater than 0 while borders are closed
                {
                    if (isolationEnforced == false) // Runs when borders are closed but isolation is not enforced
                    {
                        for (int i = 1; i <= communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                        {
                            // For each community case, use the working R value and add it to the total number of new community cases
                            newCommunityCases += workingRValue;
                        }
                    }
                }
                borderCases = importedCases; // Adds imported cases to border case count

                if (Scripts.RandomNumber(passengersEntering) == 1)
                {
                    communityCases++; // 1 in (passengersEntering) chance of having a community case outbreak with closed borders
                }
            }
            else // Runs when borders are opened
            {
                if (isolationEnforced == false) // Runs when borders are open and isolation is not enforced
                {
                    for (int i = 1; i <= communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                    {
                        // For each community case, use the working R value and add it to the total number of new community cases
                        newCommunityCases += workingRValue;
                    }
                }
                newCommunityCases += importedCases; // Imported cases count as community cases with open borders
                maxImported++; // Increase maxImported cases possibility if borders are open
            }

            

            // Code from Microsoft Docs Math.Round (https://docs.microsoft.com/en-us/dotnet/api/system.math.round?view=net-5.0)
            borderCases = Math.Round(borderCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person
            newCommunityCases = Math.Round(newCommunityCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person

            communityCases += newCommunityCases; // Add new community cases to community case count

            totalCases = borderCases + communityCases; // Sum for total cases

            casesOnDay[13] = Convert.ToInt32(newCommunityCases); // Add community cases to new cases on this day

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
                activeCases += casesOnDay[13]; // New cases for today get added onto activeCases (Not using newCommunityCases because those only count for community cases and not border cases when borders are closed)
                activeCases -= casesOnDay[0]; // Remove cases from active cases if it has been 14 days
                closedCases += casesOnDay[0]; // Adds all cases that have finished their 14 days to closedCases variable
                newDeaths = casesOnDay[0] * fatalityRate / 100; // New deaths equal cases after 14 days multiplied by the fatality rate divided by 100 to get a calculatable percentage
                deaths += newDeaths; // Add new deaths to total deaths
                population -= newDeaths; // Remove newDeaths from population
                recoveredCases += casesOnDay[0] - newDeaths; // Recovered cases equal cases after 14 days minus the death toll of the day
                fourteenDayIncrement++; // Incements the fourteenDayIncrement variable which is used at the beginning to check if it has been 14 days, and if so cases will either recover or die
                day++; // Increment day if sim not finished
            }
        }
    }
}
