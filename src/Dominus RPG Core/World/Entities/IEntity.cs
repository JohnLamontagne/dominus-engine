using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Dominus_RPG_Core.World.Entities
{
    public interface IEntity
    {
        IEntityCombatHandler CombatHandler { get; }

        Texture2D Sprite { get; }

        string Name { get; set; }

        int Level { get; set; }

        int Health { get; set; }

        Vector2 Range { get; set; }

        float Speed { get; set; }

        Vector2 Position { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}