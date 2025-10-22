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
using MahApps.Metro.Controls;

namespace CipherView
{
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : MetroWindow
    {
        public AddPerson()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string NewName = PersonName.Text;
            string NewEmail = PersonEmail.Text;
            string NewPhone = PersonPhone.Text;
            string NewAddress = PersonAddress.Text;
            string NewLabel = PersonLabels.Text;
            string NewSocials = PersonSocials.Text;
            string NewDescription = PersonDesc.Text;

            MySQLCommands.AddPerson(NewName, NewEmail, NewPhone, NewAddress, NewLabel, NewSocials, NewDescription);
            this.Close();
        }
    }
}
