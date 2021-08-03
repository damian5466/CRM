using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace CRM
{
    public static class ClientManager
    {
        public static List<Client> Clients { get; set; } = new List<Client>();
        public static string AppDirPath { get; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\CRM";
        public static string FilePath { get; } = AppDirPath + @"\clients.json";

        public static void SaveClients()
        {
            if (!Directory.Exists(AppDirPath))
            {
                _ = Directory.CreateDirectory(AppDirPath);
            }
            string result = JsonSerializer.Serialize(Clients, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            File.WriteAllText(FilePath, result);
        }

        public static void LoadClients()
        {
            if (!File.Exists(FilePath))
            {
                return;
            }
            Clients = JsonSerializer.Deserialize<List<Client>>(File.ReadAllText(FilePath));
        }
    }
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Department { get; set; }
        public List<string> Notes { get; set; }
    }
}
