using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Core.World.Entities
{
    public interface IEntity
    {
        Texture2D Sprite { get; }

        string Name { get; set; }

        int Level { get; set; }

        float Speed { get; set; }

        Vector2 Position { get; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);
    }
}