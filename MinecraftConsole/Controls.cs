using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public static class Controls
    {
        public static ConsoleKey Menu = ConsoleKey.Escape;
        public static ConsoleKey MoveLeft = ConsoleKey.A;
        public static ConsoleKey MoveRight = ConsoleKey.D;
        public static ConsoleKey LookUp = ConsoleKey.W;
        public static ConsoleKey LookDown = ConsoleKey.S;
        public static ConsoleKey Jump = ConsoleKey.Spacebar;
        public static ConsoleKey BreakBlock = ConsoleKey.Q;
        public static ConsoleKey Refresh = ConsoleKey.R;
        public static ConsoleKey Place = ConsoleKey.F;
        public static ConsoleKey HotbarSlotNine = ConsoleKey.D9;
        public static ConsoleKey HotbarSlotEight = ConsoleKey.D8;
        public static ConsoleKey HotbarSlotSeven = ConsoleKey.D7;
        public static ConsoleKey HotbarSlotSix = ConsoleKey.D6;
        public static ConsoleKey HotbarSlotFive = ConsoleKey.D5;
        public static ConsoleKey HotbarSlotFour = ConsoleKey.D4;
        public static ConsoleKey HotbarSlotThree = ConsoleKey.D3;
        public static ConsoleKey HotbarSlotTwo = ConsoleKey.D2;
        public static ConsoleKey HotbarSlotOne = ConsoleKey.D1;
        public static ConsoleKey Remove = ConsoleKey.X;

        public static Dictionary<ConsoleKey, string> ControlKeys = new Dictionary<ConsoleKey, string>
        {
            { ConsoleKey.Escape, "Menu" },
            { ConsoleKey.A, "MoveLeft" },
            { ConsoleKey.D, "MoveRight" },
            { ConsoleKey.W, "LookUp" },
            { ConsoleKey.S, "LookDown" },
            { ConsoleKey.Spacebar, "Jump" },
            { ConsoleKey.Q, "BreakBlock" },
            { ConsoleKey.R, "Refresh" },
            { ConsoleKey.F, "Place" },
            { ConsoleKey.D9, "HotbarSlotNine" },
            { ConsoleKey.D8, "HotbarSlotEight" },
            { ConsoleKey.D7, "HotbarSlotSeven" },
            { ConsoleKey.D6, "HotbarSlotSix" },
            { ConsoleKey.D5, "HotbarSlotFive" },
            { ConsoleKey.D4, "HotbarSlotFour" },
            { ConsoleKey.D3, "HotbarSlotThree" },
            { ConsoleKey.D2, "HotbarSlotTwo" },
            { ConsoleKey.D1, "HotbarSlotOne" },
            { ConsoleKey.X, "Remove" }
        };

        public static ConsoleKey GetKeyByValue(string value)
        {
            return ControlKeys.FirstOrDefault(x => x.Value == value).Key;
        }

        public static void Change(ConsoleKey key)
        {
            Console.Clear();
            Console.Write($"Enter new key for {ControlKeys[key]}: ");
            ConsoleKey newKey = Console.ReadKey().Key;
            ChangeKey(ControlKeys[key], newKey);
        }

        public static void ChangeKey(string controlName, ConsoleKey newKey)
        {
            FieldInfo fieldInfo = typeof(Controls).GetField(controlName)!;

            if (fieldInfo != null && fieldInfo.FieldType == typeof(ConsoleKey))
            {
                fieldInfo.SetValue(null, newKey);
            }
            else
            {
                throw new ArgumentException($"Control '{controlName}' not found or is not of type ConsoleKey.");
            }
        }
    }
}