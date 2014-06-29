using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Dominus_Core.Effects.Particles
{
    class TestParticleEmitter : ParticleEmitter
    {
        public TestParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position)
            : base(particleTextures, particleCount, position)
        {
        }

        protected override Particle CreateNewParticle()
        {
            var velocity = new Vector2(1f * (float)(_random.NextDouble() * 2 - 1), 1f * (float)(_random.NextDouble() * 2 - 1));
            var angle = 0f;
            var angularVelocity = 0.1f * (float)(_random.NextDouble() * 2);
            var lifeTime = _random.Next(100, 200);
            var texture = _particleTextures[_random.Next(0, _particleTextures.Length)];

            return new Particle(this.Position, velocity, angularVelocity, angle, texture, Color.White, (int)lifeTime);
        }
    }
}