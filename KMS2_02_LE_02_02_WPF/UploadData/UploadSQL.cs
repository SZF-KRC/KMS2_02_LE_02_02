using KMS2_02_LE_02_02_WPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;

namespace KMS2_02_LE_02_02_WPF.UploadData
{
    /// <summary>
    /// Provides functionality to upload person data from a SQL database.
    /// </summary>
    public class UploadSQL
    {
        /// <summary>
        /// The connection string used to connect to the SQL database.
        /// </summary>
        private static string connectionString = "Server=localhost;Database=person_manager;Uid=root;Pwd=Krc6130;";

        /// <summary>
        /// A list to store the person data retrieved from the SQL database.
        /// </summary>
        private static List<Person> sqlData = new List<Person>();

        /// <summary>
        /// Loads the person data from the SQL database and returns it as a list of <see cref="Person"/> objects.
        /// </summary>
        /// <returns>A list of <see cref="Person"/> objects containing the data from the SQL database.</returns>
        public static List<Person> LoadDataFromDatabase()
        {
            try
            {
                sqlData.Clear();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlDataAdapter adapterSQL = new MySqlDataAdapter("SELECT * FROM person", connection);
                    DataTable data = new DataTable();
                    adapterSQL.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        Person person = new Person
                        {
                            Id = Convert.ToInt32(row["ID"]),
                            Name = row["Name"].ToString(),
                            Surname = row["Surname"].ToString(),
                            Gender = row["Gender"].ToString(),
                            Age = Convert.ToInt32(row["Age"]),
                            Zip = Convert.ToInt32(row["Zip"]),
                            City = row["City"].ToString()
                        };
                        sqlData.Add(person);
                    }

                    return sqlData;
                }
            }
            catch (DataException ex) { MessageBox.Show($"Data Error: {ex.Message}", "Data Exception", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (TimeoutException ex) { MessageBox.Show($"SQL Error: {ex.Message}", "Timeout Exception", MessageBoxButton.OK, MessageBoxImage.Warning); }
            catch (InvalidOperationException ex) { MessageBox.Show($"SQL Error: {ex.Message}", "Invalid Operation Exception", MessageBoxButton.OK, MessageBoxImage.Warning); }
            catch (MySqlException ex) { MessageBox.Show($"SQL Error: {ex.Message}", "SQL Exception", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (Exception ex) { MessageBox.Show($"Data Error: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error); }
            return null;
        }
    }
}
