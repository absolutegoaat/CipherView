using MySql.Data.MySqlClient;
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
using static CipherView.FetchData;
using System.Diagnostics;
using ControlzEx;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard
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
    }
}
