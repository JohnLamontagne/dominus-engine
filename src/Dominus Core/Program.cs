using System;
using System.Threading;

namespace Dominus_Core
{
#if WINDOWS || XBOX

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            new Thread(() =>
            {
                using (DominusEngineGame game = new DominusEngineGame())
                {
                    game.Run();
                }
            }).Start();
        }
    }

#endif
}