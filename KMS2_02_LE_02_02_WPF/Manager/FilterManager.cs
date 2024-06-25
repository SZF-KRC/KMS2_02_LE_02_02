using KMS2_02_LE_02_02_WPF.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace KMS2_02_LE_02_02_WPF.Manager
{
    /// <summary>
    /// Provides functionality to apply filters to a list of persons and update the DataGrid view.
    /// </summary>
    public class FilterManager
    {
        /// <summary>
        /// Applies the specified filter criterion to the list of persons and updates the DataGrid view with the filtered data.
        /// </summary>
        /// <param name="criterion">The filter criterion to apply.</param>
        /// <param name="persons">The list of persons to be filtered.</param>
        /// <param name="dataGridView">The DataGrid view to be updated with the filtered data.</param>
        public static void ApplyFilter(string criterion, List<Person> persons, DataGrid dataGridView)
        {
            if (persons == null)
                return;

            IEnumerable<Person> filteredData = persons;

            switch (criterion)
            {
                case "Select Filter": dataGridView.ItemsSource = persons; break;
                case "City (Descending)":filteredData = persons.OrderByDescending(p => p.City); break;
                case "Age (Ascending)": filteredData = persons.OrderBy(p => p.Age); break;
                case "Gender and Age (Descending)":filteredData = persons.OrderBy(p => p.Gender).ThenByDescending(p => p.Age); break;
                case "Name (Alphabetical)":filteredData = persons.OrderBy(p => p.Name); break;
                case "City with Age (35-55)":filteredData = persons.Where(p => p.Age >= 35 && p.Age <= 55).OrderBy(p => p.City);break;
            }
            dataGridView.ItemsSource = filteredData.ToList();
        }
    }
}
