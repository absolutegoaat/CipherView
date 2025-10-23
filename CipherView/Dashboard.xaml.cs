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
using CipherView.People;
using MahApps.Metro.Controls;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard : MetroWindow
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

            PeopleGrid.LoadingRow += PeopleGrid_LoadingRow;

            try
            {
                var people = MySQLCommands.Fetcher();
                var limitedPeople = people?.Take(5).ToList();
                var users = MySQLCommands.FetchUsers();

                if (people != null)
                {
                    PeopleGrid.ItemsSource = people;
                    PeopleGrid2.ItemsSource = limitedPeople;
                    WindowStatus = "Data fetched successfully.";
                }

                if (users != null)
                {
                    UsersGrid.ItemsSource = users;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RefreshDataGrid()
        {
            
            PeopleGrid.ItemsSource = null;
            PeopleGrid2.ItemsSource = null;
            UsersGrid.ItemsSource = null;

            try
            {
                var people = MySQLCommands.Fetcher();
                var limitedPeople = people?.Take(5).ToList();
                var users = MySQLCommands.FetchUsers();

                if (people != null)
                {
                    PeopleGrid.ItemsSource = people;
                    PeopleGrid2.ItemsSource = limitedPeople;
                    WindowStatus = "Data refreshed successfully.";
                }

                if (users != null)
                {
                    UsersGrid.ItemsSource = users;
                }

                WindowStatus = "Data refreshed successfully.";
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error while refreshing: " + ex.Message);
            }
        }
        private void PeopleGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.ContextMenu != null)
            {
                foreach (var item in e.Row.ContextMenu.Items)
                {
                    if (item is MenuItem menuItem)
                    {
                        menuItem.Click -= MenuItem_ContextMenuClick; // Remove first to avoid duplicates
                        menuItem.Click += MenuItem_ContextMenuClick;
                    }
                }
            }
        }

        private void MenuItem_ContextMenuClick(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string? tag = menuItem.Tag?.ToString();

                if (tag == "Zenbar")
                {
                    ShowZenbar(sender, e);
                }
                else if (tag == "Delete")
                {
                    // Your delete code here
                }
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
            MessageBox.Show("CipherView v1.0\nDeveloped by absolutegoaat\n\nAn Administrative database viewer for CipherStorm.\n\nContributors:\nbrainrot02", "About CipherView", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void PeopleGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (PeopleGrid2.SelectedItem is FetchedData selected)
            {
                var detailsWindow = new SelectedPerson(selected);
                detailsWindow.ShowDialog();
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            var NewPersonWindow = new AddPerson();
            NewPersonWindow.ShowDialog();
        }

        private void Button_Refresh(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void Button_AddUser(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Will Be added later");
            // add later
        }

        private void ShowZenbar(object sender, RoutedEventArgs e)
        {
            if (PeopleGrid.SelectedItem is FetchedData selected)
            {
                if (Zenbar.Visibility == Visibility.Collapsed)
                {
                    ZENPlaceholder.Visibility = Visibility.Collapsed;
                    Zenbar.Visibility = Visibility.Visible;

                    ZenbarName.Text = selected.name;
                    ZenbarAddress.Text = selected.address;
                    ZenbarEmail.Text = selected.email;
                    ZenbarLabel.Text = selected.labels;
                    ZenbarPhone.Text = selected.phone;
                }
                else
                {
                    Zenbar.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
