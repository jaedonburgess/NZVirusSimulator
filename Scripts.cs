using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator 
{
    class Scripts 
    {
        // Init global variables
        public static double rValue = 0;
        public static double fatalityRate = 0;
        public static string headline = "";
        public static int maxImported = 2; // Random number generator starts at 1 so this avoids any initial errors
        public static double importedCases = Scripts.RandomNumber(maxImported);
        public static int dayIncrement = 1;
        public static int day = 0;

        // Draw application title (to be used for draw methods so this long stretch of code doesn't have to be rewritten)
        public static void DrawTitle() 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[New Zealand Virus Simulator]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("By jbapps (Jaedon Burgess) - 2021");
            Console.ForegroundColor = ConsoleColor.White;
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

    }
}
