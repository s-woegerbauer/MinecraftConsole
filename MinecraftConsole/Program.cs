namespace MinecraftConsole
{
    public class Program
    {
        public static bool IsChangingTexture = false;
        public static bool IsAlreadyJumping = false;
        public static DateTime Jump = DateTime.Now;
        public static Player player = new Player("Player1");
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(400);
            Console.CursorVisible = false;
            MaximizeWindow.Go();
            Task.Run(() => Regeneration.Go(1000));
            World world = World.GenerateNewWorld(35, player);
            world.Refresh();
            
            while(true)
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                if(key == Controls.MoveLeft)
                {
                    player.CurrentDirection = 3;
                    Task.Run(() => player.Move(-1, world));
                    Thread.Sleep(100);
                    Task.Run(() => player.Fall(world));
                }
                else if (key == Controls.MoveRight)
                {
                    player.CurrentDirection = 1;
                    Task.Run(() => player.Move(1, world));
                    Thread.Sleep(100);
                    Task.Run(() => player.Fall(world));
                }
                else if (key == Controls.Jump)
                {
                    if (DateTime.Now - Jump > TimeSpan.FromMilliseconds(500))
                    {
                        Task.Run(() => player.Jump(world));
                    }
                }
                else if(key == Controls.LookUp)
                {
                    player.CurrentDirection = 0;
                }
                else if (key == Controls.LookDown)
                {
                    player.CurrentDirection = 2;
                }
                else if(key == Controls.BreakBlock)
                {
                    world = player.BreakBlock(world);
                    Task.Run(() => player.Fall(world));
                    player.Inventory.Draw(player);
                }
                else if(key == Controls.Refresh)
                {
                    world.Refresh();
                }
                else if(key == Controls.Place)
                {
                    player.PlaceBlock(world);
                }
                else if(key == Controls.HotbarSlotOne)
                {
                    player.HotbarSlot = 0;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotTwo)
                {
                    player.HotbarSlot = 1;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotThree)
                {
                    player.HotbarSlot = 2;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotFour)
                {
                    player.HotbarSlot = 3;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotFive)
                {
                    player.HotbarSlot = 4;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotSix)
                {
                    player.HotbarSlot = 5;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotSeven)
                {
                    player.HotbarSlot = 6;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotEight)
                {
                    player.HotbarSlot = 7;
                    player.Inventory.Draw(player);
                }
                else if (key == Controls.HotbarSlotNine)
                {
                    player.HotbarSlot = 8;
                    player.Inventory.Draw(player);
                }
            }
        }
    }
}