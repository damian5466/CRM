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

namespace CRM
{
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
            Closed += (a, b) =>
            {
                MainWindow.ClearAddWindow(false);
            };
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool haveNotes = true;
            if (NameField.Text == string.Empty || LastNameField.Text == string.Empty)
            {
                _ = MessageBox.Show("Imię oraz nazwisko muszą być uzupełnione!");
                return;
            }
            if(NoteField.Text == string.Empty)
            {
                haveNotes = false;
            }
            List<string> Notes = new();
            if (haveNotes)
            {
                Notes.Add(NoteField.Text);
            }
            Client temp = new()
            {
                Name = NameField.Text,
                LastName = LastNameField.Text,
                Email = EmailField.Text,
                PhoneNumber = PhoneField.Text,
                Notes = Notes,
                Department = DepBox.Text,
                ID = ClientManager.Clients.Count
            };
            ClientManager.Clients.Add(temp);
            ClientManager.SaveClients();
            MainWindow.Instance.CreateClient(temp);
            //MainWindow.ClearAddWindow(true);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ClearAddWindow(true);
        }
    }
}
