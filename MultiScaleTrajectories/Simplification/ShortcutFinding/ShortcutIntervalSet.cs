using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    [JsonObject]
    class ShortcutIntervalSet : IShortcutSet
    {
        public Trajectory2D Trajectory { get; }

        public long Count => IntervalMap.Values.SelectMany(l => l.Select(r => r.Count)).Sum();
        public long IntervalCount => IntervalMap.Values.Select(l => l.Count).Sum();

        //worst case O(n^2), expected O(n)
        public readonly JDictionary<TPoint2D, LinkedList<Interval>> IntervalMap;

        [JsonConstructor]
        private ShortcutIntervalSet(Trajectory2D Trajectory, JDictionary<TPoint2D, LinkedList<Interval>> intervalMap)
        {
            this.Trajectory = Trajectory;
            this.IntervalMap = intervalMap;
        }

        public ShortcutIntervalSet(Trajectory2D trajectory)
        {
            Trajectory = trajectory;
            IntervalMap = new JDictionary<TPoint2D, LinkedList<Interval>>();

            foreach (var point in Trajectory)
            {
                IntervalMap[point] = new LinkedList<Interval>();
            }
        }

        public LinkedList<Interval> GetIntervals(TPoint2D point)
        {
            if (IntervalMap.ContainsKey(point))
                return IntervalMap[point];

            return new LinkedList<Interval>();
        }

        //worst case O(n^2), expected O(n)
        public void Except(IShortcutSet set)
        {
            var otherIntervalMap = ((ShortcutIntervalSet)set).IntervalMap;

            foreach (var start in otherIntervalMap.Keys)
            {
                if (!IntervalMap.ContainsKey(start))
                {
                    continue;
                }

                var intervals = IntervalMap[start];
                var otherIntervals = otherIntervalMap[start];
                var intervalsNode = intervals.First;
                var otherNode = otherIntervals.First;

                //worst case O(n), expected O(1)
                while (intervalsNode != null)
                {
                    var interval = intervalsNode.Value;

                    if (otherNode == null)
                    {
                        break;
                    }

                    var otherInterval = otherNode.Value;

                    //other interval too far back, consider next one for overlap
                    if (otherInterval.End.Index < interval.Start.Index)
                    {
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //interval too far back, consider next one for overlap
                    if (otherInterval.Start.Index > interval.End.Index)
                    {
                        intervalsNode = intervalsNode.Next;
                        continue;
                    }

                    //it overlaps at the start: prune interval start
                    if (otherInterval.End.Index < interval.End.Index && otherInterval.Start.Index <= interval.Start.Index) 
                    {
                        interval.Start = Trajectory[otherInterval.End.Index + 1];
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //it overlaps at the end: prune interval end
                    if (otherInterval.Start.Index > interval.Start.Index && otherInterval.End.Index >= interval.End.Index) 
                    {
                        interval.End = Trajectory[otherInterval.Start.Index - 1];
                        intervalsNode = intervalsNode.Next;
                        continue;
                    }

                    //spanning other interval
                    if (otherInterval.Start.Index > interval.Start.Index && otherInterval.End.Index < interval.End.Index)
                    {
                        //second part
                        intervalsNode = intervals.AddAfter(intervalsNode, new Interval(Trajectory[otherInterval.End.Index + 1], interval.End));

                        //first part
                        interval.End = Trajectory[otherInterval.Start.Index - 1];

                        otherNode = otherNode.Next;
                        continue;
                    }

                    //full overlap
                    var nextNode = intervalsNode.Next;
                    intervals.Remove(intervalsNode);
                    intervalsNode = nextNode;
                }
            }
        }

        //worst case O(n^2), expected O(n)
        public void Intersect(IShortcutSet set)
        {
            var otherIntervalMap = ((ShortcutIntervalSet)set).IntervalMap;

            foreach (var start in otherIntervalMap.Keys)
            {
                if (!IntervalMap.ContainsKey(start))
                {
                    continue;
                }

                var intervals = IntervalMap[start];
                var otherIntervals = otherIntervalMap[start];
                var intervalsNode = intervals.First;
                var otherNode = otherIntervals.First;

                //worst case O(n), expected O(1)
                while (intervalsNode != null)
                {
                    var nextNode = intervalsNode.Next;
                    var interval = intervalsNode.Value;

                    if (otherNode == null)
                    {
                        intervals.Remove(intervalsNode);
                    }
                    else
                    {
                        var otherInterval = otherNode.Value;

                        //other interval too far back, consider next one for overlap
                        if (otherInterval.End.Index < interval.Start.Index)
                        {
                            otherNode = otherNode.Next;
                            continue;
                        }

                        //other interval too far ahead
                        if (otherInterval.Start.Index > interval.End.Index)
                        {
                            intervals.Remove(intervalsNode);
                        }
                        else //some overlap
                        {
                            var nextOtherInterval = otherNode.Next?.Value;
                            var nextOverlaps = false;

                            if (nextOtherInterval != null)
                            {
                                nextOverlaps = nextOtherInterval.Start.Index <= interval.End.Index;
                            }

                            if (nextOverlaps) //split interval
                            {
                                var newInterval = new Interval(interval.Start, interval.End);

                                //it connects at the start: prune interval end
                                if (otherInterval.End.Index < interval.End.Index)
                                {
                                    newInterval.End = otherInterval.End;
                                }

                                //it connects at the end: prune interval start
                                if (otherInterval.Start.Index > interval.Start.Index)
                                {
                                    newInterval.Start = otherInterval.Start;
                                }

                                intervals.AddBefore(intervalsNode, newInterval);

                                otherNode = otherNode.Next;
                                continue;
                            }
                            
                            //it connects at the start: prune interval end
                            if (otherInterval.End.Index < interval.End.Index)
                            {
                                interval.End = otherInterval.End;
                            }

                            //it connects at the end: prune interval start
                            if (otherInterval.Start.Index > interval.Start.Index)
                            {
                                interval.Start = otherInterval.Start;
                            }
                        }
                    }

                    intervalsNode = nextNode;
                }
            }
        }

        //worst case O(n^2), expected O(n)
        public void Union(IShortcutSet set)
        {
            var otherIntervalMap = ((ShortcutIntervalSet)set).IntervalMap;

            foreach (var start in otherIntervalMap.Keys)
            {
                if (!IntervalMap.ContainsKey(start))
                    continue;

                var intervals = IntervalMap[start];
                var otherIntervals = otherIntervalMap[start];

                var intervalsNode = intervals.First;
                var otherNode = otherIntervals.First;

                while (otherNode != null)
                {
                    if (intervalsNode == null)
                    {
                        intervals.AddLast(otherNode.Value.Clone());
                        otherNode = otherNode.Next;
                        continue;
                    }

                    var interval = intervalsNode.Value;
                    var otherInterval = otherNode.Value;

                    //not yet reached
                    if (otherInterval.Start.Index > interval.End.Index + 1)
                    {
                        intervalsNode = intervalsNode.Next;
                        continue;
                    }

                    //we passed it: simply add the interval
                    if (otherInterval.End.Index < interval.Start.Index - 1)
                    {
                        intervals.AddBefore(intervalsNode, new Interval(otherInterval.Start, otherInterval.End));
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //it connects at the start: extend interval backwards
                    if (otherInterval.End.Index >= interval.Start.Index - 1)
                    {
                        interval.Start = otherInterval.Start;
                    }

                    //it connects at the end: extend interval forward
                    if (otherInterval.Start.Index <= interval.End.Index + 1)
                    {
                        interval.End = otherInterval.End;

                        var tempNode = intervalsNode.Next;
                        while (tempNode != null)
                        {
                            var nextNode = tempNode.Next;
                            var furtherInterval = tempNode.Value;

                            //remove any fully overlapped intervals
                            if (furtherInterval.End.Index <= interval.End.Index)
                            {
                                intervals.Remove(tempNode);
                                tempNode = nextNode;
                                continue;
                            }

                            //merge any interval that may connect at the end
                            if (furtherInterval.Start.Index <= interval.End.Index + 1)
                            {
                                intervals.Remove(tempNode);
                                interval.End = furtherInterval.End;
                            }

                            break;
                        }
                    }

                    otherNode = otherNode.Next;

                }
            }
        }

        //worst case O(n^2), expected O(n)
        public void Except(TPoint2D point)
        {
            var index = point.Index;
            foreach (var start in IntervalMap.Keys)
            {
                if (start == point)
                    continue;

                var intervals = IntervalMap[start];

                var node = intervals.First;
                while (node != null)
                {
                    var nextNode = node.Next;

                    var interval = node.Value;
                    var startIndex = interval.Start.Index;
                    var endIndex = interval.End.Index;

                    if (startIndex == index && endIndex == index)
                    {
                        intervals.Remove(node);
                    }
                    else if (startIndex == index)
                    {
                        interval.Start = Trajectory[startIndex + 1];
                    }
                    else if (endIndex == index)
                    {
                        interval.End = Trajectory[endIndex - 1];
                    }
                    else if (startIndex < index && endIndex > index)
                    {
                        //second part
                        intervals.AddAfter(node, new Interval(Trajectory[startIndex + 1], interval.End));

                        //first part
                        interval.End = Trajectory[index - 1];                        
                    }

                    node = nextNode;
                }
            }


            IntervalMap.Remove(point);

        }

        public void AppendInterval(TPoint2D start, int rangeStart, int rangeEnd)
        {
            var intervalList = IntervalMap[start];
            var intervalStart = Trajectory[rangeStart];
            var intervalEnd = Trajectory[rangeEnd];

            var lastInterval = intervalList.Last?.Value;
            if (lastInterval != null && lastInterval.End.Index == intervalStart.Index - 1)
                lastInterval.End = intervalEnd;
            else
                intervalList.AddLast(new Interval(Trajectory[rangeStart], Trajectory[rangeEnd]));
        }

        public void PrependInterval(TPoint2D start, int rangeStart, int rangeEnd)
        {
            var intervalList = IntervalMap[start];
            
            var intervalStart = Trajectory[rangeStart];
            var intervalEnd = Trajectory[rangeEnd];

            var firstInterval = intervalList.First?.Value;
            if (firstInterval != null && firstInterval.Start.Index == intervalEnd.Index + 1)
                firstInterval.Start = intervalStart;
            else
                intervalList.AddFirst(new Interval(Trajectory[rangeStart], Trajectory[rangeEnd]));
        }

        //ignores weight
        public void AppendShortcut(TPoint2D start, TPoint2D end)
        {
            var intervalList = IntervalMap[start];

            var lastInterval = intervalList.Last?.Value;
            if (lastInterval != null && lastInterval.End.Index == end.Index - 1)
                lastInterval.End = end;
            else
                intervalList.AddLast(new Interval(end, end));
        }

        //ignores weight
        public void PrependShortcut(TPoint2D start, TPoint2D end)
        {
            var intervalList = IntervalMap[start];

            var firstInterval = intervalList.First?.Value;
            if (firstInterval != null && firstInterval.Start.Index == end.Index + 1)
                firstInterval.Start = end;
            else
                intervalList.AddFirst(new Interval(end, end));
        }

        public void IncrementAllWeights()
        {
            foreach (var intervals in IntervalMap.Values)
            {
                foreach (var interval in intervals)
                {
                    interval.Weight++;
                }
            }
        }

        //O(n^2)
        public Dictionary<TPoint2D, JHashSet<TPoint2D>> AsMap()
        {
            var map = new Dictionary<TPoint2D, JHashSet<TPoint2D>>();

            foreach (var pair in IntervalMap)
            {
                var point = pair.Key;
                var intervals = pair.Value;
                var ends = new JHashSet<TPoint2D>();

                foreach (var interval in intervals)
                {
                    for (var i = interval.Start.Index; i <= interval.End.Index; i++)
                    {
                        ends.Add(Trajectory[i]);
                    }
                }

                map[point] = ends;
            }

            return map;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Shortcut> GetEnumerator()
        {
            return GetEnumerable().GetEnumerator();
        }

        public IEnumerable<Shortcut> GetEnumerable()
        {
            return IntervalMap.SelectMany(pair =>
            {
                var point = pair.Key;
                var intervals = pair.Value;
                return intervals.SelectMany(interval =>
                {
                    var shortcuts = new HashSet<Shortcut>();
                    for (var i = interval.Start.Index; i <= interval.End.Index; i++)
                    {
                        shortcuts.Add(new Shortcut(point, Trajectory[i]));
                    }
                    return shortcuts;
                });
            });
        }

        public IShortcutSet Clone()
        {
            var newIntervalMap = new JDictionary<TPoint2D, LinkedList<Interval>>();

            foreach (var pair in IntervalMap)
            {
                var intervals = new LinkedList<Interval>();

                foreach (var interval in pair.Value)
                {
                    intervals.AddLast(new Interval(interval.Start, interval.End, interval.Weight));
                }

                newIntervalMap.Add(pair.Key, intervals);
            }

            return new ShortcutIntervalSet(Trajectory, newIntervalMap);
        }

        public class Interval
        {
            public int Count => End.Index - Start.Index + 1;

            public TPoint2D Start;
            public TPoint2D End;
            public int Weight;

            public Interval(TPoint2D start, TPoint2D end, int weight = 1)
            {
                Start = start;
                End = end;
                Weight = weight;
            }

            public Interval Clone()
            {
                return new Interval(Start, End, Weight);
            }
        }

    }
}
