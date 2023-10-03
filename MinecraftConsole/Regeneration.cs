using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public static class Regeneration
    {
        private static bool NaturalRegeneration = true;
        public static void Go(int delayInMs)
        {
            while (true)
            {
                if (NaturalRegeneration)
                {
                    Task.Delay(delayInMs).Wait();
                    Program.Player.HealthBar.currentHealth += 0.5;
                    if (Program.Player.HealthBar.currentHealth > Program.Player.HealthBar.maxHealth)
                    {
                        Program.Player.HealthBar.currentHealth = Program.Player.HealthBar.maxHealth;
                    }

                    Program.Player.HealthBar.Draw();
                }
            }
        }

        public static void Disable()
        {
            NaturalRegeneration = false;
        }

        public static void Enable()
        {
            NaturalRegeneration = true;
        }
    }
}