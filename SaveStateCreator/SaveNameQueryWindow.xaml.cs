using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaveStateCreator
{
    /// <summary>
    /// Interaction logic for SaveNameQueryWindow.xaml
    /// </summary>
    public partial class SaveNameQueryWindow : Window
    {
        public SaveNameQueryWindow()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxName.Text.Trim().Any(c => System.IO.Path.GetInvalidFileNameChars().Contains(c)) || reservedNames.Any(c => c == TextBoxName.Text.Trim().ToUpper()))
            {
                MessageBox.Show("The save name contains invalid characters. Please use a different name.", "Invalid Name", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainWindow.currentSaveName = TextBoxName.Text.Trim() + ".Save";
            Close();
        }

        string[] reservedNames = { "CON", "PRN", "AUX", "NUL",
        "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
        "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"};
    }
}
