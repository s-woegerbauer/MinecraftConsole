namespace MinecraftConsole
{
    public class Program
    {
        public static bool IsChangingTexture = false;
        static void Main(string[] args)
        {
            Console.Clear();
            Thread.Sleep(100);
            Console.CursorVisible = false;
            MaximizeWindow.Go();
            Player player = new Player("Player1");
            World world = World.GenerateNewWorld(59, player);
            world.Draw();
            while(true) 
            {
                ConsoleKey key = Console.ReadKey(intercept: true).Key;

                if(key == Controls.MoveLeft)
                {
                    Task.Run(() => player.Move(-1, world));
                }
                else if (key == Controls.MoveRight)
                {
                    Task.Run(() => player.Move(1, world));
                }
                else if (key == Controls.Jump)
                {
                    Task.Run(() => player.Jump(world));
                }
            }
        }
    }
}