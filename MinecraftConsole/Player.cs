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
        public int CurrentDirection { get; set; }

        public Player(string name)
        {
            Name = name;
            Health = 20;
            Inventory = new Inventory();
            HotbarSlot = 0;
            Gamemode = Gamemode.Survival;
            PlayerFilePath = "\\Textures\\Player.jpg";
            X = 10;
            Y = 9;
            CurrentDirection = 1;
        }

        public World PlaceBlock(World world)
        {
            if (Inventory.Hotbar[HotbarSlot].Count > 0)
            {
                switch (CurrentDirection)
                {
                    case 0:
                        if (world.Blocks[Y - 1, X].IsAir)
                        {
                            Block block = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            world.Blocks[Y - 1, X] = block;
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                            WinAPI.DrawImage(block.TexturePath, 4, 2, 4 * X + 1, 2 * (Y - 1) + 1);
                        }
                        break;

                    case 1:
                        if (world.Blocks[Y, X + 1].IsAir)
                        {
                            Block block = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            world.Blocks[Y, X + 1] = block;
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                            WinAPI.DrawImage(block.TexturePath, 4, 2, 4 * (X + 1) + 1, 2 * Y + 1); world.Blocks[Y, X + 1] = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                        }
                        break;

                    case 2:
                        if (world.Blocks[Y + 1, X].IsAir)
                        {
                            Block block = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            world.Blocks[Y + 1, X] = block;
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                            WinAPI.DrawImage(block.TexturePath, 4, 2, 4 * X + 1, 2 * (Y + 1) + 1);
                        }
                        break;

                    case 3:
                        if (world.Blocks[Y, X - 1].IsAir)
                        {
                            Block block = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            world.Blocks[Y, X + 1] = block;
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                            WinAPI.DrawImage(block.TexturePath, 4, 2, 4 * (X - 1) + 1, 2 * Y + 1); world.Blocks[Y, X - 1] = Inventory.Hotbar[HotbarSlot].ElementAt(0).GetBlock();
                            Inventory.Hotbar[HotbarSlot].RemoveAt(0);
                        }
                        break;
                }
            }

            return world;
        }

        public World BreakBlock(World world)
        {
            if(CurrentDirection == 0)
            {
                if (world.Blocks[Y - 1, X].Hardness != int.MaxValue)
                {
                    Inventory.Add(world.Blocks[Y - 1, X].DropItem);
                    world.Blocks[Y - 1, X] = Block.ByName("Air");
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * (Y - 1) + 1);
                }
            }
            else if(CurrentDirection == 1)
            {
                if (world.Blocks[Y, X + 1].Hardness != int.MaxValue)
                {
                    Inventory.Add(world.Blocks[Y, X + 1].DropItem);
                    world.Blocks[Y, X + 1] = Block.ByName("Air");
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * (X + 1) + 1, 2 * Y + 1);
                }
            }
            else if (CurrentDirection == 2)
            {
                if (world.Blocks[Y + 1, X].Hardness != int.MaxValue)
                {
                    Inventory.Add(world.Blocks[Y + 1, X].DropItem);
                    world.Blocks[Y + 1, X] = Block.ByName("Air");
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * (Y + 1) + 1);
                }
            }
            else if (CurrentDirection == 3)
            {
                if (world.Blocks[Y, X - 1].Hardness != int.MaxValue)
                {
                    Inventory.Add(world.Blocks[Y, X - 1].DropItem);
                    world.Blocks[Y, X - 1] = Block.ByName("Air");
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * (X - 1) + 1, 2 * Y + 1);
                }
            }

            return world;
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
                    if (direction == 1)
                    {
                        PlayerFilePath = "\\Textures\\Player.jpg";
                        WinAPI.DrawImage(PlayerFilePath, 4, 2, 4 * X + 1, 2 * Y + 1);
                    }
                    else
                    {
                        PlayerFilePath = "\\Textures\\Player_Left.jpg";
                        WinAPI.DrawImage(PlayerFilePath, 4, 2, 4 * X + 1, 2 * Y + 1);
                    }
                    Program.IsChangingTexture = false;
                }
            }
        }

        public async void Jump(World world)
        {
            if (!Program.IsAlreadyJumping)
            {
                Program.IsAlreadyJumping = true;

                while (Program.IsChangingTexture)
                {

                }

                if (!world.Blocks[Y + 1, X].IsAir && world.Blocks[Y - 1, X].IsAir)
                {
                    Program.IsChangingTexture = true;
                    WinAPI.DrawImage("\\Textures\\Air.jpg", 4, 2, 4 * X + 1, 2 * Y + 1);
                    Y--;
                    WinAPI.DrawImage(PlayerFilePath, 4, 2, 4 * X + 1, 2 * Y + 1);
                    Program.IsChangingTexture = false;
                    await Task.Delay(200);
                    Program.IsChangingTexture = false;
                    await Task.Run(() => Fall(world));
                }

                Program.IsAlreadyJumping = false;
            }
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
                WinAPI.DrawImage(PlayerFilePath, 4, 2, 4 * X + 1, 2 * Y + 1);
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
