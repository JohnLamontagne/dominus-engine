using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Graphics.GUI
{
    public interface IWidget
    {
        bool Visible { get; }

        bool Active { get; set; }

        Vector2 Position { get; set; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        bool Contains(Point point);

    }
}