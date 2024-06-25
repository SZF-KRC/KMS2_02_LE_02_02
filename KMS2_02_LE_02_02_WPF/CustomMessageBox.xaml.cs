using KMS2_02_LE_02_02_WPF.Styles;
using System.Windows;

namespace KMS2_02_LE_02_02_WPF
{
    public partial class CustomMessageBox : Window
    {
  


        public DataSourceOption Result { get; private set; }

        public CustomMessageBox(string message)
        {
            InitializeComponent();
            MessageText.Text = message;
        }

        private void SqlButton_Click(object sender, RoutedEventArgs e)
        {
            Result = DataSourceOption.SQLData;
            DialogResult = true;
        }

        private void XmlButton_Click(object sender, RoutedEventArgs e)
        {
            Result = DataSourceOption.XMLData;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Result = DataSourceOption.Cancel;
            DialogResult = false;
        }


        public static DataSourceOption Show(string message)
        {
            CustomMessageBox msgBox = new CustomMessageBox(message);
            msgBox.ShowDialog();
            return msgBox.Result;
        }
    }
}
