using KMS2_02_LE_02_02_WPF.Manager;
using KMS2_02_LE_02_02_WPF.Model;
using KMS2_02_LE_02_02_WPF.SaveData;
using KMS2_02_LE_02_02_WPF.Styles;
using KMS2_02_LE_02_02_WPF.UploadData;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KMS2_02_LE_02_02_WPF.Views
{
    public partial class MainWindow : Window
    {
        private List<Person> persons;
        private string dataSource;
        private string xmlFilePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            dataGridView.CellEditEnding += DataGridView_CellEditEnding;
        }

        /// <summary>
        /// Handles the Click event of the LoadDataButton control.
        /// Loads data from the selected data source (SQL or XML).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LoadDataButton_Click(object sender, RoutedEventArgs e)
        {
            ClearImage();
            DataSourceOption result = CustomMessageBox.Show("Choose Data Source:");

            if (result == DataSourceOption.SQLData)
            {             
                persons = UploadSQL.LoadDataFromDatabase();
                dataSource = "SQL";
            }
            else if (result == DataSourceOption.XMLData)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                    xmlFilePath = openFileDialog.FileName;
                    persons = UploadXML.LoadDataFromXML(xmlFilePath);
                    dataSource = "XML";
                }
            }

            if (persons != null)
            {
                dataGridView.ItemsSource = persons;
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveFilteredDataButton control.
        /// Saves the filtered data to a file (TXT or DOCX).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveFilteredDataButton_Click(object sender, RoutedEventArgs e)
        {
            ClearImage();
            if (persons != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text file (*.txt)|*.txt|Word document (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == true)
                {
                    string fileExtension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();
                    if (fileExtension == ".txt")
                    {
                        SaveTXT.SaveFilteredData(saveFileDialog.FileName, dataGridView);
                    }
                    else if (fileExtension == ".docx")
                    {
                        SaveDOCX.SaveFilteredData(saveFileDialog.FileName, dataGridView);
                    }
                }
            }
            
        }

        private void ClearImage() { if(genderImage != null) { genderImage.Source = null; } }

        /// <summary>
        /// Handles the SelectionChanged event of the filterComboBox.
        /// Applies the selected filter to the data grid.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearImage();
            if (filterComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string filter = selectedItem.Content.ToString();
                FilterManager.ApplyFilter(filter, persons, dataGridView);
            }
        }


        /// <summary>
        /// Handles the CellEditEnding event of the dataGridView control.
        /// Updates the person data in the data source when a cell edit is committed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridCellEditEndingEventArgs"/> instance containing the event data.</param>
        private void DataGridView_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                UpdateDataGridManager.UpdatePersonData(e, persons, dataSource, xmlFilePath);
            }

        }

        private void DataGridView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGridView.SelectedItem is Person personToDelete)
                {
                    // Assuming you have a data source type and XML file path
                    string dataSource = "SQL"; // or "XML"
                    string xmlFilePath = "path_to_your_file.xml";

                    // Delete the selected person
                    UpdateDataGridManager.DeletePersonData(personToDelete, (List<Person>)dataGridView.ItemsSource, dataSource, xmlFilePath);
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dataGridView control.
        /// Updates the image based on the selected person's gender.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void DataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridView.SelectedItem is Person selectedPerson)
            {
                DataGridPicture.UpdateGenderImage(selectedPerson, genderImage);
            }
        }
    }
}
