using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Classes
{
    // **************************************************
    //
    // Title: Demo - Classes
    // Description: Demonstration of implementing simple classes
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 3/28/2020
    // Last Modified: 
    //
    // **************************************************
    
    //
    // notice that the index values are set explicitly
    // these values will represent the value of each TreasureType
    //
    public enum TreasureType
    {
        gold = 50,
        silver = 25,
        bronze = 10,
        diamond = 100,
        ruby = 75,
        emerald = 50
    }

    class Program
    {
        static void Main(string[] args)
        {

            DisplayContinuePrompt();
        }

        static Monster InitializeSid()
        {
            //
            // instantiate (create) a Monster object named "sid" using the constructor with parameters
            //
            Monster sid = new Monster("Sid", 145, Monster.Attitude.nice, true);

            //
            // add items and quantities to the Monster object's Inventory property
            //
            sid.Inventory.Add(("Apples", 12));
            sid.Inventory.Add(("Swords", 2));
            sid.Inventory.Add(("Dogs", 1));

            //
            // add treasure to the Monster object's treasure chest
            //
            sid.TreasureChest[TreasureType.gold] = 2;
            sid.TreasureChest[TreasureType.silver] = 5;
            sid.TreasureChest[TreasureType.emerald] = 11;

            return sid;
        }

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        /// <summary>
        /// Set background and foreground colors for the console
        /// </summary>
        /// <param name="background">background color</param>
        /// <param name="foreground">foreground color</param>
        static void SetTheme(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.Clear();
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// get a valid integer from the user
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="validInteger">out value</param>
        /// <returns>true if successful</returns>
        private static bool GetIsValidInteger(string prompt, int maximumAttempts, out int validInteger)
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out validInteger))
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an integer value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            Console.CursorVisible = false;

            return validResponse;
        }

        /// <summary>
        /// get a valid integer from the user within a specified range
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="minimumValue">min. value</param>
        /// <param name="maximumValue">max. value</param>
        /// <param name="validInteger">out value</param>
        /// <returns>true if successful</returns>
        private static bool GetIsValidInteger(string prompt, int minimumValue, int maximumValue, int maximumAttempts, out int validInteger)
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out validInteger))
                {
                    if (validInteger >= minimumValue && validInteger <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        Console.WriteLine($"\tYou must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an integer value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            Console.CursorVisible = false;

            return validResponse;
        }

        /// <summary>
        /// get a valid double from the user
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="validDouble">out value</param>
        /// <returns></returns>
        private static bool GetIsValidDouble(string prompt, int maximumAttempts, out double validDouble)
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(prompt);

                if (double.TryParse(Console.ReadLine(), out validDouble))
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an double value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            Console.CursorVisible = false;

            return validResponse;
        }

        /// <summary>
        /// get a valid double from the user within a specified range
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="minimumValue">min. value</param>
        /// <param name="maximumValue">max. value</param>
        /// <param name="validDouble">out value</param>
        /// <returns></returns>
        private static bool GetIsValidDouble(string prompt, double minimumValue, double maximumValue, int maximumAttempts, out double validDouble)
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(prompt);

                if (double.TryParse(Console.ReadLine(), out validDouble))
                {
                    if (validDouble >= minimumValue && validDouble <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        Console.WriteLine($"\tYou must enter a double value between {minimumValue} and {maximumValue}. Please try again.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an double value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            Console.CursorVisible = false;

            return validResponse;
        }

        #endregion
    }
}