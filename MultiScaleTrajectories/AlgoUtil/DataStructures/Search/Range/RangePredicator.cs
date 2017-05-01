namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Search.Range
{
    //1: fully inside range
    //0: partially inside range, or range too big
    //-1: fully outside range
    delegate int RangePredicate<in T>(T min, T max);

    class RangePredicator<T>
    {
        private readonly RangePredicate<T> predicate;

        protected RangePredicator()
        {
        }

        public RangePredicator(RangePredicate<T> predicate)
        {
            this.predicate = predicate;
        }

        public virtual int RangeCompliance(T min, T max)
        {
            return predicate(min, max);
        }
    }
}
