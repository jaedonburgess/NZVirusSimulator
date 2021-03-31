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
            Draw();

            Scripts.MenuReturn(); // Returns to main menu after pressing enter
        }

        // Draw interface and results of simulation
        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle("Simulation");
            Console.WriteLine("Simulating Day {0}:", Scripts.day);

        }
    }
}
