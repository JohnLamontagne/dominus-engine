using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Dominus_Core.Effects.Particles
{
    public class ParticleEmitter : IGameObject
    {
        protected List<Particle> _particles;
        protected Texture2D[] _particleTextures;
        protected Random _random;
        protected int _particleCount;


        public Vector2 Position { get; set; }

        public int ParticleCount { get { return _particleCount; } }

        public bool Emitting { get; set; }


        public ParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position)
        {
            _particles = new List<Particle>();
            _particleTextures = particleTextures;
            _random = new Random();
            _particleCount = particleCount;
            this.Position = position;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!this.Emitting) return;

            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update(gameTime);

                if (_particles[i].IsDead)
                {
                    _particles.RemoveAt(i);

                    _particles.Add(this.CreateNewParticle());
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!this.Emitting) return;

            for (int i = 0; i < _particles.Count; i++)
            {

                _particles[i].Draw(spriteBatch);
            }
        }

        public virtual void Initialize()
        {
            for (int i = 0; i < this.ParticleCount; i++)
            {
                _particles.Add(this.CreateNewParticle());
            }
        }

        protected virtual Particle CreateNewParticle()
        {
            var velocity = new Vector2((float)_random.NextDouble() * 2 - 1, (float)_random.NextDouble() * 2 - 1);
            var angle = 0f;
            var angularVelocity = 0.1f * (float)(_random.NextDouble() * 2 - 1);
            var lifeTime = _random.Next(100, 1000);
            var texture = _particleTextures[_random.Next(0, _particleTextures.Length)];

            return new Particle(this.Position, velocity, angularVelocity, angle, texture, Color.White, (int)lifeTime);
        }
    }
}