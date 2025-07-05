using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SaveStateCreator
{
    public partial class MainWindow : Window
    {
        string? btd6SavePath;
        public MainWindow()
        {
            InitializeComponent();

            btd6SavePath = Saving.GetSavePath();
            if (!System.IO.Path.Exists(Saving.appdataPath))
                Directory.CreateDirectory(System.IO.Path.Combine(Saving.appdataPath, "OldSaves"));
            if (btd6SavePath is null)
            {
                MessageBox.Show("Could not find the BTD6 save file. Please ensure that BTD6 is installed via Steam and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }
        private void CreateSaveStateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Saving.CreateSaveState(btd6SavePath!);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSaveStateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Saving.LoadSaveState(btd6SavePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ReportIssuesButton_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/pepf0/BTD6SaveStateCreator/issues/new";
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show("Unable to open the Report Issues Page");
            }
        }

        private void OldSaveStatesButton_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = System.IO.Path.Combine(Saving.appdataPath, "OldSaves");
            try
            {
                Process.Start(new ProcessStartInfo("explorer.exe", folderPath) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show("Could not open old Save States folder.");
            }
        }

        private void HowToUseButton_Click(object sender, RoutedEventArgs e)
        {
            var url = "https://github.com/pepf0/BTD6SaveStateCreator/blob/master/README.md";
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch
            {
                MessageBox.Show("Unable to open the Guide Page");
            }
        }
    }
}