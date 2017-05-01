namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range.RedBlack
{
    class StandardRedBlackRangeNode<TEl, TRange> : RedBlackRangeNode<TEl, StandardRedBlackRangeNode<TEl, TRange>, TRange>
    {
        public StandardRedBlackRangeNode(TEl element, StandardRedBlackRangeNode<TEl, TRange> succ, StandardRedBlackRangeNode<TEl, TRange> pred, 
            RangeConsolidator<TRange> rangeConsolidator, RangeInitializer<TEl, TRange> rangeInitializer) 
            : base(element, succ, pred, rangeConsolidator, rangeInitializer)
        {
        }
    }
}
