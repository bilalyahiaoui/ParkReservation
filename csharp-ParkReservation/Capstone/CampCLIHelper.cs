using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public static class CampCLIHelper
    {
        public static DateTime GetUserDateTime(string entry)
        {
            string userInput = String.Empty;
            DateTime dateValue = DateTime.MinValue;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Sorry, you entered a invalid date format. Please try again");
                }

                Console.Write(entry + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!DateTime.TryParse(userInput, out dateValue));

            return dateValue;
        }

        public static int GetUserInt(string entry)
        {
            string userInput = String.Empty;
            int intValue = 0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Sorry, you entered a invalid integer format. Please try again");
                }

                Console.Write(entry + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!int.TryParse(userInput, out intValue));

            return intValue;

        }


        public static double GetUserDouble(string entry)
        {
            string userInput = String.Empty;
            double doubleValue = 0.0;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Sorry, you entered a invalid double format. Please try again");
                }

                Console.Write(entry + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (!double.TryParse(userInput, out doubleValue));

            return doubleValue;

        }
        
        public static string GetUserString(string entry)
        {
            string userInput = String.Empty;
            int numberOfAttempts = 0;

            do
            {
                if (numberOfAttempts > 0)
                {
                    Console.WriteLine("Sorry, you entered a invalid string format. Please try again");
                }

                Console.Write(entry + " ");
                userInput = Console.ReadLine();
                numberOfAttempts++;
            }
            while (String.IsNullOrEmpty(userInput));

            return userInput;
        }
        public static string BoolToString(int number)
        {
            if(number== 0)
            {
                return "No";
            }
            else
            {
                return "Yes";
            }
        }
        public static string NumberToString(int number)
        {
            if (number == 0)
            {
                return "N/A";
            }
            else
            {
                return number.ToString();
            }
        }
    }
}
