using System;

namespace Dominus_RPG_Core.World.Entities
{
    public class DefaultCombatHandler : IEntityCombatHandler
    {
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
            int potentialDamage = 0;

            // Calculate the victim's defence potential.
            int victimDefence = 0;

            int actualDamage= potentialDamage - victimDefence;

            victim.Health -= actualDamage;

            // Let the victim's combat handler take care of any additional processing.
            victim.CombatHandler.Attacked(victim, entity, actualDamage);
        }

        public void Attacked(IEntity attacker, IEntity entity, int damageDelt)
        {
            
        }
    }
}
