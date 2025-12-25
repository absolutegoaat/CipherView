using CipherView.People;
using ControlzEx;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static CipherView.MySQLCommands;

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

        // context menu
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            ContextMenu contextMenu = new();

            // xaml wants to fuck me over so we're going with this

            //zenbar
            MenuItem zenbarItem = new() { Header = "Open in Zenbar" };
            zenbarItem.Click += MenuItem_Zenbar;
            zenbarItem.Icon = new MahApps.Metro.IconPacks.PackIconMaterial
            {
                Kind = PackIconMaterialKind.GlassCocktail,
                Width = 16,
                Height = 16,
                Margin = new Thickness(10,0,10,0),
            };

            //delete button
            MenuItem deleteItem = new() { Header = "Delete" };
            deleteItem.Click += Button_Deleteperson;
            deleteItem.Icon = new MahApps.Metro.IconPacks.PackIconMaterial
            {
                Kind = PackIconMaterialKind.DeleteCircle,
                Width = 16,
                Height = 16,
                Margin = new Thickness(10,0,10,0)
            };

            // must add a new button once a new button has been made or else its not there
            contextMenu.Items.Add(zenbarItem);
            contextMenu.Items.Add(deleteItem);

            e.Row.ContextMenu = contextMenu;
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
            MessageBox.Show("CipherView v1.1 UNSTABLE (DEVELOPER BRANCH)\nDeveloped by absolutegoaat\n\nAn Administrative database viewer for CipherStorm.\n\nContributors:\nbrainrot02", "About CipherView", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void MenuItem_Zenbar(object sender, RoutedEventArgs e)
        {
            if (PeopleGrid.SelectedItem is FetchedData selected)
            {
                Zenbar.DataContext = selected;
                Zenbar.IsOpen = true;
            }
        }

        private void Button_Deleteperson(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete function will be added later");
        }
    }
}
