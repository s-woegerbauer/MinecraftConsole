using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class MenuPreset
    {
        public List<Tuple<string, List<Action>, ConsoleColor>>? List { get; set; }
        private int Current = 0;

        public MenuPreset(List<Tuple<string, List<Action>, ConsoleColor>> list, int current)
        {
            List = list;
            Current = current;
        }

        public void Go()
        {
            Regeneration.Disable();

            while (true)
            {
                Write();

                ConsoleKey pressed = Console.ReadKey().Key;

                if (pressed == ConsoleKey.Enter)
                {
                    break;
                }
                else if (pressed == ConsoleKey.DownArrow)
                {
                    Current++;
                    if (Current > 2)
                    {
                        Current = 2;
                    }
                }
                else if (pressed == ConsoleKey.UpArrow)
                {
                    Current--;
                    if (Current < 0)
                    {
                        Current = 0;
                    }
                }
            }
            
            foreach(Action action in List![Current].Item2)
            {
                action();
            }
        }

        private void Write()
        {
            Console.Clear();
            foreach(Tuple<string, List<Action>, ConsoleColor> line in List!)
            {
                if(List.IndexOf(line) == Current)
                {
                    Console.ForegroundColor = line.Item3;
                    Console.WriteLine(line.Item1);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine(line.Item1);
                }
            }
        }
    }
}
