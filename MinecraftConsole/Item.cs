using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class Item
    {
        public ItemType Type { get; set; }
        public string Name { get; set; }
        public int MaxStack { get; set; }
        public string TexturePath { get; set; }

        public Item(ItemType type, string name, int maxStack)
        {
            Type = type;
            Name = name;
            MaxStack = maxStack;
            TexturePath = "\\Textures\\" + name + ".jpg";
        }

        public static List<Item> List = new List<Item>()
        {
            new Item(ItemType.Block, "Grass", 64),
            new Item(ItemType.Block, "Stone", 64),
            new Item(ItemType.Block, "Dirt", 64)
        };

        public static Item ByName(string name)
        {
            foreach (Item listItem in List)
            {
                if (listItem.Name == name)
                {
                    return listItem;
                }
            }

            return null;
        }

        public Block GetBlock()
        {
            return Block.ByName(Name);
        }
    }
}
