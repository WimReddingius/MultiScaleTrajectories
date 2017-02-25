using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.View.Type.Visualization.GL
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
                return map.GetValueFromKey(pickId);
            else
                throw new InvalidOperationException();
        }

        public int getPickingId(Object obj)
        {
            return map.GetKeyFromValue(obj);
        }

        public void AssignPickId(Object obj)
        {
            map.Put(pickIdGenerator++, obj);
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
