using KMS2_02_LE_02_02_WPF.Model;
using MySql.Data.MySqlClient;

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
    }
}
