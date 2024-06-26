using KMS2_02_LE_02_02_WPF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace KMS2_02_LE_02_02_WPF.SaveData
{
    /// <summary>
    /// Provides functionality to save person data to an XML file.
    /// </summary>
    public class SaveXML
    {
        /// <summary>
        /// Saves the specified list of persons to an XML file at the specified file path.
        /// </summary>
        /// <param name="persons">The list of persons to be saved.</param>
        /// <param name="xmlFilePath">The file path where the XML file will be created.</param>
        public static void SaveDataToXML(List<Person> persons, string xmlFilePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, persons);
                }
            }
            catch (IOException ex){MessageBox.Show($"I/O Error: {ex.Message}", "File Error", MessageBoxButton.OK, MessageBoxImage.Error);}
            catch (UnauthorizedAccessException ex){MessageBox.Show($"Access Denied: {ex.Message}", "Permission Error", MessageBoxButton.OK, MessageBoxImage.Error);}
            catch (InvalidOperationException ex){MessageBox.Show($"Serialization Error: {ex.Message}", "Serialization Error", MessageBoxButton.OK, MessageBoxImage.Error);}
            catch (Exception ex){MessageBox.Show($"General Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
