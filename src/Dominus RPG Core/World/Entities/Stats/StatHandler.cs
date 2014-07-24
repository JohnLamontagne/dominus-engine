using System;
using System.Collections;
using System.Collections.Generic;

namespace Dominus_RPG_Core.World.Entities.Stats
{
    [Serializable]
    public class StatHandler : IEnumerable
    {
        private Dictionary<string, IStat<object>> _stats;

        public StatHandler()
        {
            _stats = new Dictionary<string, IStat<object>>();
        }

        public IStat<T> GetStat<T>(string name)
        {
            IStat<object> value;

            if (_stats.TryGetValue(name, out value))
            {
                if (value.GetType() == typeof(T))
                {
                    return (IStat<T>)value;
                }
            }

            return default(IStat<T>);
        }

        public void AddStat<T>(IStat<T> stat, string name)
        {
            _stats.Add(name, stat as IStat<object>);
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
