using System;

namespace NZVirusSimulator 
{
    class MainMenu
    {
        public static void Main(string[] args)
        {
            Draw();
        }

        public static void Draw()
        {
            Console.Clear();
            Scripts.DrawTitle();
        }
    }
}
