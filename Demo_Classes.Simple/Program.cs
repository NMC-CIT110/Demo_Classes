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
    // Note: the values must all be unique
    //
    public enum TreasureType
    {
        gold = 50,
        silver = 25,
        bronze = 10,
        diamond = 100,
        ruby = 75,
        emerald = 60
    }

    class Program
    {
        static void Main(string[] args)
        {
            Monster sid;
            sid = InitializeSid();

            SetTheme(ConsoleColor.White, ConsoleColor.DarkBlue);

            DisplayWelcomeScreen();
            DisplayMonsterDetail(sid);
            DisplayClosingScreen();
        }

        /// <summary>
        /// initialize a Monster object and set the properties
        /// </summary>
        /// <returns>a Monster object with all properties set</returns>
        static Monster InitializeSid()
        {
            //
            // instantiate (create) a Monster object named "sid" using the constructor with parameters
            //
            Monster sid = new Monster(1001,"Sid", 145, Monster.Attitude.nice, true);

            //
            // add children to Monster object
            //
            sid.Children.Add("Fred");
            sid.Children.Add("Mary");
            sid.Children.Add("Debbie");

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

        /// <summary>
        /// **********************************************************
        /// *                                                        *
        /// *        Display All Monster Properties Screen           *
        /// *                                                        *
        /// **********************************************************
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void DisplayMonsterDetail(Monster monster)
        {
            DisplayScreenHeader($"{monster.Name}'s Information");

            Console.WriteLine($"\tId: {monster.Id}");
            Console.WriteLine($"\tAlive: {(monster.IsAlive ? "Yes" : "No")}");
            Console.WriteLine($"\tAge: {monster.Age}");
            Console.WriteLine($"\tMood: {monster.Mood}");

            Console.WriteLine();
            ChildrenList(monster);

            Console.WriteLine();
            InventoryList(monster);

            Console.WriteLine();
            TreasureChestList(monster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// generate and display a table of the Monster object's children
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void ChildrenList(Monster monster)
        {
            Console.WriteLine("\tCurrent Children");
            Console.WriteLine(
                "\t" +
                "Child".PadRight(20));
            Console.WriteLine(
                "\t" +
                "---------".PadRight(20));
            foreach (string child in monster.Children)
            {
                Console.WriteLine(
                    "\t" +
                    child.PadRight(20));
            }
        }

        /// <summary>
        /// generate and display a table of the Monster object's inventory
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void InventoryList(Monster monster)
        {
            Console.WriteLine("\tCurrent Inventory");
            Console.WriteLine(
                "\t" +
                "Item Name".PadRight(20) +
                "Quantity".PadRight(12));
            Console.WriteLine(
                "\t" +
                "---------".PadRight(20) +
                "--------".PadRight(12));
            foreach (var item in monster.Inventory)
            {
                Console.WriteLine(
                    "\t" +
                    item.itemName.PadRight(20) +
                    item.quantity.ToString().PadRight(12));
            }
        }

        /// <summary>
        /// generate and display a table of the Monster object's treasure chest
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void TreasureChestList(Monster monster)
        {
            int treasureItemValue;
            int totalTreasureValue = 0;

            Console.WriteLine("\tCurrent Treasure Chest Contents");
            Console.WriteLine(
                "\t" +
                "Treasure Name".PadRight(20) +
                "Quantity".PadRight(12) +
                "Value".PadRight(12));
            Console.WriteLine(
                "\t" +
                "---------".PadRight(20) +
                "--------".PadRight(12) +
                "--------".PadRight(12));

            foreach (var treasureItem in monster.TreasureChest)
            {
                treasureItemValue = TreasureItemValue(treasureItem.Key, treasureItem.Value);
                totalTreasureValue += treasureItemValue;

                Console.WriteLine(
                    "\t" +
                    treasureItem.Key.ToString().PadRight(20) +
                    treasureItem.Value.ToString().PadRight(12) +
                    treasureItemValue.ToString().PadRight(12));
            }
            Console.WriteLine();
            Console.WriteLine(
                "\t" +
                "Total ".PadLeft(32) +
                totalTreasureValue.ToString().PadRight(12));
        }

        /// <summary>
        /// calculate the total value of a specific treasure item in the treasure chest
        /// </summary>
        /// <param name="treasureItem">treasure item</param>
        /// <param name="quantity">quantity</param>
        /// <returns>total value for the treasure item</returns>
        static int TreasureItemValue(TreasureType treasureItem, int quantity)
        {
            return quantity * (int)treasureItem;
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
            Console.WriteLine("\t\tSimple Demonstration of Classes");
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
            Console.WriteLine("\t\tEnd of Demonstration");
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
            Console.WindowHeight = 40;
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