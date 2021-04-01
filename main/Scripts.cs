using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator 
{
    class Scripts 
    {
        // Init global variables
        public static string virusName = "SARS-CoV 2";
        public static double rValue = 2.25; // COVID-19 R-Value
        public static double workingRValue = 0.01; // Changed to reduce transmissions
        public static double rValueInc = 1.1; // Increment R value by %
        public static double fatalityRate = 0.34; // 34%
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors | This value will increase without border control
        public static double importedCases = 0;
        public static int dayIncrement = 1;
        public static int day = 0;
        public static double budget = 5000000000; // Base budget of 5 billion
        public static int alertLevel = 1;
        public static double population = 4917000; // Population of New Zealand
        public static bool bordersClosed = false; // When the borders are open, max imported cases will increase
        public static bool finishSuccess = false; // False = Fail (Everyone dead), True = Herd Immunity (Everyone vaccinated)
        public static double deaths = 0;
        public static double totalCases = 0;
        public static double borderCases = 0;
        public static double communityCases = 0;
        public static double newCommunityCases = 0;
        public static int passengersEntering = 300;
        public static bool isolationEnforced = false;

        // Error codes
        public static string strError = "\0 E3RR0R";
        public static int intError = 0;

        // Draw application title (to be used for draw methods so this long stretch of code doesn't have to be rewritten)
        public static void DrawTitle(string title) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[New Zealand Virus Simulator]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("By jbapps (Jaedon Burgess) - 2021");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("[{0}]", title);
            Console.WriteLine();
        }

        // Globally called random number generator
        public static int RandomNumber(int range)
        {
            Random random = new Random();
            int randomNum = random.Next(1, range);
            return randomNum;
        }

        // Return to main menu after pressing enter
        public static void MenuReturn()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Press enter to return to main menu...");
            Console.WriteLine("---------------------------------------");
            Console.ReadLine();
            MainMenu.Draw();
        }

        public static string Headline()
        {
            if(day == 0)
            {
                return "New Virus has MANIFESTED in Aotearoa";
            }

            return "Virus Situation Remains the Same";
        }

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
            dayIncrement = 1;
            day = 0;
            budget = 5000000000; // Base budget of 5 billion

    }

        // Read a string value
        public static string ReadString()
        {
            // Init variables
            string str = "";

            // Run while string is empty
            while(str == "")
            {
                try
                {
                    str = Console.ReadLine();
                }
                catch
                {
                    return strError;
                }

                if(str == "")
                {
                    return strError;
                }
            }
            return str;
        }

        // Read an integer
        public static int ReadInt()
        {
            // Init variables
            int num = 0;

            // Run while string is empty
            while (num == 0)
            {
                try
                {
                    num = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    return intError;
                }

                if (num == 0)
                {
                    return intError;
                }
            }
            return num;
        }
    }
}
