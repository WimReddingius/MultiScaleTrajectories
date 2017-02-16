using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.util
{
    class OneToOneMap<K, V>
    {
        private Dictionary<K, V> keyToVal;
        private Dictionary<V, K> valToKey;

        public OneToOneMap()
        {
            this.keyToVal = new Dictionary<K, V>();
            this.valToKey = new Dictionary<V, K>();
        }

        public void add(K k, V v)
        {
            if (!keyToVal.ContainsKey(k) && !valToKey.ContainsKey(v))
            {
                keyToVal.Add(k, v);
                valToKey.Add(v, k);
            }
            else {
                throw new InvalidOperationException();
            }
        }

        public V getValueFromKey(K k)
        {
            return keyToVal[k];
        }

        public K getKeyFromValue(V v)
        {
            return valToKey[v];
        }
    }
}
