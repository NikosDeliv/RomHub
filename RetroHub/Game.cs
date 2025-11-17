using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroHub
{
    public class Game
    {
        public int Id{get; set;}
        public string Title { get; set; }
        public string RomPath { get; set; }
        public string EmuPath { get; set; }
        public string IconPath { get; set; }

    }
}
