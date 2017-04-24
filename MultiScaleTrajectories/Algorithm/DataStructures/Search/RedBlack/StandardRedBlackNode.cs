namespace MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack
{
    class StandardRedBlackNode<TEl> : RedBlackNode<TEl, StandardRedBlackNode<TEl>> where TEl : class
    {
        public StandardRedBlackNode(TEl element, StandardRedBlackNode<TEl> succ, StandardRedBlackNode<TEl> pred) : base(element, succ, pred)
        {
        }
    }
}
