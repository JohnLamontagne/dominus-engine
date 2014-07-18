using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Core.Graphics.GUI.Widgets
{
    public class Label : IWidget
    {
        private string _name;

        public event EventHandler NameChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                string oldName = _name;
                _name = value;
                this.NameChanged.Invoke(this, new WidgetNameChangedEventArgs(_name, oldName));
            }
        }


        public bool Visible { get; set; }

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

            this.Visible = true;
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

        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public void Load(Microsoft.Xna.Framework.Content.ContentManager content, System.Xml.XmlNode node)
        {
            this.Text = node.ChildNodes[0].InnerText ?? "";
            this.ForeColor = new Color().FromString(node.ChildNodes[1].InnerText);
            this.Visible = bool.Parse(node.ChildNodes[2].InnerText);
            this.Position = new Vector2().FromString(node.ChildNodes[3].InnerText);
        }


        public void Save(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }



    }
}