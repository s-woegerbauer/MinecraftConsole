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
            Blocks = new Block[24, sizeX];
        }

        public static World GenerateNewWorld(int sizeX, Player player)
        {
            World world = new World(sizeX);

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Air");
                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                world.Blocks[10, x] = Block.ByName("Grass");
            }

            for (int y = 11; y < 14; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Dirt");
                }
            }

            for (int y = 14; y < 23; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    world.Blocks[y, x] = Block.ByName("Stone");
                }
            }

            for(int x = 0; x < sizeX; x++)
            {
                world.Blocks[23, x] = Block.ByName("Bedrock");
            }

            world.Players.Add(player);

            return world;
        }

        public void Draw(bool drawPlayer)
        {
            for (int y = 0; y < 24; y++)
            {
                for (int x = 0; x < Blocks.GetLength(1); x++)
                {
                    WinAPI.DrawImage(Blocks[y, x].TexturePath, 4, 2, 4 * x + 1, 2 * y + 1);
                }
            }

            if (drawPlayer)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    WinAPI.DrawImage(Players.ElementAt(i).PlayerFilePath, 4, 2, 4 * Players.ElementAt(i).X + 1, 2 * Players.ElementAt(i).Y + 1);
                }
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == 7)
                {
                    Draw(true);
                }
                else
                {
                    Draw(false);
                }
            }
        }
    }
}
