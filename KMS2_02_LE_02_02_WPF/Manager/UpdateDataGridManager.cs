﻿using KMS2_02_LE_02_02_WPF.Model;
using KMS2_02_LE_02_02_WPF.SaveData;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;

namespace KMS2_02_LE_02_02_WPF.Manager
{
    /// <summary>
    /// Provides functionality to update person data in the DataGrid and save changes to the appropriate data source.
    /// </summary>
    public static class UpdateDataGridManager
    {
        /// <summary>
        /// Updates the person data based on the cell edit in the DataGrid and saves the changes to the specified data source.
        /// </summary>
        /// <param name="e">The event arguments containing information about the cell edit.</param>
        /// <param name="persons">The list of persons displayed in the DataGrid.</param>
        /// <param name="dataSource">The data source type (SQL or XML).</param>
        /// <param name="xmlFilePath">The file path of the XML file if the data source is XML.</param>
        public static void UpdatePersonData(DataGridCellEditEndingEventArgs e, List<Person> persons, string dataSource, string xmlFilePath)
        {
            var column = e.Column as DataGridBoundColumn;
            if (column != null)
            {
                var bindingPath = (column.Binding as Binding)?.Path.Path;
                if (bindingPath != null)
                {
                    var editedPerson = e.Row.Item as Person;
                    var element = e.EditingElement as TextBox;
                    if (editedPerson != null && element != null)
                    {
                        string inputValue = element.Text.Trim();

                        if (!FailManager.ValidateInput(bindingPath, inputValue, editedPerson))
                        {
                            e.Cancel = true;
                            return;
                        }

                        switch (bindingPath)
                        {
                            case "Name":
                                editedPerson.Name = inputValue;
                                break;
                            case "Surname":
                                editedPerson.Surname = inputValue;
                                break;
                            case "Gender":
                                editedPerson.Gender = inputValue;
                                break;
                            case "Age":
                                editedPerson.Age = int.Parse(inputValue);
                                break;
                            case "Zip":
                                editedPerson.Zip = int.Parse(inputValue);
                                break;
                            case "City":
                                editedPerson.City = inputValue;
                                break;
                        }

                        if (dataSource == "SQL")
                        {
                            SaveSQL.SaveDataToDatabase(editedPerson);
                        }
                        else if (dataSource == "XML")
                        {
                            SaveXML.SaveDataToXML(persons, xmlFilePath);
                        }
                    }
                }
            }
        }

        public static void DeletePersonData(Person personToDelete, List<Person> persons, string dataSource, string xmlFilePath)
        {
            if (personToDelete != null)
            {
                persons.Remove(personToDelete);

                if (dataSource == "SQL")
                {
                    SaveSQL.DeleteDataFromDatabase(personToDelete.Id);
                }
                else if (dataSource == "XML")
                {
                    SaveXML.DeleteDataFromXML(persons, personToDelete.Id, xmlFilePath);
                }
            }
        }

    }
}