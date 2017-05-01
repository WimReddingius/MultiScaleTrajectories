using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range.RedBlack
{
    class StandardRedBlackRangeBST<TEl, TRange> : RangeRedBlackBST<TEl, StandardRedBlackRangeNode<TEl, TRange>, TRange>
    {
        public StandardRedBlackRangeBST(Comparer<TEl> comparer, RangeConsolidator<TRange> rangeConsolidator, RangeInitializer<TEl, TRange> rangeInitializer)
            : base(comparer, rangeConsolidator, rangeInitializer)
        {
        }

        public StandardRedBlackRangeBST(Comparison<TEl> comparison, RangeConsolidator<TRange> rangeConsolidator, RangeInitializer<TEl, TRange> rangeInitializer) 
            : base(comparison, rangeConsolidator, rangeInitializer)
        {
        }

        protected override StandardRedBlackRangeNode<TEl, TRange> CreateNode(TEl element, StandardRedBlackRangeNode<TEl, TRange> succ, StandardRedBlackRangeNode<TEl, TRange> pred)
        {
            return new StandardRedBlackRangeNode<TEl, TRange>(element, succ, pred, RangeConsolidator, RangeInitializer);
        }
    }
}
