using ControlzEx;
using ControlzEx.Theming;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static CipherView.MySQLCommands;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard
    {
        public string? ConnectedIpAddress { get; set; }
        public string? WindowStatus { get; set; }
        public string LoggedInUser { get; set; } = $"Hello, {Environment.UserName}";

        public Dashboard()
        {
            InitializeComponent();
            ConnectedIpAddress = MainWindow.ConnectAddress;
            WindowStatus = "MySQL Connection was successful.";
            DataContext = this;

            try
            {
                var people = MySQLCommands.Fetcher();

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
            var url = "https://github.com/absolutegoaat";
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

        private void Button_About2(object sender, RoutedEventArgs e)
        {
            // we can change this later
            MessageBox.Show("CipherView v1.0\nDeveloped by absolutegoaat\n\nA simple database viewer for CipherStorm.\n\nContributors:\nbrainrot02", "About CipherView", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
