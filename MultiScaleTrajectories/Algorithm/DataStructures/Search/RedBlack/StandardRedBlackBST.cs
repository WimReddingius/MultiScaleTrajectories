using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack
{
    class StandardRedBlackBST<TEl> : RedBlackBST<TEl, StandardRedBlackNode<TEl>> where TEl : class
    {
        public StandardRedBlackBST(Comparer<TEl> comparer) : base(comparer)
        {
        }

        public StandardRedBlackBST(Comparison<TEl> comparison) : base(comparison)
        {
        }

        protected override StandardRedBlackNode<TEl> CreateNode(TEl element, StandardRedBlackNode<TEl> succ, StandardRedBlackNode<TEl> pred)
        {
            return new StandardRedBlackNode<TEl>(element, succ, pred);
        }
    }
}
