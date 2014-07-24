using Dominus_Core.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace Dominus_RPG_Core.World.WorldStructure.DungeonGenerator
{
    public abstract class DungeonGenerator
    {
        protected Map map;

        public DungeonGenerator(Random random)
        {
            var size = new Point(random.Next(Constants.MIN_MAP_X, Constants.MAX_MAP_X),
                random.Next(Constants.MIN_MAP_Y, Constants.MAX_MAP_Y));

           // map = new Map(size);
        }

        public virtual Map Generate()
        {

            return map;
        }

        protected abstract void GenerateStructure();

        protected abstract void GenerateWalls();

        protected abstract void GenerateDetails();
    }
}