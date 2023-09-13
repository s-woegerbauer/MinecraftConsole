namespace MinecraftConsole
{
    public class Program
    {
        public static bool IsChangingTexture = false;
        public static bool IsAlreadyJumping = false;
        public static DateTime Jump = DateTime.Now;
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(400);
            Console.CursorVisible = false;
            MaximizeWindow.Go();
            Player player = new Player("Player1");
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
                }
                else if (key == Controls.HotbarSlotTwo)
                {
                    player.HotbarSlot = 1;
                }
                else if (key == Controls.HotbarSlotThree)
                {
                    player.HotbarSlot = 2;
                }
            }
        }
    }
}