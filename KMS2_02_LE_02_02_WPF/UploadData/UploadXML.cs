﻿using KMS2_02_LE_02_02_WPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace KMS2_02_LE_02_02_WPF.UploadData
{
    /// <summary>
    /// Provides functionality to upload person data from an XML file.
    /// </summary>
    public class UploadXML
    {
        /// <summary>
        /// Loads the person data from the specified XML file and returns it as a list of <see cref="Person"/> objects.
        /// </summary>
        /// <param name="filePath">The path of the XML file to load the data from.</param>
        /// <returns>A list of <see cref="Person"/> objects containing the data from the XML file.</returns>
        public static List<Person> LoadDataFromXML(string filePath)
        {
            try
            {
                List<Person> persons;
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    persons = (List<Person>)serializer.Deserialize(fs);
                }
                return persons;
            }
            catch (InvalidOperationException e) { MessageBox.Show($"Data Error: {e.Message}", "Invalid Operation Exception", MessageBoxButton.OK, MessageBoxImage.Error); ; }
            catch (Exception ex) { MessageBox.Show($"Data Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            return null;
        }
    }
}
