namespace MultiScaleTrajectories.Algorithm.DataStructures.Search
{
    delegate int SearchPredicate<in T>(T x);

    class SearchPredicator<T>
    {
        private readonly SearchPredicate<T> predicate;

        protected SearchPredicator()
        {
        }

        public SearchPredicator(SearchPredicate<T> predicate)
        {
            this.predicate = predicate;
        }

        public virtual int ElementCompliance(T x)
        {
            return predicate(x);
        }
    }
}
