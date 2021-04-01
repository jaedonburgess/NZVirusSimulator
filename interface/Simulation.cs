using System;
using System.Collections.Generic;
using System.Text;

namespace NZVirusSimulator
{
    class Simulation
    {
        // Start the simulation
        public static void Start()
        {
            // Debug loop to test simulation
            for(int i = 0; i <=20; i++)
            {
                Draw();
            }

            Scripts.MenuReturn(); // Returns to main menu after pressing enter
        }

        // Draw interface and results of simulation
        public static void Draw()
        {
            Simulate();
            Scripts.DrawTitle("Simulation");
            Console.WriteLine("Simulating Day {0}", Scripts.day);
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("News: {0}", Scripts.Headline());
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Total Cases: {0}", Scripts.totalCases);
            Console.WriteLine("Community Cases: {0}", Scripts.totalCases);
            Console.WriteLine("Imported Cases: {0}", Scripts.importedCases);
            Console.WriteLine("Transmissibility: {0}", Scripts.rValue);
            Console.WriteLine("Budget: ${0}", Scripts.budget);
        }

        public static void Simulate()
        {
            // For Each Ranges OR Iterating using a for
            // https://stackoverflow.com/questions/38379400/c-sharp-int-type-in-foreach-statement

            Scripts.importedCases = Scripts.RandomNumber(Scripts.maxImported); // Get daily imported cases

            if (Scripts.bordersClosed) // Runs when borders are closed
            {
                if (Scripts.communityCases > 0) // Runs when community cases are greater than 0 while borders are closed
                {
                    if (Scripts.isolationEnforced == false) // Runs when borders are closed but isolation is not enforced
                    {
                        for (int i = 1; i <= Scripts.communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                        {
                            // For each community case, use the working R value and add it to the total number of new community cases
                            Scripts.newCommunityCases += Scripts.workingRValue;
                        }
                    }
                }
                Scripts.borderCases = Scripts.importedCases; // Adds imported cases to border case count

                if (Scripts.RandomNumber(Scripts.passengersEntering) == 1)
                {
                    Scripts.communityCases++; // 1 in (passengersEntering) chance of having a community case outbreak with closed borders
                }
            }
            else // Runs when borders are opened
            {
                if (Scripts.isolationEnforced == false) // Runs when borders are open and isolation is not enforced
                {
                    for (int i = 1; i <= Scripts.communityCases; i++) // Variable 'i' can be 1 because community cases are greater than 0
                    {
                        // For each community case, use the working R value and add it to the total number of new community cases
                        Scripts.newCommunityCases += Scripts.workingRValue;
                    }
                }
                Scripts.newCommunityCases += Scripts.importedCases; // Imported cases count as community cases with open borders
                Scripts.maxImported++; // Increase maxImported cases possibility if borders are open
            }

            Scripts.communityCases += Scripts.newCommunityCases; // Add new community cases to community case count

            // Code from Microsoft Docs Math.Round (https://docs.microsoft.com/en-us/dotnet/api/system.math.round?view=net-5.0)
            Scripts.borderCases = Math.Round(Scripts.borderCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person
            Scripts.communityCases = Math.Round(Scripts.communityCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person

            Scripts.totalCases = Scripts.borderCases + Scripts.communityCases; // Sum for total cases
            Scripts.day = Scripts.day + Scripts.dayIncrement; // Increments day based on user setting
        }
    }
}
