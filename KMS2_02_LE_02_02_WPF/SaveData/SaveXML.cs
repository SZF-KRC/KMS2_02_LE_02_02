using KMS2_02_LE_02_02_WPF.Model;
using System.Collections.Generic;
using System.IO;
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
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
            {
                serializer.Serialize(fs, persons);
            }
        }
    }
}
