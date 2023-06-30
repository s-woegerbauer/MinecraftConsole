using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class Block
    {
        public string Name { get; set; }
        public string TexturePath { get; set; }
        public Item DropItem { get; set; }
        public ItemType ToolType { get; set; }
        public int Hardness { get; set; }
        public bool IsAir { get; set; }

        public Block(string name, ItemType toolType, int hardness, bool isAir)
        {
            TexturePath = "\\Textures\\" + name + ".jpg";
            DropItem = Item.ByName(name);
            ToolType = toolType;
            Hardness = hardness;
            IsAir = isAir;
            Name = name;
        }

        public static List<Block> List = new List<Block>()
        {
            new Block("None", ItemType.None, 0, false),
            new Block("Air", ItemType.None, int.MaxValue, true),
            new Block("Grass", ItemType.Shovel, 2, false),
            new Block("Dirt", ItemType.Shovel, 1, false),
            new Block("Stone", ItemType.Pickaxe, 2, false),
            new Block("Bedrock", ItemType.None, int.MaxValue, false)
        };

        public static Block ByName(string name)
        {

            foreach (Block listBlock in List)
            {
                if (listBlock.Name == name)
                {
                    return listBlock;
                }
            }

            return null;
        }
    }
}