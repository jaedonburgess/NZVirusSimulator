﻿using System;
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
            Console.WriteLine("[New Zealand Virus Simulator]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("By jbapps (Jaedon Burgess) - 2021");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
