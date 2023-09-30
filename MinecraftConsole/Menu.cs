using System.Numerics;

namespace MinecraftConsole
{
    public static class Menu
    {
        public static class Options
        {
            public static void Go()
            {

            }
        }

        public static class Pause
        {
            public static void Go(string worldName, Player player, World world)
            {
                List<Tuple<string, List<Action>, ConsoleColor>> menuItems = new List<Tuple<string, List<Action>, ConsoleColor>>
                {
                    new Tuple<string, List<Action>, ConsoleColor>("Resume", new List<Action>
                    {
                        () => Regeneration.Enable(),
                        () => Program.StartWorld(world, worldName),
                    }, ConsoleColor.Green),

                    new Tuple<string, List<Action>, ConsoleColor>("Options", new List<Action>
                    {
                        () => Options.Go(),
                    }, ConsoleColor.Blue),

                    new Tuple<string, List<Action>, ConsoleColor>("Main Menu", new List<Action>
                    {
                        () => Leave(worldName, player),
                    }, ConsoleColor.Red)
                };

                MenuPreset menu = new MenuPreset(menuItems, 0);
                menu.Go();
            }

            public static void Save(string worldName, Player player)
            {
                Helper.CreateSaveFile(worldName);
                Helper.WritePlayerDetails(worldName, player);
                Helper.WriteWorldDetails(worldName, Program.currentChunk);
            }

            public static void Leave(string worldName, Player player)
            {
                Save(worldName, player);
                Console.Clear();
                Main.Go();
            }
        }
        
        public static class Main
        {
            public static void Go()
            {
                List<Tuple<string, List<Action>, ConsoleColor>> menuItems = new List<Tuple<string, List<Action>, ConsoleColor>>();

                foreach(string fileName in Directory.GetDirectories(Directory.GetCurrentDirectory() + "\\Saves"))
                {
                    menuItems.Add(new Tuple<string, List<Action>, ConsoleColor>(fileName.Split('\\')[^1], new List<Action>
                    {
                        () => Load(fileName.Split('\\')[^1])
                    }, ConsoleColor.Green));
                }

                menuItems.Add(new Tuple<string, List<Action>, ConsoleColor>("Create New World", new List<Action>
                {
                    () => CreateNewWorld()
                }, ConsoleColor.Blue)); 

                MenuPreset mainMenu = new MenuPreset(menuItems, 0);
                mainMenu.Go();
            }

            public static void CreateNewWorld()
            {
                for(int i = 0; i < int.MaxValue; i++)
                {
                    if(!File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\NewWorld" + i))
                    {
                        Player player = new Player("player" + 1);
                        World world = World.GenerateNewWorld(35, player);
                        Pause.Save("NewWorld" + i, player);
                        Program.StartWorld(world, "NewWorld" + i);
                    }
                }
            }

            public static void Load(string worldName)
            {
                if(!Helper.CheckVersion(worldName))
                {
                    throw new ArgumentException("Incorrect Version!\nTry to run a world in your version!\nYou can transfer world by changing the version.txt file!\nTransfer worlds with caution!");
                }
                else
                {
                    Player player = Helper.LoadPlayer(worldName);
                    World world = Helper.LoadWorldDetails(worldName);
                    world.Players.Add(player);

                    Program.player = player;
                    Program.StartWorld(world, worldName);
                }
            }

            public static void Leave()
            {
                Environment.Exit(0);
            }
        }

        private static class Helper
        {
            public static bool CheckVersion(string worldName)
            {
                return File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\version.txt") == "version=beta_2.0";
            }

            public static void WritePlayerDetails(string worldName, Player player)
            {
                WritePlayerInfo(worldName, player);
                WritePlayerInventory(worldName, player);
            }

            public static void WriteWorldDetails(string worldName, World world)
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv"))
                {
                    File.Create($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv");
                }

                string[] lines = new string[world.Blocks.GetLength(0)];

                for (int y = 0; y < world.Blocks.GetLength(0); y++)
                {
                    for (int x = 0; x < world.Blocks.GetLength(1); x++)
                    {
                        if(x!=0)
                        {
                            lines[y] += ",";
                        }

                        lines[y] += world.Blocks[y,x].Name;
                    }
                }

                File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv", lines);
            }

