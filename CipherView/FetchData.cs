using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
#pragma warning disable IDE1006 // filling up my error list so annoying

namespace CipherView
{
    public class FetchData
    {
        public class FetchedData
        {
            public int Id { get; set; }
            public string? name { get; set; }
            public string? address { get; set; }
            public string? phone { get; set; }
            public string? email { get; set; }
            public string? ipaddress { get; set; }
            public string? labels { get; set; }
            public string? description { get; set; }
            public int convicted { get; set; }
            public string? socials { get; set; }
            public string ConvictedDisplay => convicted == 1 ? "Yes" : "No";
        }
        public static List<FetchedData>? Fetcher()
        {
            List<FetchedData> dataList = new();
            string? connectAddress = MainWindow.ConnectAddress;
            string? password = MainWindow.Sqlpassword;

            MySqlConnection conn = new();

            string ConnectionString;
            ConnectionString = $"server={connectAddress};uid=root;" + $"pwd={password};database=cipherstorm";

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();
                string query = "SELECT * FROM people";
                MySqlCommand cmd = new(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FetchedData data = new()
                    {
                        Id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        address = reader.GetString("address"),
                        phone = reader.GetString("phone"),
                        email = reader.GetString("email"),
                        ipaddress = reader.GetString("ipaddress"),
                        labels = reader.GetString("label"), 
                        description = reader.GetString("description"),
                        convicted = reader.GetInt32("convicted"),
                        socials = reader.GetString("socials")
                    };
                    dataList.Add(data);
                }
                reader.Close();
                conn.Close();

                return dataList;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public static void EditData()
        {
            // blah blah blah
        }
    }
}
