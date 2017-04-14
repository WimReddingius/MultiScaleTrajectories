using System;
using MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST;
using MultiScaleTrajectories.Algorithm.Geometry;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional
{
    class EnhancedRedBlackNode : RedBlackNode<Point2D, EnhancedRedBlackNode>
    {
        public double DistanceFromStart;
        public Point2D FurthestFromStart;
        public double FurthestFromStartDistance;
        

        public EnhancedRedBlackNode()
        {
            Action updateMaxDistance = () =>
            {
                FurthestFromStart = Element;
                FurthestFromStartDistance = DistanceFromStart;
                if (Left != null && Left.FurthestFromStartDistance > FurthestFromStartDistance)
                {
                    SetFurthestDescendantNode(Left);
                }
                if (Right != null && Right.FurthestFromStartDistance > FurthestFromStartDistance)
                {
                    SetFurthestDescendantNode(Right);
                }
            };

            ElementReplaced += updateMaxDistance;
            LeftNodeChanged += updateMaxDistance;
            RightNodeChanged += updateMaxDistance;
        }

        public void SetFurthestDescendantNode(EnhancedRedBlackNode node)
        {
            FurthestFromStart = node.FurthestFromStart;
            FurthestFromStartDistance = node.FurthestFromStartDistance;
        }
    }
}
