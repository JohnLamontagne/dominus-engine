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
    class Npc : IEntity
    {
        private Texture2D _texture;
        private readonly string _uniqueID;
        private Vector2 _position;
        private IEntityCombatHandler _combatHandler;
        private StatHandler _stats;

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public string Name { get; set; }

        public string UniqueID { get { return _uniqueID; } }

        public int Level { get; set; }

        public float Speed { get; set; }

        public Vector2 Position { get { return _position; } set { _position = value; } }

        public Vector2 Range { get; set; }

        public IEntityCombatHandler CombatHandler { get { return _combatHandler; } private set { _combatHandler = value; } }

        public StatHandler Stats { get { return _stats; } }

        public event EventHandler Moved;

        public Npc(string uniqueID)
        {
            _uniqueID = uniqueID;
            _stats = new StatHandler();
            _stats.AddStat(new Strength(), "Strength");
            _stats.AddStat(new Defence(), "Defence");
            _stats.AddStat(new Health(), "Health");
            _combatHandler = new DefaultCombatHandler();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a NPC from a XML information file.
        /// </summary>
        /// <param name="filePath">Path to the NPCs XML information file.</param>
        /// <param name="content">ContentManager from which to load the NPC texture.</param>
        /// <returns>NPC created from the specified XML information file.</returns>
        public static Npc Load(string filePath, ContentManager content)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            var node = xmlDoc.SelectSingleNode("Npc");   

            var npc = new Npc(node.Attributes["UniqueID"].Value)
            {
                Name = node.ChildNodes[0].Value,
                Level = int.Parse(node.ChildNodes[1].Value),
                Position = new Vector2().FromString(node.ChildNodes[2].Value),
                Speed = float.Parse(node.ChildNodes[3].Value),
                _texture = content.Load<Texture2D>(node.ChildNodes[4].Value)
            };

            npc.Stats.GetStat<float>("Health").SetPoints(float.Parse(node.ChildNodes[5].Value));
            npc.Stats.GetStat<int>("Strength").SetPoints(int.Parse(node.ChildNodes[6].Value));
            npc.Stats.GetStat<int>("Defence").SetPoints(int.Parse(node.ChildNodes[7].Value));

            if (node.ChildNodes[8].Value != "default" && File.Exists(node.ChildNodes[8].Value))
            {
                var assembly = Assembly.LoadFrom(node.ChildNodes[8].Value);
                Type type = assembly.GetTypes().FirstOrDefault(x => x.GetType() == typeof(IEntityCombatHandler));
                IEntityCombatHandler combatHandler = Activator.CreateInstance(type) as IEntityCombatHandler;
                npc.CombatHandler = combatHandler;
            }        

            return npc;
        }
    }
}
