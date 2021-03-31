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
            Scripts.importedCases = Scripts.RandomNumber(Scripts.maxImported); // Get daily imported cases

            if (Scripts.bordersClosed) // Runs when borders are closed
            {
                if (Scripts.communityCases > 0) // Runs when community cases are greater than 0 while borders are closed
                {
                    if (Scripts.isolationEnforced == false) // Runs when borders are closed but isolation is not enforced
                    {
                        Scripts.communityCases = (Scripts.rValue * Scripts.communityCases); // If borders are closed but community isolation is not enforced, add cases
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
                    Scripts.communityCases = Scripts.importedCases + (Scripts.rValue * (Scripts.communityCases + Scripts.importedCases)); // Imported cases count as community cases
                }
                Scripts.maxImported++; // Increase maxImported cases possibility if borders are open
            }

            // Code from Microsoft Docs (get link for Math.Round() method, WiFi not working)
            Scripts.borderCases = Math.Round(Scripts.borderCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person
            Scripts.communityCases = Math.Round(Scripts.communityCases, 0, MidpointRounding.ToNegativeInfinity); // Rounds number down to a whole person

            Scripts.totalCases = Scripts.borderCases + Scripts.communityCases; // Sum for total cases
            Scripts.day = Scripts.day + Scripts.dayIncrement; // Increments day based on user setting
        }
    }
}
