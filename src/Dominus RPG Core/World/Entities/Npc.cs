using Dominus_Core;
using Dominus_Core.Graphics;
using Dominus_RPG_Core.World.Entities.Stats;
using Dominus_Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace Dominus_RPG_Core.World.Entities
{
    internal class Npc : IEntity
    {
        private SpriteSheet _spriteSheet;
        private readonly string _uniqueID;
        private Vector2 _position;
        private IEntityCombatHandler _combatHandler;
        private StatHandler _stats;
        private OrderedDictionary<string, IGameObject> _parentLayerGameObjects;
        private IEntity _target;

        /// <summary>
        /// Represents the game objects of the layer that this particular NPC exists within.
        /// </summary>
        internal OrderedDictionary<string, IGameObject> ParentLayerGameObjects { get { return _parentLayerGameObjects; } set { _parentLayerGameObjects = value; } }

        public SpriteSheet SpriteSheet
        {
            get { return _spriteSheet; }
        }

        public virtual bool Dead { get { return _stats.GetStat<float>("Health").GetPoints() <= 0; } }

        public string Name { get; set; }

        public string UniqueID { get { return _uniqueID; } }

        public int Level { get; set; }

        public float Speed { get; set; }

        public Vector2 Position { get { return _position; } set { _position = value; _spriteSheet.Position = value; } }

        public Vector2 Range { get; set; }

        public IEntityCombatHandler CombatHandler { get { return _combatHandler; } private set { _combatHandler = value; } }

        public StatHandler Stats { get { return _stats; } }

        public event EventHandler Moved;

        public Npc(string uniqueID)
        {
            _uniqueID = uniqueID;
            _stats = new StatHandler();
            _stats.AddStat(new Strength(100), "Strength");
            _stats.AddStat(new Defence(100), "Defence");
            _stats.AddStat(new Health(100), "Health");
            _combatHandler = new DefaultCombatHandler();
        }

        protected virtual void FindTarget()
        {
            foreach (IEntity entities in _parentLayerGameObjects.Values)
            {
                // Is the target attackable?
                if (entities.GetType() == typeof(Player))
                {
                    _target = entities;
                }
                else
                {
                    // TOOD: Check if the NPC is targetable.
                }
            }
        }

        public virtual void MoveTo(Vector2 position)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            if (this.Dead) return;

            this.SpriteSheet.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (this.Dead)
            {
                // The npc is dead, so we'll draw its corpse.
            }
            else
            {
                _spriteSheet.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Loads a NPC from a XML information file.
        /// </summary>
        /// <param name="filePath">Path to the NPCs XML information file.</param>
        /// <param name="content">ContentManager from which to load the NPC texture.</param>
        /// <returns>NPC created from the specified XML information file.</returns>
        public static Npc Load(string filePath, string uniqueID, ContentManager content)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.SelectSingleNode("Npc");

            string name = node.ChildNodes[0].InnerText;
            int level = int.Parse(node.ChildNodes[1].InnerText);
            Vector2 position = new Vector2().FromString(node.ChildNodes[2].InnerText);
            float speed = float.Parse(node.ChildNodes[3].InnerText);
            Texture2D texture = ContentManagerUtilities.LoadTexture2D(content, node.ChildNodes[4].InnerText);

            var npc = new Npc(uniqueID)
            {
                Name = name,
                Level = level,
                _spriteSheet = new SpriteSheet(texture, 3, 4, 32, 32),
                Position = position,
                Speed = speed
            };

            npc.Stats.GetStat<float>("Health").SetPoints(float.Parse(node.ChildNodes[5].InnerText));
            npc.Stats.GetStat<int>("Strength").SetPoints(int.Parse(node.ChildNodes[6].InnerText));
            npc.Stats.GetStat<int>("Defence").SetPoints(int.Parse(node.ChildNodes[7].InnerText));

            if (node.ChildNodes[8].Value != "default" && File.Exists(node.ChildNodes[8].InnerText))
            {
                var assembly = Assembly.LoadFrom(node.ChildNodes[8].InnerText);
                Type type = assembly.GetTypes().FirstOrDefault(x => x.GetType() == typeof(IEntityCombatHandler));
                IEntityCombatHandler combatHandler = Activator.CreateInstance(type) as IEntityCombatHandler;
                npc.CombatHandler = combatHandler;
            }

            return npc;
        }
    }
}