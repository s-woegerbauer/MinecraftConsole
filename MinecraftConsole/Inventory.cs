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


        public void Draw()
        {
            for(int i = 0; i < 9; i++)
            {
                if (Hotbar[i].Count > 0)
                {
                    WinAPI.DrawImage(Hotbar[i][0].TexturePath, 4, 2, 1 + i * 4, 50);
                }
            }
        }
    }
}
