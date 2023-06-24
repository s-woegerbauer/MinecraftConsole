using Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class World
    {
        public Block[,] Blocks { get; set; }
        public List<Player> Players = new List<Player>();

        public World(int sizeX)
        {
            Blocks = new Block[60, sizeX];
        }

        public static World GenerateNewWorld(int sizeX, Player player)
        {
            World world = new World(sizeX);

            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Air");
                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                world.Blocks[12, x] = Block.ByName("Grass");
            }

            for (int y = 13; y < 17; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Dirt");
                }
            }

            for (int y = 17; y < 30; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Stone");
                }
            }

            world.Players.Add(player);

            return world;
        }

        public void Draw()
        {
            for (int y = 0; y < 30; y++)
            {
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    WinAPI.DrawImage(Blocks[y, x].TexturePath, 4, 2, 4 * x + 1, 2 * y + 1);
                }
            }

            for (int i = 0; i < Players.Count; i++)
            {
                WinAPI.DrawImage(Players.ElementAt(i).PlayerFilePath, 4, 2, 4 * Players.ElementAt(i).X + 1, 2 * Players.ElementAt(i).Y + 1);
            }
        }

    }
}
