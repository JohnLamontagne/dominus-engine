using System;

namespace Dominus_RPG_Core.World.Entities
{
    /// <summary>
    /// Default combat handler for the RPG Core.
    /// Warning: The entity must have the following stats in order to use this combat handler: Name: Health - Type: float, 
    /// Name: Defence - Type: int, and Name: Strength - Type: int.
    /// </summary>
    public class DefaultCombatHandler : IEntityCombatHandler
    {
        private Random _random;

        public DefaultCombatHandler()
        {
            _random = new Random();
        }

        public bool CanAttack(IEntity attacker, IEntity victim)
        {   
            // Calculate the distance between the two entities.
            float deltaX = Math.Abs(attacker.Position.X - victim.Position.X);
            float deltaY = Math.Abs(attacker.Position.Y - victim.Position.Y);

            return (deltaX <= attacker.Range.X && deltaY <= attacker.Range.Y);    
        }

        public void Attack(IEntity entity, IEntity victim)
        {
            // Calculate the attacker's damage potential.
            int potentialDamage = entity.CombatHandler.GetDamagePotential(entity);

            // Calculate the victim's defence potential.
            int potentialDefence = victim.CombatHandler.GetDefencePotential(victim);

            int actualDamage = potentialDamage - potentialDefence;

            victim.Stats.GetStat<float>("Health").RemovePoints(actualDamage);

            // Let the victim's combat handler take care of any additional processing.
            victim.CombatHandler.Attacked(victim, entity, actualDamage);
        }

        public void Attacked(IEntity attacker, IEntity entity, int damageDelt)
        {
            
        }

        public int GetDamagePotential(IEntity entity)
        {
            return (int)(entity.Stats.GetStat<int>("Strength").GetPoints() * _random.NextDouble());
        }

        public int GetDefencePotential(IEntity entity)
        {
            return (int)(entity.Stats.GetStat<int>("Defence").GetPoints() * _random.NextDouble());
        }
    }
}
