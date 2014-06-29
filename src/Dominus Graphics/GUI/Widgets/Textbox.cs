using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Text.RegularExpressions;

namespace Dominus_Graphics.GUI.Widgets
{
    public class Textbox : IWidget
    {
        private Texture2D _texture;
        private Vector2 _textPosition;
        private Rectangle _area;
        private Vector2 _position;
        private Texture2D _cursor;
        private Vector2 _cursorPosition;
        private bool _cursorVisible;
        private bool _visible;
        private int _nextCursorBlinkTime;
        private int _nextInputCheckTime;
        private string _text;
        private MouseStatus _mouseStatus;
        private Vector2 _textOffset;


        public int CharacterLimit { get; set; }

        public bool Visible
        {
            get { return _visible; }
        }

        public bool Active { get; set; }

        public Vector2 TextOffset
        {
            get
            {
                return _textOffset;
            }

            set
            {
                _textOffset = value;

                _cursorPosition = new Vector2((this.Position.X + this.TextOffset.X) + this.Font.MeasureString(_text).X, this.TextOffset.Y + (this.Position.Y + _cursor.Height / 2) + 4);
            }
        }

        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                if (_texture != null)
                    _area = new Rectangle((int)this.Position.X, (int)this.Position.Y, _texture.Width, _texture.Height);

                _cursorPosition = new Vector2((this.Position.X + this.TextOffset.X) + this.Font.MeasureString(_text).X, this.TextOffset.Y + (this.Position.Y + _cursor.Height / 2) + 4);
            }
        }

        public SpriteFont Font { get; set; }

        public Color ForeColor { get; set; }

        public char PasswordCharacter { get; set; }

        public Textbox(Texture2D texture, SpriteFont font, GraphicsDevice graphicsDevice, int characterLimit)
        {
            this.Font = font;
            this.CharacterLimit = characterLimit;

            _text = "";


            _cursor = new Texture2D(graphicsDevice, 1, (int)this.Font.MeasureString("|").Y / 2);


            this.Position = Vector2.Zero;

            _textPosition = Vector2.Zero;
            _texture = texture;

            _visible = true;
            _mouseStatus = MouseStatus.Idle;

            Color[] cursorData = new Color[_cursor.Width * _cursor.Height];

            for (int i = 0; i < cursorData.Length; ++i)
                cursorData[i] = Color.White;

            _cursor.SetData(cursorData);

        }

        private void CheckInput()
        {
            var characterPressed = InputHelper.GetHelper().GetCharacterPressed();

            if (characterPressed != '\0')
            {
                if (characterPressed == '\b')
                {
                    if (this.GetText().Length > 0)
                        this.SetText(this.GetText().Remove(this.GetText().Length - 1, 1));

                    return;
                }

                this.SetText(this.GetText() + characterPressed);
            }

        }

        public void Update(GameTime gameTime)
        {
            if (_visible == false) return;

            var mouseState = Mouse.GetState();

            if (_area.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && _mouseStatus != MouseStatus.MouseDown)
                {
                    _mouseStatus = MouseStatus.MouseDown;
                }
                else if (_mouseStatus == MouseStatus.MouseDown)
                {
                    this.Active = true;
                    _mouseStatus = MouseStatus.Hover;
                }
                else
                {
                    _mouseStatus = MouseStatus.Hover;
                }
            }
            else
            {
                _mouseStatus = MouseStatus.Idle;
            }

            if (this.Active)
            {
                if (_nextInputCheckTime < gameTime.TotalGameTime.TotalMilliseconds)
                {
                    this.CheckInput();
                    _nextInputCheckTime = (int)gameTime.TotalGameTime.TotalMilliseconds + 50;
                }

                if (_nextCursorBlinkTime < gameTime.TotalGameTime.TotalMilliseconds)
                {
                    _cursorVisible = !_cursorVisible;
                    _nextCursorBlinkTime = (int)gameTime.TotalGameTime.TotalMilliseconds + 500;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_visible) return;

            spriteBatch.Draw(_texture, this.Position, Color.White);
            spriteBatch.DrawString(this.Font, _text, _textPosition, this.ForeColor);

            if (_cursorVisible && this.Active)
                spriteBatch.Draw(_cursor, _cursorPosition, Color.White);
        }

        public void Show()
        {
            _visible = true;
        }

        public void Hide()
        {
            _visible = false;
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;

            _area = new Rectangle((int)this.Position.X, (int)this.Position.Y, _texture.Width, _texture.Height);
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }

        public void SetText(string text)
        {
            _text = text;

            if (this.PasswordCharacter != '\0')
                _text = Regex.Replace(_text, ".", this.PasswordCharacter.ToString());

            // Make sure the text doesn't go outside of the textbox.
            if (_text.Length > this.CharacterLimit)
            {
                _text = _text.Remove(_text.Length - 1, 1);
                return;
            }

            _textPosition = new Vector2()
            {
                X = this.Position.X + this.TextOffset.X,
                Y = this.Position.Y + this.TextOffset.Y
            };

            _cursorPosition = new Vector2((this.Position.X + this.TextOffset.X) + this.Font.MeasureString(_text).X + 5, this.TextOffset.Y + (this.Position.Y + _cursor.Height / 2) + 4);

        }

        public string GetText()
        {
            return _text;
        }


        public bool Contains(Point point)
        {
            throw new NotImplementedException();
        }
    }
}