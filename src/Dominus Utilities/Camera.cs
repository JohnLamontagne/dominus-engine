using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Utilities
{
    public class Camera
    {
        private float _zoom;
        private Vector2 _position;
        private float _rotation;

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = _zoom < 0.1f ? 0.1f : value;
            }
        }

        public float Rotation { get { return _rotation; } set { _rotation = value; } }

        public Vector2 Position { get { return _position; } set { _position = value; } }

        public Camera()
        {
            _zoom = 1f;
            _rotation = 0f;
            _position = Vector2.Zero;
        }

        public void Move(Vector2 distance)
        {
            _position += distance;
        }

        public void Rotate(float amount)
        {
            _rotation += amount;

            if (_rotation > 360) _rotation -= 360;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            return Matrix.CreateTranslation(new Vector3(-this.Position.X * this.Zoom, -this.Position.Y * this.Zoom, 0)) *
                Matrix.CreateRotationZ(this.Rotation) *
                Matrix.CreateScale(new Vector3(this.Zoom, this.Zoom, 1));

        }

        public void Unload()
        {

        }
    }
}