            public static World LoadWorldDetails(string worldName)
            {
                World world = new World(35);

                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv"))
                {
                    File.Create($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv");
                }

                string[] lines = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Chunks\\0.csv");

                if (lines.Length == 0)
                {
                    return World.GenerateNewWorld(35, new Player("newPlayer"));
                }

                string[,] chunk = new string[lines.Length, lines[0].Split(',').Length];

                for(int i = 0; i < lines.Length; i++)
                {
                    for(int j = 0; j < lines[0].Split(',').Length; j++)
                    {
                        chunk[i, j] = lines[i].Split(',')[j];
                    }
                }

                for(int i = 0; i < lines.Length; i++)
                {
                    for(int j = 0; j < lines[0].Split(',').Length; j++)
                    {
                        Block block = Block.ByName(chunk[i, j]);
                        world.Blocks[i, j] = block;
                    }
                }

                return world;
            }

            public static Player LoadPlayer(string worldName)
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt"))
                {
                    File.Create($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt");
                    File.Create($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\inventory.txt");
                    return new Player("player");
                }
                else
                {
                    string[] info = File.ReadAllLines($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt");
                    Player player = new Player(info[0].Split('=')[1]);
                    player.HealthBar.currentHealth = int.Parse(info[1].Split('=')[1]);
                    player.HealthBar.maxHealth = int.Parse(info[2].Split('=')[1]);
                    player.CurrentChunk = int.Parse(info[3].Split('=')[1]);
                    player.X = int.Parse(info[4].Split('=')[1].Split(';')[0]);
                    player.Y = int.Parse(info[4].Split('=')[1].Split(';')[1]);
                    player.Gamemode = GamemodeHelp.GetGamemodeByNumber(int.Parse(info[5].Split('=')[1]));

                    string inventory = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\inventory.csv");

                    foreach(string slot in inventory.Split(","))
                    {
                        if (slot.Length > 0)
                        {
                            for (int i = 0; i < int.Parse(slot.Split(":")[1]); i++)
                            {
                                int index = Array.IndexOf(inventory.Split(","), slot);
                                player.Inventory.Hotbar[index].Add(Item.ByName(slot.Split(":")[0]));
                            }
                        }
                    }

                    return player;
                }
            }

            private static void WritePlayerInfo(string worldName, Player player)
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt"))
                {
                    File.Create($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt");
                }

                List<string> playerInfo = new List<string>
                {
                    "name=" + player.Name,
                    "currentHealth=" + player.HealthBar.currentHealth,
                    "maxHealth=" + player.HealthBar.maxHealth,
                    "currentChunk=" + player.CurrentChunk,
                    "location=" + player.X + ";" + player.Y,
                    "gamemode=" + GamemodeHelp.GetNumberByGamemode(player.Gamemode)
                };

                while(!IsFileAccessible($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt")) {}

                File.WriteAllLines($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\info.txt", playerInfo);
            }

            private static void WritePlayerInventory(string worldName, Player player)
            {
                if (!File.Exists($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\inventory.csv"))
                {
                    File.Create($"\\Saves\\{worldName}\\Player\\inventory.csv");
                }

                int i = 0;
                string text = string.Empty;

                foreach (List<Item> items in player.Inventory.Hotbar)
                {
                    Item item = items.ElementAtOrDefault(0)!;

                    if(item != default)
                    {
                        if (i != 0)
                        {
                            text += ",";
                        }

                        text += item.Name + ":" + items.Count.ToString();
                    }

                    i++;
                }

                File.WriteAllText($"{Directory.GetCurrentDirectory()}\\Saves\\{worldName}\\Player\\inventory.csv", text);
            }

            public static void CreateSaveFile(string worldName)
            {
                if(!Directory.Exists(Directory.GetCurrentDirectory() + "\\Saves\\" + worldName))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Saves\\" + worldName);
                }

                if (!Directory.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Chunks"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Chunks");
                }

                if (!Directory.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player");
                }

                if (!File.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\version.txt"))
                {
                    FileStream fs =  File.Create(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\version.txt");
                    fs.Close();
                    File.WriteAllText(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\version.txt", "version=beta_2.0");
                }

                if (!File.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player\\info.txt"))
                {
                    FileStream fs = File.Create(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player\\info.txt");
                    fs.Close();
                }

                if (!File.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player\\inventory.csv"))
                {
                    FileStream fs = File.Create(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Player\\inventory.csv");
                    fs.Close();
                }

                if (!File.Exists(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Chunks\\0.csv"))
                {
                    FileStream fs = File.Create(Directory.GetCurrentDirectory() + $"\\Saves\\{worldName}\\Chunks\\0.csv");
                    fs.Close();
                }
            }

            public static bool IsFileAccessible(string filePath)
            {
                int maxAttempts = 5;
                int retryDelayMilliseconds = 1000;

                int attempts = 0;

                while (attempts < maxAttempts)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.None))
                        {
                            return true;
                        }
                    }
                    catch (IOException ex)
                    {
                        if (IsFileLocked(ex))
                        {
                            Console.WriteLine("File is locked by another process. Retrying...");
                            attempts++;

                            Thread.Sleep(retryDelayMilliseconds);
                        }
                        else
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                    }
                }

                return false; // Max retry attempts reached, file is not accessible
            }

            private static bool IsFileLocked(IOException ex)
            {
                int errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(ex) & ((1 << 16) - 1);
                return errorCode == 32 || errorCode == 33; // 32: Sharing violation, 33: Lock violation
            }
        }
    }
}