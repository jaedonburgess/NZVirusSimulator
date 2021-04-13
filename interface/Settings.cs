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
            Console.WriteLine();
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Virus Name: {0}", Simulation.virusName);
            Console.WriteLine("Virus R Value: {0}", Simulation.rValue);
            Console.WriteLine("Fatality Rate: {0}", Simulation.fatalityRate);
            Console.WriteLine("Starting Budget: ${0}", Simulation.budget);
            Console.WriteLine("Vaccine Cost/Dose: ${0}", Simulation.vaccineCost);
            Console.WriteLine("-----------------------------------");
            DrawMenu(read);
        }

        // Draw settings menu interface
        public static void DrawMenu(bool read)
        {
            Console.WriteLine();
            Console.WriteLine(" -----------------------------");
            Console.WriteLine("| 1: Virus Name               |");
            Console.WriteLine("| 2: R-Value                  |");
            Console.WriteLine("| 3: Fatality Rate            |");
            Console.WriteLine("| 4: Budget                   |");
            Console.WriteLine("| 5: Vaccine Cost/Dose        |");
            Console.WriteLine("| 6: Reset to Defaults        |");
            Console.WriteLine("| 7: Exit                     |");
            Console.WriteLine(" -----------------------------");
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
                // Change Virus Name
                case 1:
                    Simulation.virusName = StringReadHandler(); // virusName = The value returned by the string read handler
                    Draw(true);
                    break;
                // Change R-Value
                case 2:
                    Simulation.rValue = DoubleReadHandler(2); // fatalityRate = The value returned by the double read handler rounded to 2 decimal places
                    Draw(true);
                    break;
                // Change Fatality Rate
                case 3:
                    Simulation.fatalityRate = DoubleReadHandler(2); // fatalityRate = The value returned by the double read handler rounded to 2 decimal places
                    Draw(true);
                    break;
                // Change Budget
                case 4:
                    Simulation.budget = DoubleReadHandler(0); // budget = The value returned by the double read handler rounded to 0 decimal places
                    Draw(true);
                    break;
                // Change Vaccine Cost/Dose
                case 5:
                    Simulation.vaccineCost = IntReadHandler(); // vaccineCost = The value returned by the integer read handler
                    Draw(true);
                    break;
                // Asks if you want to reset, then it resets
                case 6:
                    // Ask for Y or N input
                    while (yn == "")
                    {
                        Console.WriteLine("Are you sure you wish to proceed? [Y/N]");
                        yn = Scripts.ReadString();
                        if (yn == "y" || yn == "Y") // if yes reset
                        {
                            Simulation.ResetDefaults();
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
                case 7:
                    MainMenu.Draw();
                    break;
                default:
                    Console.WriteLine("Error: Please enter a valid option [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(true);
                    break;
            }
        }

        // Application specific handle of the ReadDb() method
        public static double DoubleReadHandler(int roundingValue)
        {
            double tempDb = 0; // Temporary Double Value

            while (tempDb == 0)
            {
                // Read double
                Console.WriteLine("Please enter a value:");
                tempDb = Scripts.ReadDb();

                if (tempDb == Scripts.intError) // intError applies to doubles as well
                {
                    Console.WriteLine("Error: The value you entered is not valid [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(false); // Draw without running the ReadMenu() method
                    tempDb = 0; // Reset tempStr if an error occurs
                    continue;
                }
            }
            return Math.Round(tempDb, roundingValue, MidpointRounding.ToEven); // Return rounded value of the number entered
        }

        // Application specific handle of the ReadString() method
        public static string StringReadHandler()
        {
            string tempStr = ""; // Temporary String Value

            while (tempStr == "")
            {
                // Read String
                Console.WriteLine("Please enter a string:");
                tempStr = Scripts.ReadString();

                if (tempStr == Scripts.strError)
                {
                    Console.WriteLine("Error: The string you entered is not valid [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(false); // Draw without running the ReadMenu() method
                    tempStr = ""; // Reset tempStr if an error occurs
                    continue;
                }
            }
            return tempStr;
        }

        // Application specific handle of the ReadInt() method
        public static int IntReadHandler()
        {
            int tempInt = 0; // Temporary Integer Value

            while (tempInt == 0)
            {
                // Read integer
                Console.WriteLine("Please enter an integer value:");
                tempInt = Scripts.ReadInt();

                if (tempInt == Scripts.intError) // intError applies to doubles as well
                {
                    Console.WriteLine("Error: The value you entered is not valid integer [Please Wait...]");
                    Thread.Sleep(2000);
                    Draw(false); // Draw without running the ReadMenu() method
                    tempInt = 0; // Reset tempStr if an error occurs
                    continue;
                }
            }
            return tempInt;
        }
    }
}
