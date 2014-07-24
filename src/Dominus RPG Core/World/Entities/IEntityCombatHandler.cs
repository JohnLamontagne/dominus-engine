using System;

namespace Dominus_RPG_Core.World.Entities
{
    public interface IEntityCombatHandler
    {
        bool CanAttack(IEntity entity, IEntity victim);

        void Attack(IEntity entity, IEntity victim);

        void Attacked(IEntity entity, IEntity attacker, int damageDelt);

        int GetDamagePotential(IEntity entity);

        int GetDefencePotential(IEntity entity);
    }
}
