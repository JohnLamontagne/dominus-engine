using Dominus_Core;
using Dominus_Core.Graphics;
using Dominus_RPG_Core.World.Entities.Stats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_RPG_Core.World.Entities
{
    /// <summary>
    /// An entity is defined as something that has sentient qualities, whether these qualities are artificial or player driven is up to
    /// each individual implementation.
    /// </summary>
    public interface IEntity : IGameObject
    {
        IEntityCombatHandler CombatHandler { get; }

        StatHandler Stats { get; }

        SpriteSheet SpriteSheet { get; }

        string Name { get; set; }

        bool Dead { get; }

        int Level { get; set; }

        Vector2 Range { get; set; }

        float Speed { get; set; }

        Vector2 Position { get; }

        event EventHandler Moved;
    }
}