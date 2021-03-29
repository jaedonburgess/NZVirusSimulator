using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NZVirusSimulator
{
    class Settings
    {
        public static void Start()
        {
            Draw(true);
        }

        public static void Draw(bool read) // bool is to set the read menu to true or false (read or not to read)
        {
            Console.Clear();
            Scripts.DrawTitle("Settings");
            Console.WriteLine("Virus Name: {0}", Scripts.virusName);
            Console.WriteLine("Virus R Value: {0}", Scripts.rValue);
            Console.WriteLine("Fatality Rate: {0}", Scripts.fatalityRate);
            Console.WriteLine("Maximum Imported Cases: {0} cases", Scripts.maxImported);
            Console.WriteLine("Day Incrementation: {0} days", Scripts.dayIncrement);
            Console.WriteLine("Starting Budget: {0}", Scripts.budget);
            DrawMenu(read);
        }

        // Draw settings menu interface
        public static void DrawMenu(bool read)
        {
            Console.WriteLine();
            Console.WriteLine(" -----------------------");
            Console.WriteLine("| 1: Change Settings    |");
            Console.WriteLine("| 2: Reset to Defaults  |");
            Console.WriteLine("| 3: Exit               |");
            Console.WriteLine(" -----------------------");
            Console.WriteLine();
            if (read) // Only read menu if boolean is true (for Y/N at the bottom)
            {
                ReadMenu(); //Asks for menu input
            }
        }

        //Method to read menu option
        public static void ReadMenu()
        {
            //Init variables
            int option = 0;
            string yn = "";

            //Read option
            Console.Write("Please enter an option: ");
            try
            {
                option = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Error: Please enter a postive whole integer [Please wait...]");
                Thread.Sleep(2000);
                Draw(true);
            }

            //Checks through options to execute valid options and reset invalid options
            switch (option)
            {
                case 1:
                    Console.WriteLine("This option is under construction [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(true);
                    break;
                case 2:
                    // Ask for Y or N input
                    while (yn == "")
                    {
                        Console.WriteLine("Are you sure you wish to proceed? [Y/N]");
                        yn = Scripts.ReadString();
                        if (yn == "y" || yn == "Y") // if yes reset
                        {
                            Scripts.ResetDefaults();
                            Draw(true);
                            break;
                        }
                        else if (yn == "n" || yn == "N") // if no do not reset
                        {
                            Draw(true);
                            break;
                        }
                        else if (yn != "y" || yn != "n" || yn != "Y" || yn != "N") // if Y then reset to default settings
                        {
                            Draw(false);
                            yn = "";
                            Console.WriteLine("Error: Please enter Y or N");
                            continue;
                        }
                        Draw(true);
                        break;
                    }
                    yn = "";
                    break;
                case 3:
                    MainMenu.Draw();
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(true);
                    break;
            }
        }
    }
}
