using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class MenuPreset
    {
        public List<Tuple<string, List<Action>, ConsoleColor, bool>>? List { get; set; }
        private int Current = 0;

        public MenuPreset(List<Tuple<string, List<Action>, ConsoleColor, bool>> list, int current)
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
                    if (Current >= List!.Count)
                    {
                        Current = List.Count - 1;
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
                else if (pressed == Controls.Remove)
                {
                    if (List![0].Item4)
                    {
                        string name = List[Current].Item1;
                        List!.RemoveAt(Current);
                        Directory.Delete($"{Directory.GetCurrentDirectory()}\\Saves\\{name}", true);
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
            foreach(Tuple<string, List<Action>, ConsoleColor, bool> line in List!)
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
