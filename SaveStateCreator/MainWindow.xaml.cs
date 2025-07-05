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
        }

        private void CreateSaveStateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadSaveStateButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}