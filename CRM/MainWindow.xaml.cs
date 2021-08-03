using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public static AddClient AddWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Closed += (a, e) =>
            {
                ClearAddWindow(true);
            };
            Instance = this;
            ClientManager.LoadClients();
            ShowClients();
        }

        private void ShowClients()
        {
            foreach (Client cl in ClientManager.Clients)
            {
                CreateClient(cl);
            }
        }

        public void CreateClient(Client cl)
        {
            CreateLabel(cl.ID, cl.Name, 0);
            CreateLabel(cl.ID, cl.LastName, 1);
            CreateLabel(cl.ID, cl.Email, 2);
            CreateLabel(cl.ID, cl.PhoneNumber, 3);
            CreateLabel(cl.ID, cl.Department, 4);
        }

        private void CreateLabel(int id, string content, int tableIndex)
        {
            MyLabel xd = new()
            {
                ID = id
            };
            xd.Content = content;
            xd.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            xd.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
            xd.HorizontalContentAlignment = HorizontalAlignment.Center;
            xd.MouseRightButtonDown += RightClickLabel;
            xd.MouseDoubleClick += DoubleClickLabel;
            double marg = 0;
            marg = xd.Margin.Top + 5;
            Thickness temp = new()
            {
                Left = xd.Margin.Left,
                Top = marg,
                Right = xd.Margin.Right,
                Bottom = xd.Margin.Bottom
            };
            xd.Margin = temp;
            switch (tableIndex)
            {
                case 0:
                    _ = NamePanel.Children.Add(xd);
                    break;
                case 1:
                    _ = LastNamePanel.Children.Add(xd);
                    break;
                case 2:
                    _ = EmailPanel.Children.Add(xd);
                    break;
                case 3:
                    _ = PhonePanel.Children.Add(xd);
                    break;
                case 4:
                    _ = DepPanel.Children.Add(xd);
                    break;
                default:
                    return;
            }
        }

        private void InformationClick(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("Autor: Damian");
        }

        private void AddClientClick(object sender, RoutedEventArgs e)
        {
            ClearAddWindow(true);
            AddWindow = new AddClient();
            AddWindow.Show();
        }

        public static void ClearAddWindow(bool toClose)
        {
            if (AddWindow != null)
            {
                if(toClose)
                {
                    AddWindow.Close();
                }
                AddWindow = null;
            }
        }

        private void TableRow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = MessageBox.Show("Test");
        }

        private void RightClickLabel(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(((MyLabel)sender).Content.ToString());
        }

        private void DoubleClickLabel(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;
            _ = MessageBox.Show("Double clicked!");
        }
    }

    public class MyLabel : Label
    {
        public int ID { get; set; }
    }
}
