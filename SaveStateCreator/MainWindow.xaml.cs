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
        const string LABEL_TITLE_CONTENT = "BTD6 Save State Creator V2.0";
        public MainWindow()
        {
            InitializeComponent();

            LabelTitle.Content = LABEL_TITLE_CONTENT;
            ShowMainGUI(true);
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
            StackPanelLoadSaves.Children.Clear();
            StackPanelLoadSaves.Visibility = Visibility.Visible;
            ScrollViewerLoadSaves.Visibility = Visibility.Visible;
            LabelTitle.Visibility = Visibility.Visible;
            LabelTitle.Content = "Select a Save State to Load:";
            if (!Directory.Exists(Saving.appdataPath))
                return;

            foreach (string file in Directory.GetFiles(Saving.appdataPath))
            {
                string fileName = System.IO.Path.GetFileName(file);

                Button fileButton = new Button
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Height = 50,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(0x1f, 0x31, 0x3d)),
                    Tag = file
                };

                fileButton.Click += ButtonSelectFile_Click;

                Viewbox viewbox = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    StretchDirection = StretchDirection.Both
                };

                Label label = new Label
                {
                    Content = System.IO.Path.GetFileNameWithoutExtension(fileName),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(0xad, 0xfe, 0xf6)),
                    Padding = new Thickness(0)
                };

                viewbox.Child = label;
                fileButton.Content = viewbox;

                StackPanelLoadSaves.Children.Add(fileButton);
            }
        }

        private void ButtonSelectFile_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                string filePath = b.Tag as string;
                if (filePath != null && File.Exists(filePath))
                {
                    try
                    {
                        Saving.LoadSaveState(btd6SavePath!, System.IO.Path.GetFileName(filePath));
                        ShowMainGUI(true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while loading the save state: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid save state file selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            LabelTitle.Content = LABEL_TITLE_CONTENT;
            StackPanelLoadSaves.Children.Clear();
            StackPanelLoadSaves.Visibility = Visibility.Collapsed;
            ScrollViewerLoadSaves.Visibility = Visibility.Collapsed;
        }
        private void EditSaveStatesButton_Click(object sender, RoutedEventArgs e)
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
                StackPanelLoadSaves.Visibility = Visibility.Collapsed;
                StackPanelEditSaves.Visibility = Visibility.Collapsed;
                ScrollViewerEditSaves.Visibility = Visibility.Collapsed;
                ScrollViewerLoadSaves.Visibility = Visibility.Collapsed;
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