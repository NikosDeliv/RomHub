# RomHub

A ROM library manager for organizing and launching retro games with their respective emulators. ( Ill add  images soon)

## What It Does

RomHub acts as a central hub for your ROM collection. Add your games once with their emulator paths, then launch them with a double-click. No more hunting through folders to find the right ROM or remembering which emulator to use.

## Features

- Grid-based game library display
- Add games with ROM path, emulator path, and optional icon
- Launch games by double-clicking
- Drag and drop or browse for files
- Validates ROM and emulator file types
- SQLite database for storing your library

## Requirements

- Windows
- .NET 6.0 or higher
- Emulators for your ROMs

## Installation

1. Clone the repository
2. Open in Visual Studio 2022 or later
3. Restore NuGet packages:
   - Microsoft.Data.Sqlite
   - SQLitePCLRaw.bundle_e_sqlite3
4. Build and run

## Usage

**Adding a Game:**
1. Click "Add Game"
2. Enter game title
3. Select or drag ROM file
4. Select or drag emulator executable
5. Optionally add an icon image
6. Click "Add Game"

**Launching a Game:**
Double-click any game in the library to launch it with its configured emulator.

## Supported ROM Formats

Game Boy (.gba, .gbc, .gb), NES (.nes), SNES (.smc, .sfc), N64 (.n64, .z64), DS (.nds), PlayStation (.iso, .bin, .cue), GameCube (.gcm), Wii (.wbfs), Genesis (.gen, .md), and more. Compressed formats (.zip, .7z, .rar) are also supported.

## Technical Details

- Built with C# and WPF
- Uses SQLite for local storage
- Database file (games.db) created automatically on first run

## Database Schema

```sql
CREATE TABLE Games(
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    RomPath TEXT NOT NULL,
    EmuPath TEXT NOT NULL,
    IconPath TEXT NOT NULL
);
```

## License

MIT License
