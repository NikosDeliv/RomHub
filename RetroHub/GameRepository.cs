using Microsoft.Data.Sqlite;
using RetroHub;
using System.Collections.Generic;
using System.Reflection;

public class GameRepository
{

	private const string Conn = "Data Source=games.db";

    public List<Game> LoadGames()
	{
		var list = new List<Game>();
        using var conn = new SqliteConnection(Conn);
        conn.Open();

        var cmd = conn.CreateCommand(); 
        cmd.CommandText = "SELECT Id, Title, RomPath, EmuPath, IconPath FROM Games";

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Game
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                RomPath = reader.GetString(2),
                EmuPath = reader.GetString(3),
                IconPath = reader.GetString(4)
            });
        }
        return list;
    }
    public void AddGame(string title, string rom, string emulator, string Icon)
    {
        using var conn = new SqliteConnection(Conn);
        conn.Open();

        var cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO Games (Title, RomPath, EmuPath, IconPath) VALUES  ($title, $rom, $emu, $Icon)";
        cmd.Parameters.AddWithValue("$title", title);
        cmd.Parameters.AddWithValue("$rom", rom);
        cmd.Parameters.AddWithValue("$emu", emulator);
        cmd.Parameters.AddWithValue("$Icon", Icon);

        cmd.ExecuteNonQuery();
    }

}
