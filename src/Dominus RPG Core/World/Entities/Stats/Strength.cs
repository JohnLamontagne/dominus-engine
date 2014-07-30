using System;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    internal class Strength : IStat<int>
    {
        private int _strengthPoints;
        private int _maxPoints;

        public Strength(int maxPoints)
        {
            _maxPoints = maxPoints;
        }

        public int MaxPoints
        {
            get { return _maxPoints; }
        }

        public int GetPoints()
        {
            return _strengthPoints;
        }

        public void SetPoints(int points)
        {
            if (points <= _maxPoints)
                _strengthPoints = points;
        }

        public void AddPoints(int points)
        {
            if (_strengthPoints + points <= _maxPoints)
                _strengthPoints += points;
        }

        public void RemovePoints(int points)
        {
            throw new NotImplementedException();
        }
    }
}