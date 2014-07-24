using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_Core.Graphics.Effects.Particles
{
    public class SpreadParticleEmitter : ParticleEmitter
    {
        protected uint _xIntensity;
        protected uint _yIntensity;

        public SpreadParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position, uint xIntensity, uint yIntensity)
            : base(particleTextures, particleCount, position)
        {
            _xIntensity = xIntensity;
            _yIntensity = yIntensity;
        }

        protected override Particle CreateNewParticle()
        {
            int randomXScalar = _xIntensity > 0 ? _random.Next(1, (int)_xIntensity) * (_random.NextDouble() < .5 ? -1 : 1) : 0;

            int randomYScalar = _yIntensity > 0 ? _random.Next(1, (int)_yIntensity) * (_random.NextDouble() < .5 ? -1 : 1) : 0;

            var velocity = new Vector2((float)_random.NextDouble() * randomXScalar, (float)_random.NextDouble() * randomYScalar);
            var angle = 0f;
            var angularVelocity = 0.1f * (float)(_random.NextDouble() * 2);
            var lifeTime = _random.Next(100, 200);
            var texture = _particleTextures[_random.Next(0, _particleTextures.Length)];

            return new Particle(this.Position, velocity, angularVelocity, angle, texture, Color.White, (int)lifeTime);
        }
    }
}