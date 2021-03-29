using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator 
{
    class Scripts 
    {
        // Init global variables
        public static string virusName = "Pandemivirus";
        public static double rValue = 0;
        public static double fatalityRate = 0;
        public static string headline = "";
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors
        public static double importedCases = Scripts.RandomNumber(maxImported);
        public static int dayIncrement = 1;
        public static int day = 0;
        public static double budget = 5000000000; // Base budget of 5 billion

        // Error codes
        public static string strError = "\0 E3RR0R";

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

        // Resets all variables back to default values
        public static void ResetDefaults()
        {
            // Init global variables
            virusName = "Pandemivirus";
            rValue = 0;
            fatalityRate = 0;
            headline = "";
            maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors
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
    }
}
