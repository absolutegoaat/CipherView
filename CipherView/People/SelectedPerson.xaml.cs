using Google.Protobuf.Compiler;
using MahApps.Metro.Controls;
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
using static CipherView.MySQLCommands;

namespace CipherView.People
{
    /// <summary>
    /// Interaction logic for SelectedPerson.xaml
    /// </summary>
    public partial class SelectedPerson : MetroWindow
    {
        public SelectedPerson(FetchedData person)
        {
            InitializeComponent();
            DataContext = person;
            this.Title = "CipherView - " + person.name;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            string RegularID = PersonID.Text; // grabs id so mysql knows where to edit
            string EditedName = PersonName.Text;
            string EditedEmail = PersonEmail.Text;
            string EditedPhone = PersonPhone.Text;
            string EditedAddress = PersonAddress.Text;
            string EditedLabel = PersonLabels.Text;
            string EditedSocials = PersonSocials.Text;
            string EditedDescription = PersonDesc.Text;

            MySQLCommands.EditPerson(RegularID, EditedName, EditedEmail, EditedPhone, EditedAddress, EditedLabel, EditedSocials, EditedDescription);
        }

        private void PersonID_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ID is read-only and cannot be edited.", "Information", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
