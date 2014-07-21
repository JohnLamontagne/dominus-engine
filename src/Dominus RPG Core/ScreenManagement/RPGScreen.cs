using Dominus_Core.ScreenManagement;
using Dominus_RPG_Core.World.Entities;
using Dominus_RPG_Core.World.WorldStructure;
using System.Linq;
using System.Collections.Generic;


namespace Dominus_RPG_Core.ScreenManagement
{
    public class RPGScreen : Screen
    {
        private readonly Dictionary<string, IEntity> _entities;
        private Map _map;

        public Map Map { get { return _map; } }

        public RPGScreen()
        {
            _entities = new Dictionary<string, IEntity>();
        }

        public virtual void ChangeMap(Map map)
        {
            // Change map logic.
            _map = map;

            // Spawn in any entities or game objects that the map specifies.
            foreach (var npcSpawn in this.Map.SpawnInformation.GetNpcSpawns())
            {
                // Load in the npc.

                // Add it to the entities.
            }
        }

        public void AddEntity(IEntity entity, string name)
        {
            _entities.Add(name, entity);
        }

        public void RemoveEntity(string name)
        {
            _entities.Remove(name);
        }

        public void RemoveEntity(IEntity entity)
        {
            var name = _entities.FirstOrDefault(x => x.Value == entity).Key;

            this.RemoveGameObject(name);
        }

        public IEntity[] GetEntities()
        {
            var values = new IEntity[_entities.Count];

            _entities.Values.CopyTo(values, 0);

            return values;
        }

        public T GetEntity<T>(string name) where T : IEntity
        {
            IEntity value;

            if (_entities.TryGetValue(name, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (T)value;
                }
            }

            return default(T);
        }

        
    }
}
