using System;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    class Strength : IStat<int>
    {
        private int _strengthPoints;

        public int GetPoints()
        {
            return _strengthPoints;
        }

        public void SetPoints(int points)
        {
            _strengthPoints = points;
        }


        public void AddPoints(int points)
        {
            throw new NotImplementedException();
        }

        public void RemovePoints(int points)
        {
            throw new NotImplementedException();
        }
    }
}
