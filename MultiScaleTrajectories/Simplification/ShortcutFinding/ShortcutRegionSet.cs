using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Util;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding
{
    [JsonObject]
    class ShortcutRegionSet : IShortcutSet
    {
        public Trajectory2D Trajectory { get; }

        public int Count => RegionMap.Values.SelectMany(l => l.Select(r => r.Count)).Sum();

        //worst case O(n^2), expected O(n)
        public readonly JDictionary<TPoint2D, LinkedList<Region>> RegionMap;

        [JsonConstructor]
        private ShortcutRegionSet(Trajectory2D Trajectory, JDictionary<TPoint2D, LinkedList<Region>> RegionMap)
        {
            this.Trajectory = Trajectory;
            this.RegionMap = RegionMap;
        }

        public ShortcutRegionSet(Trajectory2D trajectory)
        {
            Trajectory = trajectory;
            RegionMap = new JDictionary<TPoint2D, LinkedList<Region>>();

            foreach (var point in Trajectory)
            {
                RegionMap[point] = new LinkedList<Region>();
            }
        }

        public LinkedList<Region> GetRegions(TPoint2D point)
        {
            if (RegionMap.ContainsKey(point))
                return RegionMap[point];

            return new LinkedList<Region>();
        }

        //worst case O(n^2), expected O(n)
        public void Except(IShortcutSet set)
        {
            var otherRegionMap = ((ShortcutRegionSet)set).RegionMap;

            foreach (var start in otherRegionMap.Keys)
            {
                if (!RegionMap.ContainsKey(start))
                {
                    continue;
                }

                var regions = RegionMap[start];
                var otherRegions = otherRegionMap[start];
                var regionsNode = regions.First;
                var otherNode = otherRegions.First;

                //worst case O(n), expected O(1)
                while (regionsNode != null)
                {
                    var region = regionsNode.Value;

                    if (otherNode == null)
                    {
                        break;
                    }

                    var otherRegion = otherNode.Value;

                    //other region too far back, consider next one for overlap
                    if (otherRegion.End.Index < region.Start.Index)
                    {
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //region too far back, consider next one for overlap
                    if (otherRegion.Start.Index > region.End.Index)
                    {
                        regionsNode = regionsNode.Next;
                        continue;
                    }

                    //it overlaps at the start: prune region start
                    if (otherRegion.End.Index < region.End.Index && otherRegion.Start.Index <= region.Start.Index) 
                    {
                        region.Start = Trajectory[otherRegion.End.Index + 1];
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //it overlaps at the end: prune region end
                    if (otherRegion.Start.Index > region.Start.Index && otherRegion.End.Index >= region.End.Index) 
                    {
                        region.End = Trajectory[otherRegion.Start.Index - 1];
                        regionsNode = regionsNode.Next;
                        continue;
                    }

                    //spanning other region
                    if (otherRegion.Start.Index > region.Start.Index && otherRegion.End.Index < region.End.Index)
                    {
                        //second part
                        regionsNode = regions.AddAfter(regionsNode, new Region(Trajectory[otherRegion.End.Index + 1], region.End));

                        //first part
                        region.End = Trajectory[otherRegion.Start.Index - 1];

                        otherNode = otherNode.Next;
                        continue;
                    }

                    //full overlap
                    var nextNode = regionsNode.Next;
                    regions.Remove(regionsNode);
                    regionsNode = nextNode;
                }
            }
        }

        //worst case O(n^2), expected O(n)
        public void Intersect(IShortcutSet set)
        {
            var otherRegionMap = ((ShortcutRegionSet)set).RegionMap;

            foreach (var start in otherRegionMap.Keys)
            {
                if (!RegionMap.ContainsKey(start))
                {
                    continue;
                }

                var regions = RegionMap[start];
                var otherRegions = otherRegionMap[start];
                var regionsNode = regions.First;
                var otherNode = otherRegions.First;

                //worst case O(n), expected O(1)
                while (regionsNode != null)
                {
                    var nextNode = regionsNode.Next;
                    var region = regionsNode.Value;

                    if (otherNode == null)
                    {
                        regions.Remove(regionsNode);
                    }
                    else
                    {
                        var otherRegion = otherNode.Value;

                        //other region too far back, consider next one for overlap
                        if (otherRegion.End.Index < region.Start.Index)
                        {
                            otherNode = otherNode.Next;
                            continue;
                        }

                        //other region too far ahead
                        if (otherRegion.Start.Index > region.End.Index)
                        {
                            regions.Remove(regionsNode);
                        }
                        else //some overlap
                        {
                            var nextOtherRegion = otherNode.Next?.Value;
                            var nextOverlaps = false;

                            if (nextOtherRegion != null)
                            {
                                nextOverlaps = nextOtherRegion.Start.Index <= region.End.Index;
                            }

                            if (nextOverlaps) //split region
                            {
                                var newRegion = new Region(region.Start, region.End);

                                //it connects at the start: prune region end
                                if (otherRegion.End.Index < region.End.Index)
                                {
                                    newRegion.End = otherRegion.End;
                                }

                                //it connects at the end: prune region start
                                if (otherRegion.Start.Index > region.Start.Index)
                                {
                                    newRegion.Start = otherRegion.Start;
                                }

                                regions.AddBefore(regionsNode, newRegion);

                                otherNode = otherNode.Next;
                                continue;
                            }
                            
                            //it connects at the start: prune region end
                            if (otherRegion.End.Index < region.End.Index)
                            {
                                region.End = otherRegion.End;
                            }

                            //it connects at the end: prune region start
                            if (otherRegion.Start.Index > region.Start.Index)
                            {
                                region.Start = otherRegion.Start;
                            }
                        }
                    }

                    regionsNode = nextNode;
                }
            }
        }

        //worst case O(n^2), expected O(n)
        public void Union(IShortcutSet set)
        {
            var otherRegionMap = ((ShortcutRegionSet)set).RegionMap;

            foreach (var start in otherRegionMap.Keys)
            {
                if (!RegionMap.ContainsKey(start))
                    continue;

                var regions = RegionMap[start];
                var otherRegions = otherRegionMap[start];

                var regionsNode = regions.First;
                var otherNode = otherRegions.First;

                while (otherNode != null)
                {
                    if (regionsNode == null)
                    {
                        regions.AddLast(otherNode.Value.Clone());
                        otherNode = otherNode.Next;
                        continue;
                    }

                    var region = regionsNode.Value;
                    var otherRegion = otherNode.Value;

                    //not yet reached
                    if (otherRegion.Start.Index > region.End.Index + 1)
                    {
                        regionsNode = regionsNode.Next;
                        continue;
                    }

                    //we passed it: simply add the region
                    if (otherRegion.End.Index < region.Start.Index - 1)
                    {
                        regions.AddBefore(regionsNode, new Region(otherRegion.Start, otherRegion.End));
                        otherNode = otherNode.Next;
                        continue;
                    }

                    //it connects at the start: extend region backwards
                    if (otherRegion.End.Index >= region.Start.Index - 1)
                    {
                        region.Start = otherRegion.Start;
                    }

                    //it connects at the end: extend region forward
                    if (otherRegion.Start.Index <= region.End.Index + 1)
                    {
                        region.End = otherRegion.End;

                        var tempNode = regionsNode.Next;
                        while (tempNode != null)
                        {
                            var nextNode = tempNode.Next;
                            var furtherRegion = tempNode.Value;

                            //remove any fully overlapped regions
                            if (furtherRegion.End.Index <= region.End.Index)
                            {
                                regions.Remove(tempNode);
                                tempNode = nextNode;
                                continue;
                            }

                            //merge any region that may connect at the end
                            if (furtherRegion.Start.Index <= region.End.Index + 1)
                            {
                                regions.Remove(tempNode);
                                region.End = furtherRegion.End;
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
            foreach (var start in RegionMap.Keys)
            {
                if (start == point)
                    continue;

                var regions = RegionMap[start];

                var node = regions.First;
                while (node != null)
                {
                    var nextNode = node.Next;

                    var region = node.Value;
                    var startIndex = region.Start.Index;
                    var endIndex = region.End.Index;

                    if (startIndex == index && endIndex == index)
                    {
                        regions.Remove(node);
                    }
                    else if (startIndex == index)
                    {
                        region.Start = Trajectory[startIndex + 1];
                    }
                    else if (endIndex == index)
                    {
                        region.End = Trajectory[endIndex - 1];
                    }
                    else if (startIndex < index && endIndex > index)
                    {
                        //second part
                        regions.AddAfter(node, new Region(Trajectory[startIndex + 1], region.End));

                        //first part
                        region.End = Trajectory[index - 1];                        
                    }

                    node = nextNode;
                }
            }


            RegionMap.Remove(point);

        }

        public void AppendRegion(TPoint2D start, int rangeStart, int rangeEnd)
        {
            var regionList = RegionMap[start];
            var regionStart = Trajectory[rangeStart];
            var regionEnd = Trajectory[rangeEnd];

            var lastRegion = regionList.Last?.Value;
            if (lastRegion != null && lastRegion.End.Index == regionStart.Index - 1)
                lastRegion.End = regionEnd;
            else
                regionList.AddLast(new Region(Trajectory[rangeStart], Trajectory[rangeEnd]));
        }

        public void PrependRegion(TPoint2D start, int rangeStart, int rangeEnd)
        {
            var regionList = RegionMap[start];
            
            var regionStart = Trajectory[rangeStart];
            var regionEnd = Trajectory[rangeEnd];

            var firstRegion = regionList.First?.Value;
            if (firstRegion != null && firstRegion.Start.Index == regionEnd.Index + 1)
                firstRegion.Start = regionStart;
            else
                regionList.AddFirst(new Region(Trajectory[rangeStart], Trajectory[rangeEnd]));
        }

        public void AppendShortcut(TPoint2D start, TPoint2D end)
        {
            var regionList = RegionMap[start];

            var lastRegion = regionList.Last?.Value;
            if (lastRegion != null && lastRegion.End.Index == end.Index - 1)
                lastRegion.End = end;
            else
                regionList.AddLast(new Region(end, end));
        }

        public void PrependShortcut(TPoint2D start, TPoint2D end)
        {
            var regionList = RegionMap[start];

            var firstRegion = regionList.First?.Value;
            if (firstRegion != null && firstRegion.Start.Index == end.Index + 1)
                firstRegion.Start = end;
            else
                regionList.AddFirst(new Region(end, end));
        }

        //O(n^2)
        public Dictionary<TPoint2D, ICollection<TPoint2D>> AsMap()
        {
            var map = new Dictionary<TPoint2D, ICollection<TPoint2D>>();

            foreach (var pair in RegionMap)
            {
                var point = pair.Key;
                var regions = pair.Value;
                var ends = new HashSet<TPoint2D>();

                foreach (var region in regions)
                {
                    for (var i = region.Start.Index; i <= region.End.Index; i++)
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
            return RegionMap.SelectMany(pair =>
            {
                var point = pair.Key;
                var regions = pair.Value;
                return regions.SelectMany(region =>
                {
                    var shortcuts = new HashSet<Shortcut>();
                    for (var i = region.Start.Index; i <= region.End.Index; i++)
                    {
                        shortcuts.Add(new Shortcut(point, Trajectory[i]));
                    }
                    return shortcuts;
                });
            });
        }

        public class Region
        {
            public int Count => End.Index - Start.Index + 1;

            public TPoint2D Start;
            public TPoint2D End;

            public Region(TPoint2D start, TPoint2D end)
            {
                Start = start;
                End = end;
            }

            public Region Clone()
            {
                return new Region(Start, End);
            }
        }

    }
}
