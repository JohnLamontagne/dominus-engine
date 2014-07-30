using Dominus_Core;
using Dominus_Core.Graphics.Effects.Particles;
using Dominus_RPG_Core.World.Entities;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml;
using TiledSharp;

namespace Dominus_RPG_Core.World.WorldStructure
{
    public class MapObjectLoader
    {
        public MapObjectLoader()
        {
        }

        public Layer[] LoadObjectLayers(TmxMap tmxMap, ContentManager content)
        {
            var layers = new Layer[tmxMap.ObjectGroups.Count];

            for (int i = 0; i < tmxMap.ObjectGroups.Count; i++)
            {
                OrderedDictionary<string, IGameObject> _gameObjects = new OrderedDictionary<string, IGameObject>();

                foreach (var tmxObject in tmxMap.ObjectGroups[i].Objects)
                {
                    if (tmxObject.Type == "Particle Emitter")
                    {
                        IGameObject gameObject = this.LoadParticleEmitter(tmxObject, content);
                        _gameObjects.Add(tmxObject.Name, gameObject);
                    }
                    else if (tmxObject.Type == "NPC Spawner")
                    {
                        IGameObject gameObject = this.LoadNpcSpawner(tmxObject, _gameObjects, content);
                        _gameObjects.Add(tmxObject.Name, gameObject);
                    }
                }

                layers[i] = new Layer(_gameObjects, tmxMap.ObjectGroups[i].ZOrder);
            }

            return layers;
        }

        private ParticleEmitter LoadParticleEmitter(TmxObjectGroup.TmxObject tmxObject, ContentManager content)
        {
            ParticleEmitter particleEmitter = null;

            Texture2D[] particleTextures = ContentManagerUtilities.LoadParticleTextures(tmxObject.Properties["TexturesFile"], content);
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

        private NpcSpawner LoadNpcSpawner(TmxObjectGroup.TmxObject tmxObject, OrderedDictionary<string, IGameObject> gameObjects, ContentManager content)
        {
            OrderedDictionary<string, Npc> _npcs = new OrderedDictionary<string, Npc>();

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(tmxObject.Properties["SpawnerFile"]);

            XmlNodeList nodes = xmlDoc.SelectNodes("NPCS/NPC");

            var spawningEntities = new List<IEntity>();

            for (int i = 0; i < nodes.Count; i++)
            {
                // Load the npcs for the spawner.
                string uniqueID = nodes[i].Attributes["UniqueID"].Value;
                string npcDataFilePath = nodes[i].Attributes["filepath"].Value;

                spawningEntities.Add(Npc.Load(npcDataFilePath, uniqueID, content));
            }

            var spawnerPosition = new Vector2(tmxObject.X, tmxObject.Y);
            var updateInterval = uint.Parse(tmxObject.Properties["UpdateInterval"]);

            return new NpcSpawner(spawnerPosition, updateInterval, gameObjects, spawningEntities);
        }
    }
}