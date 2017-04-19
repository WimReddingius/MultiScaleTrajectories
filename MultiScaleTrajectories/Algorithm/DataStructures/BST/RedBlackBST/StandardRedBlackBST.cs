using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST
{
    class StandardRedBlackBST<TEl> : RedBlackBST<TEl, StandardRedBlackNode<TEl>> where TEl : class
    {
        public StandardRedBlackBST(Comparer<TEl> comparer) : base(comparer)
        {
        }

        public StandardRedBlackBST(Comparison<TEl> comparison) : base(comparison)
        {
        }
    }
}
