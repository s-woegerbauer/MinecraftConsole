using Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class Inventory
    {
        public List<Item>[] Hotbar { get; set; }

        public Inventory()
        {
            Hotbar = new List<Item>[9];
            for(int i = 0; i < 9; i++)
            {
                Hotbar[i] = new List<Item>();
            }
        }

        public void Add(Item item, int slot)
        {
            Hotbar[slot].Add(item);
        }

        public void Add(Item item)
        {
            for(int i = 0; i < 9; i++)
            {
                if (Hotbar[i].Contains(item) && Hotbar[i].Count < item.MaxStack)
                {
                    Hotbar[i].Add(item);
                    break;
                }
                else if (Hotbar[i].Count == 0)
                {
                    Hotbar[i].Add(item);
                    break;
                }
            }
        }

        public void Remove(Item item, int slot)
        {
            Hotbar[slot].Remove(item);
        }

        public void Remove(Item item)
        {
            for (int i = 0; i < 9; i++)
            {
                if (Hotbar[i].Contains(item))
                {
                    Hotbar[i].Remove(item);
                    break;
                }
            }
        }


        public void Draw(Player player)
        {
            for(int i = 0; i < 9; i++)
            {
                if (Hotbar[i].Count > 0)
                {
                    if (player.HotbarSlot == i)
                    {
                        WinAPI.DrawImage("\\Textures\\Current_Slot.jpg", 8, 5, 1 + i * 8, 45);
                        WinAPI.DrawImage(ConvertPath(Hotbar[i][0].TexturePath), 6, 3, 2 + i * 8, 46);
                    }
                    else
                    {
                        WinAPI.DrawImage(ConvertPath(Hotbar[i][0].TexturePath), 8, 5, 1 + i * 8, 45);
                    }
                }
                else
                {
                    if (player.HotbarSlot == i)
                    {
                        WinAPI.DrawImage("\\Textures\\Current_Slot.jpg", 8, 5, 1 + i * 8, 45);
                    }
                    else
                    {
                        WinAPI.DrawImage("\\Textures\\None_Slot.jpg", 8, 5, 1 + i * 8, 45);
                    }
                }

                Console.SetCursorPosition((int) Math.Round(i * 6.75) + 3, 40);
                Console.Write(i + 1);
            }
        }

        private static string ConvertPath(string currentPath)
        {
            string newPath = string.Empty;

            foreach(char ch in currentPath)
            {
                if(ch == '.')
                {
                    return newPath + "_Slot.jpg";
                }

                newPath += ch;
            }

            return newPath;
        }
    }
}