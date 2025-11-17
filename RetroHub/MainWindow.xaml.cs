using System.Windows;
using System.Diagnostics;
using RetroHub;

namespace RetroHub;
public partial class MainWindow : Window
{
    GameRepository repo = new GameRepository();

    public MainWindow()
    {
        InitializeComponent();
        LoadGames();
    }

    private void LoadGames()
    {
        GameList.Items.Clear();
        foreach (var game in repo.LoadGames())
            GameList.Items.Add(game);
    }
    
    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
        var addGameWindow = new AddGameWindow();
        addGameWindow.ShowDialog();
        LoadGames();
    }

    private void GameList_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (GameList.SelectedItem is Game game)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = game.EmuPath,
                Arguments = $"\"{game.RomPath}\"", 
                UseShellExecute = true
            });
        }
    }
}