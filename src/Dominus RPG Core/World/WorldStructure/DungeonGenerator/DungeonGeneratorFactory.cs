namespace Dominus_RPG_Core.World.WorldStructure.DungeonGenerator
{
    public class DungeonGeneratorFactory
    {
        public Map GenerateDungeon(DungeonTypes dungeonType)
        {
            Map dungeon = null;

            switch (dungeonType)
            {
                case DungeonTypes.Cave:
                    break;

                case DungeonTypes.Standard:
                    break;
            }

            return dungeon;
        }
    }
}