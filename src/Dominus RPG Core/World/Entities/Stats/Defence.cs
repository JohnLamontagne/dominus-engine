using System;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    public class Defence : IStat<int>
    {
        private int _defencePoints;

        public int GetPoints()
        {
            return _defencePoints;
        }

        public void SetPoints(int points)
        {
            _defencePoints = points;
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
