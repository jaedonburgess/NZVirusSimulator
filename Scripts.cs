using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator 
{
    class Scripts 
    {
        public static void DrawTitle() 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" -----------------");
            Console.WriteLine("|   New Zealand   |");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("| Virus Simulator |");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("|   By Jaedon B   |");
            Console.WriteLine(" -----------------");
            Console.WriteLine();
        }
    }
}
