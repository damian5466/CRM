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
        public List<MyLabel> ClientLabels { get; } = new();

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

        public void ShowClients()
        {
            foreach (Client cl in ClientManager.Clients)
            {
                CreateClient(cl);
            }
        }

        public void UpdateClient(Client cl)
        {
            foreach(MyLabel label in ClientLabels)
            {
                if(label.Client.ID != cl.ID)
                {
                    continue;
                }
                switch(label.TableIndex)
                {
                    case 0:
                        label.Content = cl.Name;
                        break;
                    case 1:
                        label.Content = cl.LastName;
                        break;
                    case 2:
                        label.Content = cl.Email;
                        break;
                    case 3:
                        label.Content = cl.PhoneNumber;
                        break;
                    case 4:
                        label.Content = cl.Department;
                        break;
                    case 5:
                        label.Content = cl.Company;
                        break;
                    case 6:
                        label.Content = cl.AgreementNumber;
                        break;
                    case 7:
                        label.Content = cl.Title;
                        break;
                    case 8:
                        label.Content = cl.DevelopTime;
                        break;
                    default:
                        break;
                }
            }
        }

        public void CreateClient(Client cl)
        {
            CreateLabel(cl, cl.Name, 0);
            CreateLabel(cl, cl.LastName, 1);
            CreateLabel(cl, cl.Email, 2);
            CreateLabel(cl, cl.PhoneNumber, 3);
            CreateLabel(cl, cl.Department, 4);
            CreateLabel(cl, cl.Company, 5);
            CreateLabel(cl, cl.AgreementNumber, 6);
            CreateLabel(cl, cl.Title, 7);
            CreateLabel(cl, cl.DevelopTime, 8);
        }

        public void RemoveClient(Client cl)
        {
            List<MyLabel> toRemove = new();
            foreach(MyLabel label in ClientLabels)
            {
                if (label.Client.ID != cl.ID)
                {
                    continue;
                }
                StackPanel parent = (StackPanel)label.Parent;
                parent.Children.Remove(label);
                toRemove.Add(label);
            }
            foreach(MyLabel label in toRemove)
            {
                ClientLabels.Remove(label);
            }
        }

        private void CreateLabel(Client cl, string content, int tableIndex)
        {
            MyLabel xd = new()
            {
                Client = cl,
                TableIndex = tableIndex
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
                case 5:
                    _ = FirPanel.Children.Add(xd);
                    break;
                case 6:
                    _ = ConPanel.Children.Add(xd);
                    break;
                case 7:
                    _ = TitlePanel.Children.Add(xd);
                    break;
                case 8:
                    _ = TimePanel.Children.Add(xd);
                    break;
                default:
                    return;
            }
            ClientLabels.Add(xd);
        }

        private void InformationClick(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("Autor: damian5466\n\nKod Źródłowy:\nhttps://github.com/damian5466/CRM");
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
            ClearAddWindow(true);
            MyLabel label = (MyLabel)sender;
            AddWindow = new AddClient(label.Client);
            AddWindow.Show();
        }
    }

    public class MyLabel : Label
    {
        public Client Client { get; set; }
        public int TableIndex { get; set; }
    }
}
