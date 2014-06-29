using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Dominus_Core.Effects.Particles
{
    public class DirectionalParticleEmitter : ParticleEmitter
    {
        protected bool _positiveXVelocity;
        protected bool _positiveYVelocity;

        public DirectionalParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position, bool positiveXVelocity, bool positiveYVelocity)
            : base(particleTextures, particleCount, position)
        {
            _positiveXVelocity = positiveXVelocity;
            _positiveYVelocity = positiveYVelocity;
        }

        public override void Initialize()
        {
            base.Initialize();

            foreach (var particle in _particles)
                particle.Drawable = false;
        }

        protected override Particle CreateNewParticle()
        {
            var velocity = new Vector2((float)_random.NextDouble() * 2f, (float)_random.NextDouble() * 2f);
            var angle = 0f;
            var angularVelocity = 0.1f * (float)(_random.NextDouble() * 2);
            var lifeTime = _random.Next(100, 200);
            var texture = _particleTextures[_random.Next(0, _particleTextures.Length)];


            velocity.X *= _positiveXVelocity ? 1 : -1;

            // Y is backwards because the top of the screen is 0, and the user thinks of up as positive.
            velocity.Y *= _positiveYVelocity ? -1 : 1;

            return new Particle(this.Position, velocity, angularVelocity, angle, texture, Color.White, (int)lifeTime);
        }
    }
}