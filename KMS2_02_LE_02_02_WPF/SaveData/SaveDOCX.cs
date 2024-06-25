using KMS2_02_LE_02_02_WPF.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace KMS2_02_LE_02_02_WPF.SaveData
{
    /// <summary>
    /// Provides functionality to save filtered data from a DataGrid to a DOCX file.
    /// </summary>
    public class SaveDOCX
    {
        /// <summary>
        /// Saves the filtered data from the specified DataGrid to a DOCX file at the specified file path.
        /// </summary>
        /// <param name="filePath">The path where the DOCX file will be saved.</param>
        /// <param name="dataGridView">The DataGrid containing the data to be saved.</param>
        public static void SaveFilteredData(string filePath, DataGrid dataGridView)
        {
            var data = dataGridView.ItemsSource as List<Person>;
            if (data != null)
            {
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    Body body = new Body();

                    // Create the table
                    Table table = new Table();

                    // Add the table header
                    TableRow headerRow = new TableRow();
                    headerRow.Append(
                        new TableCell(new Paragraph(new Run(new Text("ID")))),
                        new TableCell(new Paragraph(new Run(new Text("Name")))),
                        new TableCell(new Paragraph(new Run(new Text("Surname")))),
                        new TableCell(new Paragraph(new Run(new Text("Gender")))),
                        new TableCell(new Paragraph(new Run(new Text("Age")))),
                        new TableCell(new Paragraph(new Run(new Text("Zip")))),
                        new TableCell(new Paragraph(new Run(new Text("City"))))
                    );
                    table.Append(headerRow);

                    // Add data rows to the table
                    foreach (var person in data)
                    {
                        TableRow row = new TableRow();
                        row.Append(
                            new TableCell(new Paragraph(new Run(new Text(person.Id.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(person.Name)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Surname)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Gender)))),
                            new TableCell(new Paragraph(new Run(new Text(person.Age.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(person.Zip.ToString())))),
                            new TableCell(new Paragraph(new Run(new Text(person.City))))
                        );
                        table.Append(row);
                    }

                    body.Append(table);
                    mainPart.Document.Append(body);
                    mainPart.Document.Save();
                }
            }
        }
    }
}
