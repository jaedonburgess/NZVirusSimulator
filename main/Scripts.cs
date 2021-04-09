using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator 
{
    class Scripts 
    {
        

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
