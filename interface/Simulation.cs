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
        public static double workingRValue = 2.25; // Changed to reduce transmissions
        public static double fatalityRate = 0.34; // 34%
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
        public static double importedCases = 0;
        public static int day = 0;
        public static double budget = 5000000000; // Base budget of 5 billion
        public static int alertLevel = 1;
        public static double population = 4917000; // Population of New Zealand
        public static bool bordersClosed = false; // When the borders are open, max imported cases will increase
        public static bool finishSuccess = false; // False = Fail (Everyone dead), True = Herd Immunity (Everyone vaccinated)
        public static bool gameRunning = true;
        public static double deaths = 0;
        public static double totalCases = 0;
        public static double borderCases = 0;
        public static double communityCases = 0;
        public static double newCommunityCases = 0;
        public static int passengersEntering = 300;
        public static bool isolationEnforced = false;

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
            // Debug loop to test simulation
            while (gameRunning)
            {
                Draw();

                // When enter is pressed, the user continues on to the next day of the simulation
                Console.WriteLine();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine(" Press enter to continue to next day...");
                Console.WriteLine("---------------------------------------");
                Console.ReadLine();
            }


            Scripts.MenuReturn(); // Returns to main menu after pressing enter
        }

        // Draw interface and results of simulation
        public static void Draw()
        {
            Simulate();
            Console.Clear();
            Scripts.DrawTitle("Simulation");
            Console.WriteLine("Simulating Day {0}", day);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("News: {0}", Headline());
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Total Cases: {0}", totalCases);
            Console.WriteLine("New Community Cases: {0}", newCommunityCases);
            Console.WriteLine("Community Cases: {0}", totalCases);
            Console.WriteLine("Imported Cases: {0}", importedCases);
            Console.WriteLine("Transmissibility: {0}", rValue);
            Console.WriteLine("Budget: ${0}", budget);
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
            day++;
        }
    }
}
