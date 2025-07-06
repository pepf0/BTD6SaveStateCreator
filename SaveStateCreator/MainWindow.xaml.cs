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
        public static string currentSaveName = "Profile.Save";
        public MainWindow()
        {
            InitializeComponent();

            btd6SavePath = Saving.GetSavePath();
            if (!System.IO.Path.Exists(Saving.appdataPath))
                Directory.CreateDirectory(System.IO.Path.Combine(Saving.appdataPath));
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
                SaveNameQueryWindow saveNameQueryWindow = new SaveNameQueryWindow();
                saveNameQueryWindow.ShowDialog();
                Saving.CreateSaveState(btd6SavePath!, currentSaveName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSaveStateButton_Click(object sender, RoutedEventArgs e)
        {
            ShowMainGUI(false);
            //try
            //{
            //    Saving.LoadSaveState(btd6SavePath);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
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

        private void EditSaveStatesButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void ShowMainGUI(bool show)
        {
            if (show)
            {
                ButtonCreateSaveState.Visibility = Visibility.Visible;
                ButtonLoadSaveState.Visibility = Visibility.Visible;
                ButtonReportIssues.Visibility = Visibility.Visible;
                ButtonEditSaveStates.Visibility = Visibility.Visible;
                ButtonHowToUse.Visibility = Visibility.Visible;
                LabelMadeBy.Visibility = Visibility.Visible;
                LabelTitle.Visibility = Visibility.Visible;
            }
            else
            {
                ButtonCreateSaveState.Visibility = Visibility.Collapsed;
                ButtonLoadSaveState.Visibility = Visibility.Collapsed;
                ButtonReportIssues.Visibility = Visibility.Collapsed;
                ButtonEditSaveStates.Visibility = Visibility.Collapsed;
                ButtonHowToUse.Visibility = Visibility.Collapsed;
                LabelMadeBy.Visibility = Visibility.Collapsed;
                LabelTitle.Visibility = Visibility.Collapsed;
            }
        }
    }
}