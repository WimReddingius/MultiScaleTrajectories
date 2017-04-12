namespace MultiScaleTrajectories.Algorithm.DataStructures.BST
{
    delegate int BSTPredicate<in T>(T x);

    class BSTPredicator<T>
    {
        private readonly BSTPredicate<T> predicate;

        protected BSTPredicator()
        {
        }

        public BSTPredicator(BSTPredicate<T> predicate)
        {
            this.predicate = predicate;;
        }

        public virtual int Compare(T x)
        {
            return predicate(x);
        }
    }
}
