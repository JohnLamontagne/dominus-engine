using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Xml;

namespace Dominus_Core.Graphics.GUI.Widgets
{
    public interface IWidget
    {
        event EventHandler NameChanged;

        string Name { get; set; }

        bool Visible { get; }

        bool Active { get; set; }

        Vector2 Position { get; set; }

        void Update(GameTime gameTime);

        void Draw(SpriteBatch spriteBatch);

        bool Contains(Point point);

        void Load(ContentManager content, XmlNode node);

        void Save(XmlWriter writer);
    }
}