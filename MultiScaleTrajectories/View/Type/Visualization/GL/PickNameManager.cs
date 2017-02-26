using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.View.Type.Visualization.GL
{
    class PickNameManager
    {
        private OneToOneMap<int, object> Map;
        private int PickIdGenerator;

        public PickNameManager()
        {
            PickIdGenerator = 1;
            Map = new OneToOneMap<int, Object>();
        }

        public bool PickingHit(int pickId)
        {
            return pickId >= 1;
        }

        public object GetPickedObject(int pickId)
        {
            if (PickingHit(pickId))
                return Map.GetValueFromKey(pickId);
            else
                throw new InvalidOperationException();
        }

        public int GetPickingId(object obj)
        {
            return Map.GetKeyFromValue(obj);
        }

        public void AssignPickId(object obj)
        {
            Map.Put(PickIdGenerator++, obj);
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
