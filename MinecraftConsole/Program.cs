namespace MinecraftConsole
{
    public class Program
    {
        public static ConsoleColor MenuColor = ConsoleColor.Green;
        public static bool IsChangingTexture = false;
        public static bool IsAlreadyJumping = false;
        public static DateTime Jump = DateTime.Now;
        public static Player Player = new Player("player1");
        public static World CurrentChunk = new World(35);
        public static bool DeveloperMode = false;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            MaximizeWindow.Go();
            Menu.Main.Go();
        }

        public static void StartWorld(World world, string worldName)
        {
            CurrentChunk = world;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;
            Thread.Sleep(400);
            Task.Run(() => Regeneration.Go(1000));
            if(DeveloperMode)
            {
                world.Draw(true);
            }
            else
            {
                world.Refresh();
            }
            Console.CursorVisible = false;

            while (true)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                if (key == Controls.MoveLeft)
                {
                    Player.CurrentDirection = 3;
                    Task.Run(() => Player.Move(-1, world));
                    Thread.Sleep(100);
                    Task.Run(() => Player.Fall(world));
                }
                else if (key == Controls.MoveRight)
                {
                    Player.CurrentDirection = 1;
                    Task.Run(() => Player.Move(1, world));
                    Thread.Sleep(100);
                    Task.Run(() => Player.Fall(world));
                }
                else if (key == Controls.Jump)
                {
                    if (DateTime.Now - Jump > TimeSpan.FromMilliseconds(500))
                    {
                        Task.Run(() => Player.Jump(world));
                    }
                }
                else if (key == Controls.LookUp)
                {
                    Player.CurrentDirection = 0;
                }
                else if (key == Controls.LookDown)
                {
                    Player.CurrentDirection = 2;
                }
                else if (key == Controls.BreakBlock)
                {
                    world = Player.BreakBlock(world);
                    Task.Run(() => Player.Fall(world));
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.Refresh)
                {
                    if(DeveloperMode)
                    {
                        world.Draw(true);
                    }
                    else
                    {
                        world.Refresh();
                    }
                }
                else if (key == Controls.Place)
                {
                    Player.PlaceBlock(world);
                }
                else if (key == Controls.HotbarSlotOne)
                {
                    Player.HotbarSlot = 0;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotTwo)
                {
                    Player.HotbarSlot = 1;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotThree)
                {
                    Player.HotbarSlot = 2;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotFour)
                {
                    Player.HotbarSlot = 3;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotFive)
                {
                    Player.HotbarSlot = 4;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotSix)
                {
                    Player.HotbarSlot = 5;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotSeven)
                {
                    Player.HotbarSlot = 6;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotEight)
                {
                    Player.HotbarSlot = 7;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.HotbarSlotNine)
                {
                    Player.HotbarSlot = 8;
                    Player.Inventory.Draw(Player);
                }
                else if (key == Controls.Menu)
                {
                    Menu.Pause.Go(worldName, Player, world);
                }
            }
        }
    }
}