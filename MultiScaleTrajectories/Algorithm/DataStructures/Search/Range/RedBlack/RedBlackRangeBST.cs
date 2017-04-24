using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Search.Range.RedBlack
{
    abstract class RangeRedBlackBST<TEl, TNode, TRange> : RedBlackBST<TEl, TNode>, IRangeTree<TEl, TRange>
        where TNode : RedBlackRangeNode<TEl, TNode, TRange>
    {
        protected RangeConsolidator<TRange> RangeConsolidator;
        protected RangeInitializer<TEl, TRange> RangeInitializer;

        protected RangeRedBlackBST(Comparer<TEl> comparer, RangeConsolidator<TRange> rangeConsolidator, RangeInitializer<TEl, TRange> rangeInitializer) 
            : base(comparer)
        {
            RangeConsolidator = rangeConsolidator;
            RangeInitializer = rangeInitializer;
        }

        protected RangeRedBlackBST(Comparison<TEl> comparison, RangeConsolidator<TRange> rangeConsolidator, RangeInitializer<TEl, TRange> rangeInitializer) 
            : this(Comparer<TEl>.Create(comparison), rangeConsolidator, rangeInitializer)
        {
        }

        public bool TryFindRangeData(RangePredicate<TEl> rangePredicate, out TRange data)
        {
            return TryFindRangeData(Root, new RangePredicator<TEl>(rangePredicate), out data);
        }

        public bool TryFindRangeData(RangePredicator<TEl> rangePredicator, out TRange data)
        {
            return TryFindRangeData(Root, rangePredicator, out data);
        }

        //O(2logn)
        protected bool TryFindRangeData(TNode node, RangePredicator<TEl> rangePredicator, out TRange data)
        {
            var rangeCompliant = rangePredicator.RangeCompliance(node.Min, node.Max);
            if (rangeCompliant == 1)
            {
                data = node.SubtreeData;
                return true;
            }

            if (rangeCompliant == -1)
            {
                data = default(TRange);
                return false;
            }

            TRange leftData = default(TRange), rightData = default(TRange), descendantsData = default(TRange);
            var leftFound = node.Left != null && TryFindRangeData(node.Left, rangePredicator, out leftData);
            var rightFound = node.Right != null && TryFindRangeData(node.Right, rangePredicator, out rightData);

            //descendant data
            if (!leftFound && rightFound)
            {
                descendantsData =  rightData;
            }
            else if (!rightFound && leftFound)
            {
                descendantsData = leftData;
            }
            else if (leftFound && rightFound)
            {
                descendantsData = RangeConsolidator(leftData, rightData);
            }

            //final data
            if (!leftFound && !rightFound)
            {
                data = node.NodeData;
            }
            else if (rangePredicator.RangeCompliance(node.Element, node.Element) == 1)
            {
                data = RangeConsolidator(node.NodeData, descendantsData);
            }
            else
            {
                data =  descendantsData;
            }

            return true;
        }

        public TRange GetRangeData(TEl element)
        {
            return GetNode(element).SubtreeData;
        }
    }
}
