using System.Windows;
using System.Text.RegularExpressions;
using KMS2_02_LE_02_02_WPF.Model;

namespace KMS2_02_LE_02_02_WPF.Manager
{
    /// <summary>
    /// Provides functionality to notify users about invalid input.
    /// </summary>
    public static class FailManager
    {
        /// <summary>
        /// Displays a message box with the specified message to notify the user about invalid input.
        /// </summary>
        /// <param name="message">The message to be displayed in the message box.</param>
        public static void NotifyInvalidInput(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool ValidateInput(string field, string value, Person editedPerson)
        {
            switch (field)
            {
                case "Name":
                case "Surname":
                case "City":
                    if (!IsValidName(value))
                    {
                        NotifyInvalidInput($"{field} must contain only alphabetic characters and cannot be empty.");
                        return false;
                    }
                    break;
                case "Gender":
                    if (!IsValidGender(value))
                    {
                        NotifyInvalidInput("Gender must be 'M', 'F', or 'D'.");
                        return false;
                    }
                    break;
                case "Age":
                    if (!int.TryParse(value, out int age) || !IsValidAge(age))
                    {
                        NotifyInvalidInput("Age must be a number between 1 and 119.");
                        return false;
                    }
                    break;
                case "Zip":
                    if (!int.TryParse(value, out int zip) || !IsValidZip(value))
                    {
                        NotifyInvalidInput("Zip must be an integer and up to 8 digits long.");
                        return false;
                    }
                    break;
                default:
                    NotifyInvalidInput("Invalid field.");
                    return false;
            }
            return true;
        }

        private static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[a-zA-Z]+$");
        }

        private static bool IsValidGender(string gender)
        {
            return gender == "M" || gender == "F" || gender == "D";
        }

        private static bool IsValidAge(int age)
        {
            return age > 0 && age < 120;
        }

        private static bool IsValidZip(string zip)
        {
            return zip.Length <= 8 && int.TryParse(zip, out _);
        }
    }
}