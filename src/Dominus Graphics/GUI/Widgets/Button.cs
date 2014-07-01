using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Xml;

namespace Dominus_Graphics.GUI.Widgets
{
    public class Button : IWidget
    {
        private string _text;
        private Texture2D[] _textures;
        private MouseStatus _buttonState;
        private Vector2 _textPosition;
        private Rectangle _buttonArea;
        private Vector2 _position;
        private Vector2 _scale;

        /// <summary>
        /// Our widget's properties. We make all of these virtual so that our GUI editor may
        /// override them to provide the PropertyGrid object with necessary information.
        /// </summary>

        #region Properties

        public virtual bool Visible { get; set; }

        public virtual bool Active { get; set; }

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

        public event EventHandler ButtonClicked;

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

        public void Load(ContentManager content, SpriteFont font, XmlNode node)
        {
            _textures = new Texture2D[3];
            _textPosition = new Vector2();
            _position = Vector2.Zero;
            _buttonState = MouseStatus.Idle;

            this.IdleTexture = content.Load<Texture2D>(node.ChildNodes[0].InnerText);
            this.Font = font;
            this.Text = node.ChildNodes[2].InnerText ?? "";
            this.ForeColor = new Color().FromString(node.ChildNodes[3].InnerText);
            this.Visible = bool.Parse(node.ChildNodes[4].InnerText);
            this.Scale = new Vector2().FromString(node.ChildNodes[1].InnerText);
            this.Position = new Vector2().FromString(node.ChildNodes[0].InnerText);

        }
    }
}