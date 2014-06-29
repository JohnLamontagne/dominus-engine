using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominus_Core.Effects.Particles
{
    public class ColorSpreadParticleEmitter : SpreadParticleEmitter
    {
        public ColorSpreadParticleEmitter(Texture2D[] particleTextures, int particleCount, Vector2 position, uint xIntensity, uint yIntensity)
            : base(particleTextures, particleCount, position, xIntensity, yIntensity)
        {
            _xIntensity = xIntensity;
            _yIntensity = yIntensity;


        }

        protected override Particle CreateNewParticle()
        {
            var particle = base.CreateNewParticle();

            particle.Color = new Color(_random.Next(255), _random.Next(255), _random.Next(255));

            return particle;
        }
    }
}
