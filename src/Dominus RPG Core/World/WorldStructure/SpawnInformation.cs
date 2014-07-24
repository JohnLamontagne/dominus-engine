using Dominus_Core;
using Dominus_Core.Graphics.Effects.Particles;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml;
using TiledSharp;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class SpawnInformation
    {
        private Dictionary<string, IGameObject> _gameObjects;

        public SpawnInformation()
        {
            _gameObjects = new Dictionary<string, IGameObject>();
        }

        public void LoadGameObjects(TmxMap tmxMap, ContentManager content)
        {
            foreach (var tmxObjectGroup in tmxMap.ObjectGroups)
            {
                foreach (var tmxObject in tmxObjectGroup.Objects)
                {
                    if (tmxObject.Type == "Particle Emitter")
                    {
                        IGameObject gameObject = this.LoadParticleEmitter(tmxObject, content);
                        _gameObjects.Add(tmxObject.Name, gameObject);
                    }
                }
            }
        }

        private ParticleEmitter LoadParticleEmitter(TmxObjectGroup.TmxObject tmxObject, ContentManager content)
        {
            ParticleEmitter particleEmitter = null;

            Texture2D[] particleTextures = this.LoadParticleTextures(tmxObject.Properties["TexturesFile"], content);
            int particleCount = int.Parse(tmxObject.Properties["ParticleCount"]);
            Vector2 position = new Vector2(tmxObject.X, tmxObject.Y);

            // Get the emitter type.
            switch (tmxObject.Properties["EmitterType"].ToLower())
            {
                case "default":
                    particleEmitter = new ParticleEmitter(particleTextures, particleCount, position);
                    break;

                case "spread":
                    uint xIntensity = uint.Parse(tmxObject.Properties["XIntensity"]);
                    uint yIntensity = uint.Parse(tmxObject.Properties["YIntensity"]);

                    particleEmitter = new SpreadParticleEmitter(particleTextures, particleCount, position, xIntensity, yIntensity);
                    break;

                case "directional":

                    bool positiveXVelocity = bool.Parse(tmxObject.Properties["PositiveXVelocity"]);
                    bool positiveYVelocity = bool.Parse(tmxObject.Properties["PositiveYVelocity"]);

                    particleEmitter = new DirectionalParticleEmitter(particleTextures, particleCount, position, positiveXVelocity, positiveYVelocity);
                    break;
            }

            particleEmitter.Initialize();

            particleEmitter.Emitting = true;

            return particleEmitter;
        }

        private Texture2D[] LoadParticleTextures(string particleTextureFile, ContentManager content)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(particleTextureFile);

            XmlNodeList nodes = xmlDoc.SelectNodes("Textures/Texture");

            var textures = new Texture2D[nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                textures[i] = ContentManagerUtilities.LoadTexture2D(content, nodes[i].Attributes["filepath"].Value);
            }

            return textures;
        }

        public Dictionary<string, IGameObject> GetGameObjects()
        {
            return _gameObjects;
        }
    }
}