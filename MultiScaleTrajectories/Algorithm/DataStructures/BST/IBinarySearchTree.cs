using System;

namespace MultiScaleTrajectories.Algorithm.DataStructures.BST
{
    interface IBinarySearchTree<TEl>
    {
        TEl Min { get; }
        TEl Max { get; }
        int Size { get; }
        int Height { get; }

        bool Contains(TEl element);
        TEl Find(TEl element);
        TEl Find(BSTPredicator<TEl> comparer);

        void Insert(TEl element, Func<TEl, TEl, bool> overwriteFunc = null);
        bool Delete(TEl element);
        void DeleteMin();
        void DeleteMax();

        TEl GetPredecessor(TEl element);
        TEl GetSuccessor(TEl element);
    }
}
