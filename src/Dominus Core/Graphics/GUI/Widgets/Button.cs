using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Xml;

namespace Dominus_Core.Graphics.GUI.Widgets
{
    public class Button : IWidget
    {
        #region Fields
        private string _name;
        private string _text;
        private Texture2D[] _textures;
        private MouseStatus _buttonState;
        private Vector2 _textPosition;
        private Rectangle _buttonArea;
        private Vector2 _position;
        private Vector2 _scale;
        #endregion

        #region Properties

        public virtual string Name
        {
            get { return _name; }
            set
            {
                string oldName = _name;
                _name = value;

                if (this.NameChanged != null)
                    this.NameChanged.Invoke(this, new WidgetNameChangedEventArgs(_name, oldName));
            }
        }

        public virtual bool Visible { get; set; }

        public virtual bool Active { get; set; }

        public Vector2 TrueDimensions
        {
            get
            {
                if (this.IdleTexture != null)
                    return new Vector2(this.IdleTexture.Width * this.Scale.X, this.IdleTexture.Height * this.Scale.Y);
                else
                    return new Vector2(0, 0);
            }
        }

        public virtual string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;

                if (_textures[0] != null)
                {
                    _textPosition = new Vector2()
                    {
                        X = this.Position.X + (_textures[(int)_buttonState].Width * this.Scale.X) / 2 - (this.Font.MeasureString(this.Text).X / 2),
                        Y = this.Position.Y + (_textures[(int)_buttonState].Height * this.Scale.Y) / 2 - (this.Font.MeasureString(this.Text).Y / 2)
                    };
                }
            }
        }

        public virtual Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                if (_textures[0] != null)
                {
                    _buttonArea = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(_textures[0].Width * this.Scale.X), (int)(_textures[0].Height * this.Scale.Y));

                    _textPosition = new Vector2()
                    {
                        X = this.Position.X + (_textures[(int)_buttonState].Width * this.Scale.X) / 2 - (this.Font.MeasureString(this.Text).X / 2),
                        Y = this.Position.Y + (_textures[(int)_buttonState].Height * this.Scale.Y) / 2 - (this.Font.MeasureString(this.Text).Y / 2)
                    };
                }
            }
        }

        public virtual SpriteFont Font { get; set; }

        public virtual Vector2 Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;

                if (_textures[0] != null)
                {
                    _buttonArea = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(_textures[0].Width * this.Scale.X), (int)(_textures[0].Height * this.Scale.Y));

                    _textPosition = new Vector2()
                    {
                        X = this.Position.X + (_textures[(int)_buttonState].Width * this.Scale.X) / 2 - (this.Font.MeasureString(this.Text).X / 2),
                        Y = this.Position.Y + (_textures[(int)_buttonState].Height * this.Scale.Y) / 2 - (this.Font.MeasureString(this.Text).Y / 2)
                    };
                }
            }
        }

        public virtual Color ForeColor { get; set; }
        public virtual Texture2D IdleTexture
        {
            get
            {
                return _textures[0];
            }
            set
            {
                _textures[0] = value;

                _buttonState = MouseStatus.Idle;
                _buttonArea = new Rectangle((int)this.Position.X, (int)this.Position.Y, (int)(_textures[0].Width * this.Scale.X), (int)(_textures[0].Height * this.Scale.Y));

                _textPosition = new Vector2()
                {
                    X = this.Position.X + (_textures[(int)_buttonState].Width * this.Scale.X) / 2 - (this.Font.MeasureString(this.Text).X / 2),
                    Y = this.Position.Y + (_textures[(int)_buttonState].Height * this.Scale.Y) / 2 - (this.Font.MeasureString(this.Text).Y / 2)
                };
            }
        }

        public virtual Texture2D HoverTexture
        {
            get
            {
                return _textures[1];
            }

            set
            {
                _textures[1] = value;
            }
        }

        public virtual Texture2D MouseDownTexture
        {
            get
            {
                return _textures[2];
            }

            set
            {
                _textures[2] = value;
            }
        }

        #endregion Properties

        #region Event Handlers
        public event EventHandler ButtonClicked;
        public event EventHandler NameChanged;
        #endregion

        /// <summary>
        /// Only used for loading the button from XML.
        /// </summary>
        internal Button()
        {

        }

        public Button(Texture2D idleTexture, SpriteFont font)
        {
            _textures = new Texture2D[3];
            _textPosition = new Vector2();
            _position = Vector2.Zero;
            _textures[0] = idleTexture;
            _buttonState = MouseStatus.Idle;

            this.Font = font;
            this.Text = "";
            this.ForeColor = Color.Black;
            this.Visible = true;
            this.Scale = new Vector2(1, 1);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Visible) return;

            var mouseState = Mouse.GetState();

            if (_buttonArea.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _buttonState != MouseStatus.MouseDown)
                {
                    _buttonState = MouseStatus.MouseDown;

                    this.Active = true;

                    return;
                }
                else if (mouseState.LeftButton == ButtonState.Released && _buttonState == MouseStatus.MouseDown)
                {
                    if (this.ButtonClicked != null) this.ButtonClicked.Invoke(this, new EventArgs());

                    _buttonState = MouseStatus.Hover;

                    return;
                }
                else if (_buttonState != MouseStatus.MouseDown)
                    _buttonState = MouseStatus.Hover;
            }
            else
            {
                _buttonState = MouseStatus.Idle;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Visible) return;

            switch (_buttonState)
            {
                case MouseStatus.Idle:
                    if (_textures[0] != null)
                        spriteBatch.Draw(_textures[0], this.Position, null, Color.White, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                    break;

                case MouseStatus.Hover:
                    if (_textures[1] != null)
                        spriteBatch.Draw(_textures[1], this.Position, null, Color.White, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                    else
                        spriteBatch.Draw(_textures[0], this.Position, null, Color.White, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                    break;

                case MouseStatus.MouseDown:
                    if (_textures[2] != null)
                        spriteBatch.Draw(_textures[2], this.Position, null, Color.White, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                    else
                        spriteBatch.Draw(_textures[0], this.Position, null, Color.White, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f);
                    break;
            }

            if (_textures[0] != null)
                spriteBatch.DrawString(this.Font, this.Text, _textPosition, this.ForeColor);
        }

        public bool Contains(Point point)
        {
            return _buttonArea.Contains(point);
        }

        public void Load(ContentManager content, XmlNode node)
        {
            _textures = new Texture2D[3];
            _textPosition = new Vector2();
            _position = Vector2.Zero;
            _buttonState = MouseStatus.Idle;

            this.Font = content.Load<SpriteFont>("menufont");
            this.Text = node.ChildNodes[2].InnerText ?? "";
            this.IdleTexture = ContentManagerUtilities.LoadTexture2D(content, node.ChildNodes[5].ChildNodes[1].InnerText);
            this.HoverTexture = ContentManagerUtilities.LoadTexture2D(content, node.ChildNodes[5].ChildNodes[0].InnerText);
            this.ForeColor = new Color().FromString(node.ChildNodes[3].InnerText);
            this.Visible = bool.Parse(node.ChildNodes[4].InnerText);
            this.Scale = new Vector2().FromString(node.ChildNodes[1].InnerText);
            this.Position = new Vector2().FromString(node.ChildNodes[0].InnerText);
        }

        public void Save(XmlWriter writer)
        {
            writer.WriteElementString("Position", this.Position.ToString());
            writer.WriteElementString("Scale", this.Scale.ToString());
            writer.WriteElementString("Text", this.Text);
            writer.WriteElementString("ForeColor", this.ForeColor.ToString());
            writer.WriteElementString("Visible", this.Visible.ToString());
            //xmlWriter.WriteElementString("Font", this.Font.)

            // Write the texture information.
            writer.WriteStartElement("Textures");
            writer.WriteElementString("HoverTexture", this.HoverTexture == null ? "naught" : this.HoverTexture.Name);
            writer.WriteElementString("IdleTexture", this.IdleTexture == null ? "naught" : this.IdleTexture.Name);
            writer.WriteElementString("MouseDownTexture", this.MouseDownTexture == null ? "naught" : this.MouseDownTexture.Name);
            writer.WriteEndElement();
        }

    }
}