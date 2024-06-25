using KMS2_02_LE_02_02_WPF.Model;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace KMS2_02_LE_02_02_WPF.SaveData
{
    /// <summary>
    /// Provides functionality to save filtered data from a DataGrid to a TXT file.
    /// </summary>
    public class SaveTXT
    {

        /// <summary>
        /// Saves the filtered data from the specified DataGrid to a TXT file at the specified file path.
        /// </summary>
        /// <param name="filePath">The path where the TXT file will be saved.</param>
        /// <param name="dataGridView">The DataGrid containing the data to be saved.</param>
        public static void SaveFilteredData(string filePath, DataGrid dataGridView)
        {
            var data = dataGridView.ItemsSource as List<Person>;
            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var person in data)
                    {
                        writer.WriteLine($"{person.Id}, {person.Name}, {person.Surname}, {person.Gender}, {person.Age}, {person.Zip}, {person.City}");
                    }
                }
            }
        }
    }
}
