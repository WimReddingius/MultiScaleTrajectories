using System;
using System.Collections.Generic;

namespace AlgorithmVisualization.View.GLVisualization.GLUtil
{
    public class PickNameManager
    {
        private readonly OneToOneMap<int, object> map;
        private int pickIdGenerator;

        public PickNameManager()
        {
            pickIdGenerator = 1;
            map = new OneToOneMap<int, Object>();
        }

        public bool PickingHit(int pickId)
        {
            return pickId >= 1;
        }

        public object GetPickedObject(int pickId)
        {
            if (PickingHit(pickId))
                return map.GetValueFromKey(pickId);

            throw new InvalidOperationException();
        }

        public int GetPickingId(object obj)
        {
            return map.GetKeyFromValue(obj);
        }

        public void AssignPickId(object obj)
        {
            map.Put(pickIdGenerator++, obj);
        }

        private class OneToOneMap<K, V>
        {
            private readonly Dictionary<K, V> keyToVal;
            private readonly Dictionary<V, K> valToKey;

            public OneToOneMap()
            {
                keyToVal = new Dictionary<K, V>();
                valToKey = new Dictionary<V, K>();
            }

            public void Put(K k, V v)
            {
                if (!keyToVal.ContainsKey(k))
                    keyToVal.Add(k, v);

                if (!valToKey.ContainsKey(v))
                    valToKey.Add(v, k);
            }

            public V GetValueFromKey(K k)
            {
                return keyToVal[k];
            }

            public K GetKeyFromValue(V v)
            {
                return valToKey[v];
            }
        }

    }
}
