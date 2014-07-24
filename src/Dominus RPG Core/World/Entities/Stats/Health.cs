
namespace Dominus_RPG_Core.World.Entities.Stats
{
    public class Health : IStat<float>
    {
        private float _healthPoints;

        public float GetPoints()
        {
            return _healthPoints;
        }

        public void SetPoints(float points)
        {
            _healthPoints = points;
        }


        public void AddPoints(float points)
        {
            _healthPoints += points;
        }

        public void RemovePoints(float points)
        {
            _healthPoints -= points;
        }
    }
}
