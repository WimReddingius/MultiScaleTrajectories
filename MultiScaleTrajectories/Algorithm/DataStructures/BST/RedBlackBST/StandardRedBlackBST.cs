using System;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST
{
    class StandardRedBlackBST<T> : RedBlackBST<T, StandardRedBlackNode<T>>
    {
        public StandardRedBlackBST(Comparer<T> comparer) : base(comparer)
        {
        }

        public StandardRedBlackBST(Comparison<T> comparison) : base(comparison)
        {
        }
    }
}
