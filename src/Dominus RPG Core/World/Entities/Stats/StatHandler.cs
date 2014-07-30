using System;
using System.Collections;
using System.Collections.Generic;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    [Serializable]
    public class StatHandler : IEnumerable
    {
        private Dictionary<string, object> _stats;

        public StatHandler()
        {
            _stats = new Dictionary<string, object>();
        }

        public IStat<T> GetStat<T>(string name)
        {
            return _stats[name] as IStat<T>;
        }

        public void AddStat<T>(IStat<T> stat, string name)
        {
            _stats.Add(name, stat as object);
        }

        public void RemoveStat(string name)
        {
            _stats.Remove(name);
        }

        public IEnumerator GetEnumerator()
        {
            return _stats.GetEnumerator();
        }
    }
}