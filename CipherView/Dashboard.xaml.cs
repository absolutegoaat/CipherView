using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace CipherView
{
    public partial class Dashboard : MetroWindow
    {
        public string? ConnectedIpAddress { get; set; }
        public string? WindowStatus { get; set; }
        public string? ColorBar { get; set; }

        public Dashboard()
        {
            InitializeComponent();

            ColorBar = "Blue";
            ConnectedIpAddress = MainWindow.ConnectAddress;
            WindowStatus = "MySQL Connection was successful.";
            DataContext = this;

            try
            {
                var people = FetchData.Fetcher();

                if (people != null)
                {
                    PeopleGrid.ItemsSource = people;
                    WindowStatus = "Data fetched successfully.";
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            ColorBar = "Green";

            var currentTheme = ThemeManager.Current.DetectTheme(this);
            if (currentTheme?.Name.Contains("Dark") == true)
                DarkModeToggle.IsChecked = true;
        }

        private void DarkModeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Dark.Blue");
            WindowStatus = "Dark Mode Enabled";
        }

        private void DarkModeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeTheme(Application.Current, "Light.Blue");
            WindowStatus = "Light Mode Enabled";
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            WindowStatus = "User Loaded Settings";
            var settingsWindow = new Settings();
            settingsWindow.Show();
        }

        private void Button_SignOut(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void PeopleGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PeopleGrid.SelectedItem is FetchedData selected)
            {
                var detailsWindow = new SelectedPerson(selected);
                detailsWindow.ShowDialog();
            }
        }

        private void Button_About(object sender, RoutedEventArgs e)
        {
            const string url = "https://github.com/absolutegoaat";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open URL: {ex.Message}");
            }
        }
    }
}
