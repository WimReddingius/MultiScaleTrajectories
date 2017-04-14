using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm;

namespace MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision
{
    class ShortcutShortestPathConcrete<TAlgo> : ShortcutShortestPath where TAlgo : SingleSourceShortestPath<DataNode<Point2D>, WeightedEdge>, new()
    {
        public ShortcutShortestPathConcrete() : base(new TAlgo())
        {
        }
    }
}
