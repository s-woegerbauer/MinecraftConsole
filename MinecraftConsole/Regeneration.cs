using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public static class Regeneration
    {
        public static void Go(int delayInMs)
        {
            while (true)
            {
                Task.Delay(delayInMs).Wait();
                Program.player.HealthBar.currentHealth += 0.5;
                if (Program.player.HealthBar.currentHealth > Program.player.HealthBar.maxHealth)
                {
                    Program.player.HealthBar.currentHealth = Program.player.HealthBar.maxHealth;
                }

                Program.player.HealthBar.Draw();
            }
        }
    }
}
