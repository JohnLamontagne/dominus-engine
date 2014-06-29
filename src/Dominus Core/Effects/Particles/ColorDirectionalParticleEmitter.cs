using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_Core.Effects.Particles
{
    public class ColorDirectionalParticleEmitter : DirectionalParticleEmitter
    {
        public ColorDirectionalParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position, bool positiveXVelocity, bool positiveYVelocity)
            : base(particleTextures, particleCount, position, positiveXVelocity, positiveYVelocity)
        {
            _positiveXVelocity = positiveXVelocity;
            _positiveYVelocity = positiveYVelocity;
        }

        protected override Particle CreateNewParticle()
        {
            var particle = base.CreateNewParticle();

            particle.Color = new Color(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));

            return particle;
        }
    }
}