using KMS2_02_LE_02_02_WPF.Model;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace KMS2_02_LE_02_02_WPF.Styles
{
    public static class DataGridPicture
    {
        /// <summary>
        /// Updates the image based on the selected person's gender.
        /// </summary>
        /// <param name="selectedPerson">The selected person from the DataGrid.</param>
        /// <param name="imageControl">The Image control to display the gender image.</param>
        public static void UpdateGenderImage(Person selectedPerson, Image imageControl)
        {
            if (selectedPerson == null || imageControl == null)
            {
                return;
            }

            string imageName = string.Empty;

            switch (selectedPerson.Gender)
            {
                case "M":
                    imageName = "M.png";
                    break;
                case "F":
                    imageName = "W.png";
                    break;
                case "D":
                    imageName = "D.png";
                    break;
                default:
                    imageControl.Source = null;
                    return;
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"pack://application:,,,/Styles/Images/{imageName}");
            bitmap.EndInit();
            imageControl.Source = bitmap;
        }
    }
}
