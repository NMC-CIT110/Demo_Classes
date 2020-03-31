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
        const string TAB = "\t";

        static void Main(string[] args)
        {
            List<Monster> monsters = InitializeMonsterList();

            SetTheme(ConsoleColor.White, ConsoleColor.DarkBlue);

            DisplayWelcomeScreen();
            DisplayMainMenu(monsters);
            DisplayClosingScreen();
        }

        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // get monster to delete
            //
            int id = GetValidMonsterId(monsters);
            Monster monster = monsters.FirstOrDefault(m => m.Id == id);

            if (monster != null)
            {
                Console.WriteLine();
                Console.Write(TAB + $"Are you sure you want to delete the monster named {monster.Name}?");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    monsters.Remove(monster);
                    Console.WriteLine();
                    Console.WriteLine(TAB + $"{monster.Name} has been deleted.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(TAB + "Unable to locate the monster.");
            }

            DisplayMenuPrompt("Main");
        }

        /// <summary>
        /// ***** SCREEN: Monster Detail *****
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // get monster to display
            //
            int id = GetValidMonsterId(monsters);
            Monster monster = monsters.FirstOrDefault(m => m.Id == id);

            if (monster != null)
            {
                MonsterDetail(monster);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(TAB + "Unable to locate the monster.");
            }

            DisplayMenuPrompt("Main");
        }

        /// <summary>
        /// get a valid monster id from the user
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// <returns>monster id</returns>
        static int GetValidMonsterId(List<Monster> monsters)
        {
            bool validId = false;
            int id;

            //
            // generate a list of valid Monster object ids
            //
            List<int> validIdNumbers = monsters.Select(m => m.Id).ToList();

            do
            {
                DisplayScreenHeader("Choose Monster");

                Console.WriteLine(TAB + "Choose a monster by entering the Id below.");
                MonsterListTable(monsters);
                GetIsValidInteger(TAB + "Enter monster id:", 3, out id); // not implementing maximum attempts

                if (validIdNumbers.Contains(id))
                {
                    validId = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(TAB + $"{id} is not a valid monster Id.");
                    Console.WriteLine(TAB + "PLease try again.");
                    DisplayContinuePrompt();
                }

            } while (!validId);

            return id;
        }

        /// <summary>
        /// display a list of monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMonsterList(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster List");

            MonsterListTable(monsters);

            DisplayMenuPrompt("Main");
        }

        /// <summary>
        /// generate a table of monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void MonsterListTable(List<Monster> monsters)
        {
            Console.WriteLine(
                TAB +
                "Id".PadRight(10) +
                "Name".PadRight(20) +
                "Alive".PadRight(10));
            Console.WriteLine(
                TAB +
                "----".PadRight(10) +
                "--------".PadRight(20) +
                "--------".PadRight(10));

            foreach (Monster monster in monsters)
            {
                Console.WriteLine(
                    TAB +
                    monster.Id.ToString().PadRight(10) +
                    monster.Name.PadRight(20) +
                    (monster.IsAlive ? "Yes" : "No").PadRight(10));
            }
        }

        /// <summary>
        /// initialize a Monster object and set the properties
        /// </summary>
        /// <returns>a Monster object with all properties set</returns>
        static List<Monster> InitializeMonsterList()
        {
            //
            // use a list initializer for the Monster list
            // use a list initializer in each Monster object's inventory.
            // use a dictionary initializer in each Monster object's treasure chest
            //
            List<Monster> monsters = new List<Monster>()
            {
                new Monster()
                {
                    Id = 1001,
                    Name = "Sid",
                    IsAlive = true,
                    Age = 145,
                    Mood = Monster.Attitude.happy,
                    Inventory = new List<(string itemName, int quantity)>()
                    {
                        ("bread", 2),
                        ("sword", 3),
                        ("potion", 1)
                    },
                    TreasureChest = new Dictionary<TreasureType, int>()
                    {
                        {TreasureType.gold, 4},
                        {TreasureType.silver, 12},
                        {TreasureType.diamond, 1}
                    }
                },

                new Monster()
                {
                    Id = 1002,
                    Name = "Suzy",
                    IsAlive = true,
                    Age = 113,
                    Mood = Monster.Attitude.happy,
                    Inventory = new List<(string itemName, int quantity)>()
                    {
                        ("rose", 2),
                        ("knife", 3),
                        ("potion", 3)
                    },
                    TreasureChest = new Dictionary<TreasureType, int>()
                    {
                        {TreasureType.gold, 1},
                        {TreasureType.bronze, 42},
                        {TreasureType.ruby, 1}
                    }
                },
            };

            return monsters;
        }

        /// <summary>
        /// display a block of monster details
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void MonsterDetail(Monster monster)
        {
            DisplayScreenHeader($"{monster.Name}'s Information");

            Console.WriteLine($"\tId: {monster.Id}");
            Console.WriteLine($"\tAlive: {(monster.IsAlive ? "Yes" : "No")}");
            Console.WriteLine($"\tAge: {monster.Age}");
            Console.WriteLine($"\tMood: {monster.Mood}");
            Console.WriteLine();
            InventoryList(monster);
            Console.WriteLine();
            TreasureChestList(monster);
        }

        /// <summary>
        /// generate and display a table of the Monster object's inventory
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void InventoryList(Monster monster)
        {
            Console.WriteLine("\tCurrent Inventory");
            Console.WriteLine(
                TAB +
                "Item Name".PadRight(20) +
                "Quantity".PadRight(12));
            Console.WriteLine(
                TAB +
                "---------".PadRight(20) +
                "--------".PadRight(12));
            foreach (var item in monster.Inventory)
            {
                Console.WriteLine(
                    TAB +
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
                TAB +
                "Treasure Name".PadRight(20) +
                "Quantity".PadRight(12) +
                "Value".PadRight(12));
            Console.WriteLine(
                TAB +
                "---------".PadRight(20) +
                "--------".PadRight(12) +
                "--------".PadRight(12));

            foreach (var treasureItem in monster.TreasureChest)
            {
                treasureItemValue = TreasureItemValue(treasureItem.Key, treasureItem.Value);
                totalTreasureValue += treasureItemValue;

                Console.WriteLine(
                    TAB +
                    treasureItem.Key.ToString().PadRight(20) +
                    treasureItem.Value.ToString().PadRight(12) +
                    treasureItemValue.ToString().PadRight(12));
            }
            Console.WriteLine();
            Console.WriteLine(
                TAB +
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
        /// ***** SCREEN: Main Menu *****
        /// </summary>
        static void DisplayMainMenu(List<Monster> monsters)
        {
            bool quitMainMenu = false;
            ConsoleKeyInfo menuChoiceKey;
            char menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                Console.WriteLine(TAB + "a) List All Monsters");
                Console.WriteLine(TAB + "b) Monster Detail");
                Console.WriteLine(TAB + "c) Add Monster");
                Console.WriteLine(TAB + "d) Update Monster");
                Console.WriteLine(TAB + "e) Delete Monster");
                Console.WriteLine(TAB + "q) Quit");
                Console.WriteLine();

                Console.Write(TAB + "Menu Choice:");

                Console.CursorVisible = false;
                menuChoiceKey = Console.ReadKey();
                menuChoice = menuChoiceKey.KeyChar;

                switch (menuChoice)
                {
                    case 'a':
                        DisplayMonsterList(monsters);
                        break;

                    case 'b':
                        DisplayMonsterDetail(monsters);
                        break;

                    case 'c':

                        break;

                    case 'd':

                        break;

                    case 'e':
                        DisplayDeleteMonster(monsters);
                        break;

                    case 'q':
                        quitMainMenu = true;
                        break;

                    default:
                        //
                        // feedback message for invalid response
                        //
                        Console.WriteLine();
                        Console.WriteLine(TAB + "----------------------------------------------------------------");
                        Console.WriteLine(TAB + "  It appears your have entered and invalid menu choice.");
                        Console.WriteLine(TAB + "  Please try again by entering the letter of your menu choice.");
                        Console.WriteLine(TAB + "----------------------------------------------------------------");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMainMenu);
        }

        /// <summary>
        /// ***** SCREEN: Welcome *****
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tDemonstration of Classes and CRUD Management");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// ***** SCREEN: Closing *****
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("End of Demonstration");

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to exit.");
            Console.ReadKey();
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
            Console.WindowHeight = 30;
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