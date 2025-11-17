using Microsoft.Data.Sqlite;
using System.Windows;
using System.IO;

namespace RetroHub
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SQLitePCL.Batteries.Init();

            string databaseFileName = "games.db";

            if (!File.Exists(databaseFileName))
            {
                using (var connection = new SqliteConnection("Data Source=games.db"))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = @"CREATE TABLE Games( Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Title TEXT NOT NULL, RomPath TEXT NOT NULL, EmuPath TEXT NOT NULL, IconPath TEXT NOT NULL);";
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}