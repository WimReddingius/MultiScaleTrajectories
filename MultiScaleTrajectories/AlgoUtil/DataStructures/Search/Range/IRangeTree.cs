namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range
{
    delegate TRange RangeConsolidator<TRange>(TRange first, TRange second);

    delegate TRange RangeInitializer<TEl, TRange>(TEl element);

    interface IRangeTree<TEl, TRange> : ISearchTree<TEl>
    {
        bool TryFindRangeData(RangePredicator<TEl> rangePredicator, out TRange data);

        bool TryFindRangeData(RangePredicate<TEl> rangePredicate, out TRange data);

        TRange GetRangeData(TEl element);
    }
}
