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
        public int CurrentClientID { get; }
        public bool EditMode { get; }
        public AddClient()
        {
            InitializeComponent();
            Closed += (a, b) =>
            {
                MainWindow.ClearAddWindow(false);
            };
            EditMode = false;
            RemoveButton.Visibility = EditMode ? Visibility.Visible : Visibility.Hidden;
        }

        public AddClient(Client cl)
        {
            InitializeComponent();
            Closed += (a, b) =>
            {
                MainWindow.ClearAddWindow(false);
            };
            CurrentClientID = cl.ID;
            InsertClientInfo(cl);
            EditMode = true;
            AddButton.Content = "Zapisz";
            Title = "Edytuj dane klienta";
            RemoveButton.Visibility = EditMode ? Visibility.Visible : Visibility.Hidden;
        }

        private void InsertClientInfo(Client cl)
        {
            NameField.Text = cl.Name;
            LastNameField.Text = cl.LastName;
            EmailField.Text = cl.Email;
            PhoneField.Text = cl.PhoneNumber;
            if (cl.Notes.Count > 0)
            {
                NoteField.Text = cl.Notes[0];
            }
            DepBox.Text = cl.Department;
            FirField.Text = cl.Company;
            NrField.Text = cl.AgreementNumber;
            TitleField.Text = cl.Title;
            TimeField.Text = cl.DevelopTime;
        }

        private void UpdateClientData(Client cl)
        {
            cl.Name = NameField.Text;
            cl.LastName = LastNameField.Text;
            cl.Email = EmailField.Text;
            cl.PhoneNumber = PhoneField.Text;
            if (NoteField.Text != string.Empty)
            {
                if (cl.Notes.Count > 0)
                {
                    cl.Notes[0] = NoteField.Text;
                }
                else
                {
                    cl.Notes.Add(NoteField.Text);
                }
            }
            cl.Department = DepBox.Text;
            cl.Company = FirField.Text;
            cl.AgreementNumber = NrField.Text;
            cl.Title = TitleField.Text;
            cl.DevelopTime = TimeField.Text;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckCriteria(out bool haveNotes))
            {
                return;
            }
            List<string> Notes = new();
            if (haveNotes)
            {
                Notes.Add(NoteField.Text);
            }
            if(!EditMode)
            {
                Client temp = new()
                {
                    ID = ClientManager.Clients.Count,
                    Name = NameField.Text,
                    LastName = LastNameField.Text,
                    Email = EmailField.Text,
                    PhoneNumber = PhoneField.Text,
                    Notes = Notes,
                    Department = DepBox.Text,
                    Company = FirField.Text,
                    AgreementNumber = NrField.Text,
                    Title = TitleField.Text,
                    DevelopTime = TimeField.Text
                };
                ClientManager.Clients.Add(temp);
                ClientManager.SaveClients();
                MainWindow.Instance.CreateClient(temp);
            }
            else
            {
                Client cl = null;
                foreach (Client client in ClientManager.Clients)
                {
                    if (client.ID == CurrentClientID)
                    {
                        cl = client;
                    }
                }
                UpdateClientData(cl);
                ClientManager.SaveClients();
                MainWindow.Instance.UpdateClient(cl);
            }
            //MainWindow.ClearAddWindow(true);
        }

        private bool CheckCriteria(out bool haveNotes)
        {
            haveNotes = true;
            if (NameField.Text == string.Empty || LastNameField.Text == string.Empty)
            {
                _ = MessageBox.Show("Imię oraz nazwisko muszą być uzupełnione!");
                return false;
            }
            if (NoteField.Text == string.Empty)
            {
                haveNotes = false;
            }
            return true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ClearAddWindow(true);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Client toRemove = null;
            foreach(Client cl in ClientManager.Clients)
            {
                if(cl.ID == CurrentClientID)
                {
                    MainWindow.Instance.RemoveClient(cl);
                    toRemove = cl;
                }
            }
            ClientManager.Clients.Remove(toRemove);
            ClientManager.SaveClients();
            MainWindow.ClearAddWindow(true);
        }
    }
}
