using MultiScaleTrajectories.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
