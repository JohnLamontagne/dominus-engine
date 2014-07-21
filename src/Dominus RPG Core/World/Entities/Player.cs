using Dominus_Core.Utilities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Dominus_RPG_Core.World.Entities
{
    public class Player : IEntity
    {
        private Vector2 _position;
        private Texture2D _sprite;

        public Texture2D Sprite
        {
            get { return _sprite; }
        }

        public string Name { get; set; }

        public int Level { get; set; }

        public float Speed { get; set; }

        public Vector2 Position { get { return _position; } private set { _position = value; } }

        public Camera Camera { get; private set; }

        public Player(Texture2D playerTexture, Camera camera)
        {
            _sprite = playerTexture;
            this.Camera = camera;
        }

        private void CheckInput()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
                this.Move(new Vector2(0, -this.Speed));

            if (keyboardState.IsKeyDown(Keys.Down))
                this.Move(new Vector2(0, this.Speed));

            if (keyboardState.IsKeyDown(Keys.Left))
                this.Move(new Vector2(-this.Speed, 0));

            if (keyboardState.IsKeyDown(Keys.Right))
                this.Move(new Vector2(this.Speed, 0));
        }

        private void Move(Vector2 distance)
        {
            this.Position += distance;
        }

        public void Update(GameTime gameTime)
        {
            this.CheckInput();

            // Update the camera.
            if (this.Camera.Position.X < this.Position.X)
            {
                float delta = Math.Abs(this.Camera.Position.X - this.Position.X);

                this.Camera.Move(new Vector2(.05f * delta, 0));
            }
            else if (this.Camera.Position.X > this.Position.X)
            {
                float delta = Math.Abs(this.Camera.Position.X - this.Position.X);

                this.Camera.Move(new Vector2(.05f * -delta, 0));
            }

            if (this.Camera.Position.Y < this.Position.Y)
            {
                float delta = Math.Abs(this.Camera.Position.Y - this.Position.Y);

                this.Camera.Move(new Vector2(0, .05f * delta));
            }
            else if (this.Camera.Position.Y > this.Position.Y)
            {
                float delta = Math.Abs(this.Camera.Position.Y - this.Position.Y);

                this.Camera.Move(new Vector2(0, .05f * -delta));

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_sprite, this.Position, Color.Navy);
        }

        public IEntityCombatHandler CombatHandler
        {
            get { throw new NotImplementedException(); }
        }


        public int Health
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public Vector2 Range
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}