namespace Dominus_RPG_Core.World.Entities.Stats
{
    public class Health : IStat<float>
    {
        private float _healthPoints;
        private float _maxPoints;

        public float MaxPoints { get { return _maxPoints; } }

        public Health(float maxPoints)
        {
            _maxPoints = maxPoints;
        }

        public float GetPoints()
        {
            return _healthPoints;
        }

        public void SetPoints(float points)
        {
            if (points <= _maxPoints)
                _healthPoints = points;
        }

        public void AddPoints(float points)
        {
            if (_healthPoints + points <= _maxPoints)
                _healthPoints += points;
        }

        public void RemovePoints(float points)
        {
            _healthPoints -= points;
        }
    }
}