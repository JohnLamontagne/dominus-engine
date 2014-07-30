using Dominus_Core;

namespace Dominus_RPG_Core
{
    public abstract class DominusRPGGame : DominusGame
    {
        public static RPGGameProperties Properties { get; set; }

        public DominusRPGGame(RPGGameProperties properties)
        {
            DominusRPGGame.Properties = properties;
        }
    }
}