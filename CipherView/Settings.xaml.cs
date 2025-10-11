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
using MahApps.Metro.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro;
using ControlzEx.Theming;

namespace CipherView
{
    public partial class Settings : MetroWindow
    {
        private bool _isInitializing = true;

        public Settings()
        {
            InitializeComponent();

            Loaded += Settings_Loaded;
        }
        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            _isInitializing = true;

            var theme = ThemeManager.Current.DetectTheme(Application.Current);
            if (theme != null)
            {
                DarkModeToggle.IsOn = theme.BaseColorScheme == "Dark";
            }

            _isInitializing = false;
        }

        private void DarkMode(object sender, RoutedEventArgs e)
        {
            if (_isInitializing) return;

            var theme = ThemeManager.Current.DetectTheme(Application.Current);
            if (theme != null)
            {
                var newBaseTheme = theme.BaseColorScheme == "Dark" ? "Light" : "Dark";
                ThemeManager.Current.ChangeTheme(Application.Current, newBaseTheme, theme.ColorScheme);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
