using System.Windows;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace RetroHub
{
    public partial class AddGameWindow : Window
    {
        // Common ROM file extensions
        private readonly string[] validRomExtensions = new[]
        {
            ".gba", ".gbc", ".gb",     // Game Boy
            ".nes", ".nez",             // NES
            ".smc", ".sfc",             // SNES
            ".n64", ".z64", ".v64",     // N64
            ".nds", ".dsi",             // DS
            ".3ds", ".cci",             // 3DS
            ".iso", ".cso",             // PSP/PS1
            ".bin", ".cue",             // PS1
            ".gcm", ".gcz",             // GameCube
            ".wbfs", ".wad",            // Wii
            ".gen", ".md", ".smd",      // Genesis/Mega Drive
            ".sms", ".gg",              // Master System/Game Gear
            ".32x",                     // 32X
            ".zip", ".7z", ".rar"       // Compressed ROMs
        };

        // Common emulator extensions
        private readonly string[] validEmuExtensions = new[]
        {
            ".exe", ".app", ".sh", ".bat"
        };

        public AddGameWindow()
        {
            InitializeComponent();
        }

        // Browse button handlers
        private void BrowseRom_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select ROM File",
                Filter = "ROM Files (*.gba;*.gbc;*.gb;*.nes;*.smc;*.sfc;*.n64;*.z64;*.nds;*.iso;*.zip;*.7z)|*.gba;*.gbc;*.gb;*.nes;*.smc;*.sfc;*.n64;*.z64;*.nds;*.iso;*.zip;*.7z|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                RomBox.Text = dialog.FileName;

                // Autofill title if empty
                if (string.IsNullOrWhiteSpace(TitleBox.Text))
                {
                    TitleBox.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
                }
            }
        }

        private void BrowseEmu_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Emulator",
                Filter = "Executable Files (*.exe;*.bat)|*.exe;*.bat|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                EmuBox.Text = dialog.FileName;
            }
        }

        private void BrowseIcon_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Icon",
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.ico;*.bmp)|*.png;*.jpg;*.jpeg;*.ico;*.bmp|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                IconBox.Text = dialog.FileName;
            }
        }

        // Drag and drop handlers
        private void TextBox_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void RomBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    RomBox.Text = files[0];

                    // Autofill title if empty
                    if (string.IsNullOrWhiteSpace(TitleBox.Text))
                    {
                        TitleBox.Text = Path.GetFileNameWithoutExtension(files[0]);
                    }
                }
            }
        }

        private void EmuBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    EmuBox.Text = files[0];
                }
            }
        }

        private void IconBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    IconBox.Text = files[0];
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Please enter a game title.", "Missing Title",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(RomBox.Text))
            {
                MessageBox.Show("Please select a ROM file.", "Missing ROM",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(EmuBox.Text))
            {
                MessageBox.Show("Please select an emulator.", "Missing Emulator",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validate ROM file exists
            if (!File.Exists(RomBox.Text))
            {
                MessageBox.Show("The ROM file does not exist.", "File Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate ROM file extension
            string romExtension = Path.GetExtension(RomBox.Text).ToLower();
            if (!validRomExtensions.Contains(romExtension))
            {
                var result = MessageBox.Show(
                    $"The file extension '{romExtension}' is not a recognized ROM format.\n\n" +
                    "Do you want to add it anyway?",
                    "Unrecognized ROM Format",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;
            }

            // Validate emulator file exists
            if (!File.Exists(EmuBox.Text))
            {
                MessageBox.Show("The emulator file does not exist.", "File Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validate emulator extension
            string emuExtension = Path.GetExtension(EmuBox.Text).ToLower();
            if (!validEmuExtensions.Contains(emuExtension))
            {
                var result = MessageBox.Show(
                    $"The file extension '{emuExtension}' is not a recognized executable format.\n\n" +
                    "Do you want to add it anyway?",
                    "Unrecognized Emulator Format",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                    return;
            }

            // Validate icon if provided
            if (!string.IsNullOrWhiteSpace(IconBox.Text) && !File.Exists(IconBox.Text))
            {
                MessageBox.Show("The icon file does not exist.", "File Not Found",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // All validations passed then add the game
            new GameRepository().AddGame(
                TitleBox.Text,
                RomBox.Text,
                EmuBox.Text,
                IconBox.Text);

            MessageBox.Show("Game added successfully!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }
    }
}