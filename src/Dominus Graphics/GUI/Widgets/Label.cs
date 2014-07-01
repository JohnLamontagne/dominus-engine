using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Graphics.GUI.Widgets
{
    public class Label : IWidget
    {
        private bool _visible;

        public bool Visible
        {
            get { return _visible; }
        }

        public string Text { get; set; }

        public bool Active { get; set; }

        public Vector2 Position { get; set; }

        public SpriteFont Font { get; set; }

        public Color ForeColor { get; set; }

        public Label(SpriteFont font)
        {
            this.Font = font;

            this.Position = Vector2.Zero;

            this.Text = "";

            this.Show();
        }

        public void Update(GameTime gameTime)
        {
            // Nothing to update.
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Visible)
                spriteBatch.DrawString(this.Font, this.Text, this.Position, this.ForeColor);
        }

        public void Show()
        {
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }


        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }






        public void Load(Microsoft.Xna.Framework.Content.ContentManager content, SpriteFont font, System.Xml.XmlNode node)
        {
            throw new NotImplementedException();
        }
    }
}