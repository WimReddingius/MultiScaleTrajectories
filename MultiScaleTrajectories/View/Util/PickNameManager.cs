using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.view
{
    class PickNameManager
    {
        private OneToOneMap<int, Object> map;
        private int pickIdGenerator;

        public PickNameManager()
        {
            this.pickIdGenerator = 1;
            this.map = new OneToOneMap<int, Object>();
        }

        public bool pickingHit(int pickId)
        {
            return pickId >= 1;
        }

        public Object getPickedObject(int pickId)
        {
            if (pickingHit(pickId))
                return map.getValueFromKey(pickId);
            else
                throw new InvalidOperationException();
        }

        public int getPickingId(Object obj)
        {
            return map.getKeyFromValue(obj);
        }

        public void AssignPickId(Object obj)
        {
            map.add(pickIdGenerator++, obj);
        }

        private class OneToOneMap<K, V>
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
}
