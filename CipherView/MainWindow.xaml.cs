using System.Net;
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
using MySql.Data.MySqlClient;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static string? ConnectAddress { get; set; }

        public static string? Sqlpassword { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new Buffer();
            buffer.Show();

            ConnectAddress = SQLAddress.Text;
            Sqlpassword = SQLPassword.Password;

            MySql.Data.MySqlClient.MySqlConnection conn = new();
            string ConnectionString;

            ConnectionString = $"server={ConnectAddress};uid=root;" + $"pwd={Sqlpassword};database=cipherstorm";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();

                var dashboard = new Dashboard();
                dashboard.Show();

                buffer.Close();
                this.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_FAQ(object sender, RoutedEventArgs e)
        {
            const string message = "Q: Why isn't my CipherStorm Credentials working like web?\n" +
                                   "A: We're using MySQL Credentials not Cipherstorm Credentials on web. Please ask your local admin in charge of CipherStorm Web to give you a password to the MySQL Server.\n\n";
            MessageBox.Show(message, "FAQ", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Settings();
            settingsWindow.Show();
        }
    }
}