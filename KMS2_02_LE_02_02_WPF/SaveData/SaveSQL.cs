using KMS2_02_LE_02_02_WPF.Model;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace KMS2_02_LE_02_02_WPF.SaveData
{
    /// <summary>
    /// Provides functionality to save person data to a SQL database.
    /// </summary>
    public class SaveSQL
    {
        /// <summary>
        /// The connection string used to connect to the SQL database.
        /// </summary>
        private static string connectionString = "Server=localhost;Database=person_manager;Uid=root;Pwd=Krc6130;";

        /// <summary>
        /// Saves the specified person data to the SQL database.
        /// </summary>
        /// <param name="person">The person object containing the data to be saved.</param>
        public static void SaveDataToDatabase(Person person)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE person SET Name = @Name, Surname = @Surname, Gender = @Gender, Age = @Age, Zip = @Zip, City = @City WHERE ID = @Id", connection);
                    cmd.Parameters.AddWithValue("@ID", person.Id);
                    cmd.Parameters.AddWithValue("@Name", person.Name);
                    cmd.Parameters.AddWithValue("@Surname", person.Surname);
                    cmd.Parameters.AddWithValue("@Gender", person.Gender);
                    cmd.Parameters.AddWithValue("@Age", person.Age);
                    cmd.Parameters.AddWithValue("@Zip", person.Zip);
                    cmd.Parameters.AddWithValue("@City", person.City);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex) { MessageBox.Show($"MySQL Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);}
            catch (InvalidOperationException ex) { MessageBox.Show($"Invalid Operation: {ex.Message}", "Operation Error", MessageBoxButton.OK, MessageBoxImage.Warning);}
            catch (TimeoutException ex){ MessageBox.Show($"Timeout: {ex.Message}", "Timeout Error", MessageBoxButton.OK, MessageBoxImage.Warning);}
            catch (Exception ex) {MessageBox.Show($"General Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);}
        }

        public static void DeleteDataFromDatabase(int personId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM person WHERE ID = @Id", connection);
                    cmd.Parameters.AddWithValue("@ID", personId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex) { MessageBox.Show($"MySQL Error: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (InvalidOperationException ex) { MessageBox.Show($"Invalid Operation: {ex.Message}", "Operation Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
            catch (TimeoutException ex) { MessageBox.Show($"Timeout: {ex.Message}", "Timeout Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
            catch (Exception ex) { MessageBox.Show($"General Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

    }
}
