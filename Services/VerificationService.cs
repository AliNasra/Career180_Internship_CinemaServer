using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class VerificationService
    {
        public static bool isValidEmail(string email)
        {
            var regularExpression = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (regularExpression.IsMatch(email))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool isValidBirthDate(string birthDateString) {
            DateTime dt;
            bool dateConverted = DateTime.TryParse(birthDateString, out dt);
            if (dateConverted)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        public static bool isValidCategory(string category)
        {
            if (category == "1" || category == "2")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isValidUserName(string userName) {
            var regularExpression = new Regex(@"^[^\b]*$");
            if (regularExpression.IsMatch(userName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isValidPassword(string password)
        {
            var regularExpression = new Regex(@"^[^\b]*$");
            if (regularExpression.IsMatch(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static DateTime readPremierDate()
        {
            Console.WriteLine("Enter the date (format: MM/dd/yyyy):");
            string dateInput = Console.ReadLine();
            Console.WriteLine("Enter the time (format: h:mm tt, e.g., 1:00 PM):");
            string timeInput = Console.ReadLine().Trim();
            try
            {
                if (!timeInput.ToLower().EndsWith("pm") && !timeInput.ToLower().EndsWith("am"))
                {
                    Console.WriteLine("Time must be rounded to the nearest hour");
                    return DateTime.MinValue;
                }
                string combinedInput = $"{dateInput} {timeInput}";
                DateTime parsedDateTime = DateTime.ParseExact(combinedInput, "MM/dd/yyyy h:mm tt", null);
                return parsedDateTime;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid date or time format. Please try again.");
                return DateTime.MinValue;
            }        
        }
        public static int getPositiveIntegerInput(string promptMessage = "Please insert a positive integer")
        {
            Console.WriteLine(promptMessage);
            string stringInput = Console.ReadLine();
            try
            {
                int numericalInput = Convert.ToInt32(stringInput);
                if (numericalInput < 0)
                {
                    return -1;
                }
                else
                {
                    return numericalInput;
                }
            }
            catch (Exception e) {
                return -1;
            }
        }

        public static double getPositiveDoubleInput(string promptMessage = "Please insert a positive decimal number")
        {
            Console.WriteLine(promptMessage);
            string stringInput = Console.ReadLine();
            try
            {
                double numericalInput = Convert.ToDouble(stringInput);
                if (numericalInput < 0)
                {
                    return -1;
                }
                else
                {
                    return numericalInput;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }
    }
}
