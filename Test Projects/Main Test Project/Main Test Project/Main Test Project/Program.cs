using Dominus_RPG_Core;
using System;

namespace Main_Test_Project
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            RPGGameProperties properties = new RPGGameProperties();

            properties.PlayerTexturePath = @"C:\Users\General\Desktop\char.png";

            using (var game = new Game(properties))
            {
                game.Run();
            }
        }
    }

#endif
}