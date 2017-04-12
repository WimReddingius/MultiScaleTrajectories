using System;
using MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST;
using MultiScaleTrajectories.Algorithm.Geometry;
using OpenTK;

namespace MultiScaleTrajectories.ImaiIri.EpsilonFinding.ConvexHull.Bidirectional
{
    class EnhancedConvexHullBST : RedBlackBST<Point2D, EnhancedRedBlackNode>
    {
        private readonly EnhancedConvexHull hull;
        private Point2D ShortcutStart => hull.ShortcutStart;

        public EnhancedConvexHullBST(EnhancedConvexHull hull) : base(ConvexHull.BSTPointComparison)
        {
            this.hull = hull;
        }

        protected override EnhancedRedBlackNode CreateNode(Point2D point, bool color, int N)
        {
            var node = base.CreateNode(point, color, N);
            node.DistanceFromStart = Geometry2D.Distance(point, ShortcutStart);
            node.SetFurthestDescendantNode(node);
            return node;
        }

        public Point2D FurthestPointLeftOfShortcut(Point2D shortcutEnd)
        {
            return FurthestNodeLeftOfShortcut(Root, shortcutEnd)?.FurthestFromStart;
        }

        //O(2logn)
        protected EnhancedRedBlackNode FurthestNodeLeftOfShortcut(EnhancedRedBlackNode node, Point2D shortcutEnd)
        {
            //create a line through the start
            var vec = Vector2d.Subtract(shortcutEnd.AsVector(), ShortcutStart.AsVector());
            var normal = ShortcutStart.X < shortcutEnd.X ? vec.PerpendicularLeft : vec.PerpendicularRight;
            var normalAngle = Geometry2D.Angle(normal);
            var lineEnd = new Point2D(ShortcutStart.X + Math.Cos(normalAngle), ShortcutStart.Y + Math.Sin(normalAngle));

            //find whether the start/end of the range represented by this subtree is to the left/right of this line
            var minOrientation = Geometry2D.Orient2D(ShortcutStart, lineEnd, node.Min);
            var maxOrientation = Geometry2D.Orient2D(ShortcutStart, lineEnd, node.Max);

            //entire range on the left
            //don't report when range is compliant, but in fact you are dealing with two ranges
            var twoRanges = !hull.HullAngleCompliant(normalAngle) && node.Min.X < ShortcutStart.X && node.Max.X > ShortcutStart.X;
            if (maxOrientation == 1 && minOrientation == 1 && !twoRanges)
            {
                return node;
            }

            //entire range on the right
            if (maxOrientation == -1 && minOrientation == -1 && !twoRanges)
            {
                return null;
            }

            //range crosses line
            EnhancedRedBlackNode max = node;
            var leftMax = node.Left != null ? FurthestNodeLeftOfShortcut(node.Left, shortcutEnd) : null;
            var rightMax = node.Right != null ? FurthestNodeLeftOfShortcut(node.Right, shortcutEnd) : null;

            //note: can't be both null
            if (leftMax != null && leftMax.FurthestFromStartDistance > max.FurthestFromStartDistance)
                max = leftMax;
            if (rightMax != null && rightMax.FurthestFromStartDistance > max.FurthestFromStartDistance)
                max = rightMax;

            return max;
        }

    }
}
