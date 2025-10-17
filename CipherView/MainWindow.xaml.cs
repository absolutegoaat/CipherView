using System.Windows;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace CipherView
{
    public partial class MainWindow
    {
        public static string? ConnectAddress { get; set; }
        public static string? Sqlpassword { get; set; }

        private bool _isDarkMode;

        public MainWindow()
        {
            InitializeComponent();
            _isDarkMode = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var buffer = new Buffer();
            buffer.Show();

            ConnectAddress = SQLAddress.Text;
            Sqlpassword = SQLPassword.Password;

            string connectionString = $"server={ConnectAddress};uid=root;pwd={Sqlpassword};database=cipherstorm";

            try
            {
                using var conn = new MySqlConnection(connectionString);
                conn.Open();

                var dashboard = new Dashboard();
                dashboard.Show();

                buffer.Close();
                Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_FAQ(object sender, RoutedEventArgs e)
        {
            const string message =
                "Q: Why isn't my CipherStorm Credentials working like web?\n" +
                "A: We're using MySQL Credentials not Cipherstorm Credentials on web. " +
                "Please ask your local admin in charge of CipherStorm Web to give you a password to the MySQL Server.\n\n";

            MessageBox.Show(message, "FAQ", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Settings(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Settings();
            settingsWindow.Show();
        }

        private void ToggleDarkMode_Click(object sender, RoutedEventArgs e)
        {
            _isDarkMode = !_isDarkMode;

            var bg = _isDarkMode ? new SolidColorBrush(Color.FromRgb(25, 25, 25)) : new SolidColorBrush(Color.FromRgb(240, 240, 240));
            var fg = _isDarkMode ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);

            Background = bg;
            Foreground = fg;

            foreach (var element in LogicalTreeHelper.GetChildren(this))
            {
                if (element is FrameworkElement fe)
                {
                    fe.Foreground = fg;
                    if (fe is Panel panel)
                        panel.Background = bg;
                }
            }
        }
    }
}
