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
using MySql.Data.MySqlClient;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Dashboard
    {
        public string? ConnectedIpAddress { get; set; }

        public Dashboard()
        {
            InitializeComponent();

            ConnectedIpAddress = MainWindow.ConnectAddress;
            DataContext = this;
        }
    }
}
