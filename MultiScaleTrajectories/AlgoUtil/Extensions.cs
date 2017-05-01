using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiScaleTrajectories.AlgoUtil
{
    static class Extensions
    {
        public static void MonotonicallyIteratePairs<T>(this List<T> list, Action<T, T> pairAction, Action<T> newFirst = null, bool forward = true)
        {
            Func<int, int> step;
            int startI;
            Func<int, bool> conditionI;
            Func<int, bool> conditionJ;

            if (forward)
            {
                step = i => i + 1;
                startI = 0;
                conditionI = i => i < list.Count - 2;
                conditionJ = j => j < list.Count;
            }
            else
            {
                step = i => i - 1;
                startI = list.Count() - 1;
                conditionI = i => i >= 2;
                conditionJ = j => j >= 0;
            }

            for (var i = startI; conditionI(i); i = step(i))
            {
                var elementI = list[i];

                newFirst?.Invoke(elementI);

                for (var j = step(i); conditionJ(j); j = step(j))
                {
                    pairAction(elementI, list[j]);
                }
            }
        }
    }
}
