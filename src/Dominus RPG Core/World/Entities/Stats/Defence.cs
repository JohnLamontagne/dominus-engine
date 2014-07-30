using System;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    public class Defence : IStat<int>
    {
        private int _defencePoints;
        private int _maxPoints;

        public int MaxPoints { get { return _maxPoints; } }

        public Defence(int maxPoints)
        {
            _maxPoints = maxPoints;
        }

        public int GetPoints()
        {
            return _defencePoints;
        }

        public void SetPoints(int points)
        {
            if (points <= _maxPoints)
                _defencePoints = points;
        }

        public void AddPoints(int points)
        {
            if (_defencePoints + points <= _maxPoints)
                _defencePoints += points;
        }

        public void RemovePoints(int points)
        {
            throw new NotImplementedException();
        }
    }
}