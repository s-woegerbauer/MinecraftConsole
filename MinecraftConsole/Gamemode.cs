using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public enum Gamemode
    {
        Survival,
        Creative,
        Spectator,
        Adventure,
        None
    }
    
    public static class GamemodeHelp
    {
        public static int GetNumberByGamemode(Gamemode gamemode)
        {
            switch(gamemode)
            {
                case Gamemode.Survival:
                    return 0;

                case Gamemode.Creative:
                    return 1;

                case Gamemode.Spectator:
                    return 2;

                case Gamemode.Adventure:
                    return 3;

                default:
                    return -1;
            }
        }

        public static Gamemode GetGamemodeByNumber(int gamemode)
        {
            switch (gamemode)
            {
                case 0:
                    return Gamemode.Survival;

                case 1:
                    return Gamemode.Creative;

                case 2:
                    return Gamemode.Spectator;

                case 3:
                    return Gamemode.Adventure;

                default:
                    return Gamemode.None;
            }
        }
    }
}
