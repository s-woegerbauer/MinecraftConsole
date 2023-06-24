using Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Inventory Inventory { get; set; }
        public int HotbarSlot { get; set; }
        public Gamemode Gamemode { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string PlayerFilePath { get; set; }

        public Player(string name)
        {
            Name = name;
            Health = 20;
            Inventory = new Inventory();
            HotbarSlot = 0;
            Gamemode = Gamemode.Survival;
            PlayerFilePath = "\\Textures\\Player.jpg";
            X = 10;
            Y = 11;
        }

        public async void Move(int direction, World world)
        { 

            if (X <= world.Blocks.GetLength(1) && Y <= world.Blocks.GetLength(0) && X + direction >= 0 && X + direction < world.Blocks.GetLength(1))
            {
                if (world.Blocks[Y, X + direction].IsAir)
                {
                    while (Program.IsChangingTexture)
                    {

                    }

                    Program.IsChangingTexture = true;
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
                    X += direction;
                    WinAPI.DrawImage("\\Textures\\Player.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
                    Program.IsChangingTexture = false;
                }
            }
        }

        public async void Jump(World world)
        {
            while (Program.IsChangingTexture)
            {

            }

            Program.IsChangingTexture = true;
            WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
            Y--;
            WinAPI.DrawImage("\\Textures\\Player.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
            Program.IsChangingTexture = false;
            await Task.Delay(200);
            Program.IsChangingTexture = false;
            await Task.Run(() => Fall(world));
        }

        public async void Fall(World world)
        {
            int timesFallen = 0;
            while(world.Blocks[Y + 1, X].IsAir)
            {
                timesFallen++;
                while (Program.IsChangingTexture)
                {

                }

                Program.IsChangingTexture = true;
                WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
                Y++;
                WinAPI.DrawImage("\\Textures\\Player.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
                Program.IsChangingTexture = false;

                int delay = 200 - 10 * timesFallen;

                if(delay < 0)
                {
                    delay = 0;
                }

                await Task.Delay(delay);
            }

            int reduceHealth = timesFallen - 4;

            if(reduceHealth < 0)
            {
                reduceHealth = 0;
            }

            Health -= reduceHealth;
        }
    }
}
