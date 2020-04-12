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
    // Description: Demonstration of implementing CRUD with classes
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
        const string NEW_LINE = "\n";

        static void Main(string[] args)
        {
            List<Monster> monsters = InitializeMonsterList();

            SetTheme(ConsoleColor.White, ConsoleColor.DarkBlue);

            DisplayWelcomeScreen();
            DisplayMainMenu(monsters);
            DisplayClosingScreen();
        }

        /// <summary>
        /// update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            string userResponse;

            DisplayScreenHeader("Update Monster");

            //
            // get monster to update
            //
            int id = GetValidMonsterId(monsters);
            Monster monster = monsters.FirstOrDefault(m => m.Id == id);

            if (monster != null)
            {
                Console.WriteLine();
                //
                // Note: Id property must not be updated
                //

                //
                // ***** Name *****
                //
                Console.Write(TAB + $"Name: {monster.Name} >");
                userResponse = Console.ReadLine();
                if (userResponse != "") monster.Name = userResponse;

                //
                // ***** Age *****
                //
                Console.Write(TAB + $"Age: {monster.Age} >");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (!int.TryParse(userResponse, out int age))
                    {
                        Console.WriteLine(TAB + "You must enter an integer value between 0 and 1000.");
                        Console.WriteLine(TAB + "Please try again.");
                        Console.WriteLine();
                        GetIsValidInteger(TAB + $"Age: {monster.Age} >", 0, 1000, 3, out age);
                    }
                    monster.Age = age;
                }

                //
                // ***** Mood *****
                //
                //
                // list all valid moods
                //
                foreach (Monster.Attitude mood in Enum.GetValues(typeof(Monster.Attitude)))
                {
                    if (mood != Monster.Attitude.none)
                    {
                        Console.Write(TAB + mood);
                    }
                }
                Console.Write(NEW_LINE + TAB + $"Mood: {monster.Mood} >");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (!Enum.TryParse(userResponse, out Monster.Attitude mood))
                    {
                        Console.WriteLine(TAB + "You must enter a proper mood name.");
                        Console.WriteLine(TAB + "Please try again.");
                        Console.WriteLine();
                        GetIsValidEnum<Monster.Attitude>(TAB + $"Mood: {monster.Mood} >", 3, out mood);
                    }
                    monster.Mood = mood;
                }

                //
                // ***** Alive *****
                //
                Console.Write(TAB + $"Alive: {(monster.IsAlive ? "Yes" : "No")} >");
                userResponse = Console.ReadLine().ToLower();
                if (userResponse != "")
                {
                    if (!(userResponse == "yes" || userResponse == "no"))
                    {
                        Console.WriteLine(TAB + "You must enter either \"yes\" or \"no\".");
                        Console.WriteLine(TAB + "Please try again.");
                        Console.WriteLine();
                        GetIsValidYesNoBool($"Alive: {(monster.IsAlive ? "Yes" : "No")} >", 3, out bool alive);
                        monster.IsAlive = alive; // TODO check code
                    }
                    else
                    {
                        monster.IsAlive = (userResponse == "yes") ? true : false;
                    }
                }

                //
                // ***** Children *****
                //
                DisplayUpdateChildren(monster);

                //
                // ***** Inventory *****
                //
                Console.WriteLine(TAB + "Updating the inventory will be available in a later version."); //TODO Add 

                //
                // ***** Treasure Chest *****
                //
                DisplayUpdateTreasureChest(monster);

                DisplayMenuPrompt("Main");
            }

            // TODO - add else to Update monster method
        }

        /// <summary>
        /// add new monster and set properties
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster monster = new Monster();

            DisplayScreenHeader("Add Monster");

            Console.WriteLine(TAB + "Enter all of the properties for the new monster.");
            DisplayContinuePrompt();

            Console.Write(TAB + "Name: ");
            monster.Name = Console.ReadLine();
            GetIsValidInteger(TAB + "Age: ", 1, 1000, 3, out int age);
            monster.Age = age;
            GetIsValidEnum<Monster.Attitude>(TAB + "Mood [happy, sad, angry, nice]: ", 3, out Monster.Attitude mood);
            monster.Mood = mood;
            monster.IsAlive = true; // all monsters are created alive

            DisplayScreenHeader("Add Children");
            DisplayAddChildren(monster);

            DisplayScreenHeader("Add Monster");
            DisplayAddInventoryItems(monster);

            DisplayScreenHeader("Add Monster");
            DisplayUpdateTreasureChest(monster);

            //
            // get next id number and set the new monster's id
            //
            monster.Id = monsters.Max(m => m.Id) + 1;

            monsters.Add(monster);

            DisplayScreenHeader("Add Monster");
            Console.WriteLine(TAB + $"{monster.Name} has been added to the list of monsters.");

            DisplayMenuPrompt("Main");
        }

        /// <summary>
        /// add children to the monster
        /// </summary>
        /// <param name="monster">monster</param>
        static void DisplayAddChildren(Monster monster)
        {
            string name;

            DisplayScreenHeader("Add Children");

            Console.WriteLine(TAB + "Enter the name of the child.");
            Console.WriteLine(TAB + "To stop adding children, enter \"done\" for the name.");
            Console.WriteLine();

            do
            {
                Console.Write(TAB + "Child name: ");
                name = Console.ReadLine();

                if (name.ToLower() == "done") break;

                monster.Children.Add(name);
            } while (true); // continue looping until the break command is executed

            Console.WriteLine();
            Console.WriteLine(TAB + $"You have added {monster.Children.Count} children to the monster.");
            Console.WriteLine();
            ChildrenList(monster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// update children
        /// </summary>
        /// <param name="monster">monster</param>
        static void DisplayUpdateChildren(Monster monster)
        {
            string userResponse;

            //
            // lists used in the foreach statement cannot be modified in the loop
            // generate a new list of children names to reference in the foreach loop
            //
            List<string> childrenNames = new List<string>(monster.Children);

            DisplayScreenHeader("Update Children");

            ChildrenList(monster);

            Console.WriteLine();
            Console.WriteLine(TAB + "To keep the current child name, type Enter.");
            Console.WriteLine(TAB + "To keep the delete child name, type \"delete\".");
            Console.WriteLine(TAB + "To keep the change child name, type the new name.");
            Console.WriteLine();

            foreach (string child in childrenNames)
            {
                Console.Write(TAB + $"{child}: >");
                userResponse = Console.ReadLine();
                if (userResponse == "delete")
                {
                    monster.Children.Remove(child);
                }
                else if (userResponse != "")
                {
                    monster.Children[monster.Children.IndexOf(child)] = userResponse;
                }
            }

            Console.WriteLine();
            ChildrenList(monster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// update treasure chest quantities
        /// </summary>
        /// <param name="monster">monster</param>
        static void DisplayUpdateTreasureChest(Monster monster)
        {
            string userResponse;
            int quantity;

            DisplayScreenHeader("Update Treasure Chest");

            Console.WriteLine(TAB + "To keep the current quantity, just type Enter.");
            Console.WriteLine();

            //
            // cannot iterate through a dictionary and change values
            // therefore, create a list of TreasureChest keys to iterate through
            //
            List<TreasureType> treasureTypesKeys = new List<TreasureType>(monster.TreasureChest.Keys);

            foreach (TreasureType treasureTypeKey in treasureTypesKeys)
            {
                Console.Write(TAB + $"{treasureTypeKey}: {monster.TreasureChest[treasureTypeKey]} >");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    if (!int.TryParse(userResponse, out quantity))
                    {
                        Console.WriteLine(TAB + "You must enter an integer value between 0 and 1000.");
                        Console.WriteLine(TAB + "Please try again.");
                        Console.WriteLine();
                        GetIsValidInteger(TAB + $"{treasureTypeKey}: {monster.TreasureChest[treasureTypeKey]} >", 0, 1000, 3, out quantity);
                    }
                    monster.TreasureChest[treasureTypeKey] = quantity;
                }
            }

            Console.WriteLine();
            TreasureChestList(monster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// add items to the monster's inventory
        /// </summary>
        /// <param name="monster">monster</param>
        static void DisplayAddInventoryItems(Monster monster)
        {
            string name;
            int quantity;

            DisplayScreenHeader("Add Inventory Items");

            Console.WriteLine(TAB + "Enter the name of the new inventory item and then the quantity.");
            Console.WriteLine(TAB + "To stop adding items, enter \"done\" for the name.");
            Console.WriteLine();

            do
            {
                Console.Write(TAB + "Item name: ");
                name = Console.ReadLine();

                if (name.ToLower() == "done") break;

                GetIsValidInteger(TAB + "Item quantity: ", 1, 1000, 3, out quantity); // not checking for maximum attempts
                monster.Inventory.Add((name, quantity));
                Console.WriteLine();
            } while (true); // continue looping until the break command is executed

            Console.WriteLine();
            Console.WriteLine(TAB + $"You have added {monster.Inventory.Count} items to the monster's inventory.");
            Console.WriteLine();
            InventoryList(monster);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// delete a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
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
                GetIsValidYesNo($"Are you sure you want to delete the monster named {monster.Name}?", 3, out string yesNoChoice);
                if (yesNoChoice.ToLower() == "yes")
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
                    Children = new List<string>()
                    {
                        "Fred",
                        "Mary",
                        "Tom"
                    },
                    Inventory = new List<(string itemName, int quantity)>()
                    {
                        ("bread", 2),
                        ("sword", 3),
                        ("potion", 1)
                    },
                    TreasureChest = new Dictionary<TreasureType, int>()
                    {
                        {TreasureType.gold, 10},
                        {TreasureType.silver, 0},
                        {TreasureType.bronze, 4},
                        {TreasureType.diamond, 10},
                        {TreasureType.ruby, 0},
                        {TreasureType.emerald, 1}
                    }
                },

                new Monster()
                {
                    Id = 1002,
                    Name = "Suzy",
                    IsAlive = true,
                    Age = 113,
                    Mood = Monster.Attitude.happy,
                    Children = new List<string>()
                    {
                        "Emma"
                    },
                    Inventory = new List<(string itemName, int quantity)>()
                    {
                        ("rose", 2),
                        ("knife", 3),
                        ("potion", 3)
                    },
                    TreasureChest = new Dictionary<TreasureType, int>()
                    {
                        {TreasureType.gold, 1},
                        {TreasureType.silver, 0},
                        {TreasureType.bronze, 42},
                        {TreasureType.diamond, 1},
                        {TreasureType.ruby, 0},
                        {TreasureType.emerald, 1}
                    }
                }
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
            ChildrenList(monster);
            Console.WriteLine();
            InventoryList(monster);
            Console.WriteLine();
            TreasureChestList(monster);
        }

        /// <summary>
        /// generate and display a table of the Monster object's children
        /// </summary>
        /// <param name="monster">Monster object</param>
        static void ChildrenList(Monster monster)
        {
            Console.WriteLine("\tCurrent Children");
            Console.WriteLine(
                TAB +
                "Child".PadRight(20));
            Console.WriteLine(
                TAB +
                "---------".PadRight(20));
            foreach (string child in monster.Children)
            {
                Console.WriteLine(
                    TAB +
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
                        DisplayAddMonster(monsters);
                        break;

                    case 'd':
                        DisplayUpdateMonster(monsters);
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

            Console.CursorVisible = true;
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
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine(TAB + $"Press any key to return to the {menuName} Menu.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(TAB + TAB + headerText);
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
        /// prompt the user and get a yes/no response
        /// </summary>
        /// <param name="prompt">prompt message</param>
        /// <returns>true if valid response</returns>
        private static bool GetIsValidYesNo(string prompt, int maximumAttempts, out string yesNoChoice)
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(TAB + prompt + " [yes/no]:");
                yesNoChoice = Console.ReadLine().ToLower();

                if (yesNoChoice == "yes" || yesNoChoice == "no")
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine(TAB + $"You must enter either \"yes\" or \"no\". Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            return validResponse;
        }

        /// <summary>
        /// prompt the user and get a yes/no response and translates the response as a bool
        /// </summary>
        /// <param name="prompt">prompt message</param>
        /// <returns>true if valid response</returns>
        private static bool GetIsValidYesNoBool(string prompt, int maximumAttempts, out bool yesNoBoolChoice)
        {
            bool validResponse = false;
            string userResponse;
            int attempts = 0;

            yesNoBoolChoice = false;

            do
            {
                Console.Write(TAB + prompt + " [yes/no]:");
                userResponse = Console.ReadLine().ToLower();

                if (userResponse == "yes" || userResponse == "no")
                {
                    yesNoBoolChoice = userResponse == "yes" ? true : false;
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine(TAB + $"You must enter either \"yes\" or \"no\". Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            return validResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="prompt">prompt message</param>
        /// <param name="maximumAttempts"></param>
        /// <param name="validEnum"></param>
        /// <returns></returns>
        private static bool GetIsValidEnum<TEnum>(string prompt, int maximumAttempts, out TEnum validEnum) where TEnum : struct
        {
            bool validResponse = false;
            int attempts = 0;

            do
            {
                Console.Write(prompt);

                if (Enum.TryParse<TEnum>(Console.ReadLine(), true, out validEnum))
                {
                    validResponse = true;
                }
                else
                {
                    Console.WriteLine(TAB + $"You must enter a {nameof(TEnum)} value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            return validResponse;
        }

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
                    Console.WriteLine(TAB + $"You must enter an integer value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

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
                        Console.WriteLine(TAB + $"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
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
                    Console.WriteLine(TAB + $"You must enter an double value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

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
                        Console.WriteLine(TAB + $"You must enter a double value between {minimumValue} and {maximumValue}. Please try again.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine(TAB + $"You must enter an double value. Please try again.");
                    Console.WriteLine();
                }

                attempts++;

            } while (!validResponse && attempts < maximumAttempts);

            return validResponse;
        }

        #endregion
    }
}