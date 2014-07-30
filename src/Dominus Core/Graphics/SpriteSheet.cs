using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Core.Graphics
{
    public class SpriteSheet
    {
        private readonly Texture2D _texture;
        private Rectangle _frameRectangle;
        private Rectangle _destionationRect;
        private int _horizontalFrameIndex;
        private int _verticalFrameIndex;
        private int _verticalFrames;
        private int _horizontalFrames;
        private int _frameWidth;
        private int _frameHeight;

        public Vector2 Position
        {
            get
            {
                return new Vector2(_destionationRect.X, _destionationRect.Y);
            }
            set
            {
                _destionationRect = new Rectangle((int)value.X, (int)value.Y, _frameWidth, _frameHeight);
            }
        }

        public int HorizontalFrameIndex
        {
            get { return _horizontalFrameIndex; }
            set
            {
                if ((value < 0) || value >= _horizontalFrames) value = 0;

                _horizontalFrameIndex = value;

                _frameRectangle.X = _horizontalFrameIndex * _frameWidth;
            }
        }

        public int VerticalFrameIndex
        {
            get { return _verticalFrameIndex; }
            set
            {
                if ((value < 0) || value >= _verticalFrames) value = 0;

                _verticalFrameIndex = value;

                _frameRectangle.Y = _verticalFrameIndex * _frameWidth;
            }
        }

        public SpriteSheet(Texture2D texture, int horizontalFrames, int verticalFrames, int frameWidth, int frameHeight)
        {
            _texture = texture;

            _frameRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            _frameWidth = frameWidth;
            _frameHeight = frameHeight;
            _horizontalFrames = horizontalFrames;
            _verticalFrames = verticalFrames;

            this.Position = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destionationRect, _frameRectangle, Color.White);
        }
    }
}