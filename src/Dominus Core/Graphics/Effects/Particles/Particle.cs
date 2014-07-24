using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Dominus_Core.Graphics.Effects.Particles
{
    public class Particle
    {
        private Texture2D _sprite;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _angularVelocity;
        private int _lifeTime;
        private float _angle;
        private Color _color;

        private Vector2 _origin;

        public bool IsDead { get { return _lifeTime <= 0; } }

        public Color Color { get { return _color; } set { _color = value; } }

        public bool Drawable { get; set; }

        public Particle(Vector2 initialPosition, Vector2 velocity, float angularVelocity, float angle, Texture2D sprite, Color color, int lifeTime)
        {
            _sprite = sprite;
            _position = initialPosition;
            _lifeTime = lifeTime;
            _velocity = velocity;
            _angularVelocity = angularVelocity;
            _angle = angle;
            _color = color;

            _origin = new Vector2(_sprite.Width / 2, _sprite.Height / 2);

            this.Drawable = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Drawable) return;

            spriteBatch.Draw(_sprite, _position, null, _color, _angle, _origin, 1f, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            _lifeTime--;

            _position += _velocity;
            _angle += _angularVelocity;
        }
    }
}