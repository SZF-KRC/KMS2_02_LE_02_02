using System.Windows;

namespace KMS2_02_LE_02_02_WPF.Manager
{
    /// <summary>
    /// Provides functionality to notify users about invalid input.
    /// </summary>
    public static class FailManager
    {
        // <summary>
        /// Displays a message box with the specified message to notify the user about invalid input.
        /// </summary>
        /// <param name="message">The message to be displayed in the message box.</param>
        public static void NotifyInvalidInput(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
