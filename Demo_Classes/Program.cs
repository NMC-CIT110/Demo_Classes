using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Classes
{
    class Program
    {
        static void Main(string[] args)
        {


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

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// get a valid integer from the player - note: if max and min values are both 0, range validation is disabled
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="minimumValue">min. value</param>
        /// <param name="maximumValue">max. value</param>
        /// <param name="validInteger">out value</param>
        /// <returns>true if successful</returns>
        private static bool GetIsValidInteger(string prompt, int minimumValue, int maximumValue, int maximumAttempts, out int validInteger)
        {
            bool validResponse = false;
            int attempts = 1;
            validInteger = 0;

            //
            // validate on range if either minimumValue and maximumValue are not 0
            //
            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            Console.Write(prompt);
            while (!validResponse && attempts <= maximumAttempts)
            {
                if (int.TryParse(Console.ReadLine(), out validInteger))
                {
                    if (validateRange)
                    {
                        if (validInteger >= minimumValue && validInteger <= maximumValue)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            Console.WriteLine($"\tYou must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                            Console.WriteLine();
                            Console.Write(prompt);
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an integer value. Please try again.");
                    Console.WriteLine();
                    Console.Write(prompt);
                }
                attempts++;
            }

            Console.CursorVisible = false;

            //
            // check to see if user exceeded the maximum number of attempts allowed.
            //
            if (attempts <= maximumAttempts)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get a valid integer from the player - note: if max and min values are both 0, range validation is disabled
        /// </summary>
        /// <param name="prompt">prompt message in console</param>
        /// <param name="minimumValue">min. value</param>
        /// <param name="maximumValue">max. value</param>
        /// <param name="validDouble">out value</param>
        /// <returns></returns>
        private static bool GetValidDouble(string prompt, int minimumValue, int maximumValue, int maximumAttempts, out double validDouble)
        {
            bool validResponse = false;
            int attempts = 1;
            validDouble = 0;

            //
            // validate on range if either minimumValue and maximumValue are not 0
            //
            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            Console.Write(prompt);
            while (!validResponse && attempts <= maximumAttempts)
            {
                if (double.TryParse(Console.ReadLine(), out validDouble))
                {
                    if (validateRange)
                    {
                        if (validDouble >= minimumValue && validDouble <= maximumValue)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            Console.WriteLine($"\tYou must enter an numeric value between {minimumValue} and {maximumValue}. Please try again.");
                            Console.WriteLine();
                            Console.Write(prompt);
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    Console.WriteLine($"\tYou must enter an numeric value. Please try again.");
                    Console.WriteLine();
                    Console.Write(prompt);
                }
                attempts++;
            }

            Console.CursorVisible = false;

            //
            // check to see if user exceeded the maximum number of attempts allowed.
            //
            if (attempts <= maximumAttempts)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
