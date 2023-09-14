using Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftConsole
{
    public class HeartBar
    {
        public double currentHealth { get; set; }
        public double maxHealth { get; set; }

        public HeartBar() 
        {
            currentHealth = 10.0;
            maxHealth = 10.0;
        }

        public void Draw()
        {
            for(int i = 0; i < maxHealth; i++)
            {
                if(currentHealth >= i)
                {
                    WinAPI.DrawImage("\\Textures\\Heart.jpg", 4, 2, 2 + i * 4, 40);
                }
                else if(currentHealth + 1 > i)
                {
                    WinAPI.DrawImage("\\Textures\\Half_Heart.jpg", 4, 2, 2 + i * 4, 40);
                }
                else
                {
                    WinAPI.DrawImage("\\Textures\\Lost_Heart.jpg", 4, 2, 2 + i * 4, 40);
                }
            }
        }
    }
}
