using System;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Search
{
    interface ISearchTree<TEl>
    {
        TEl Min { get; }
        TEl Max { get; }
        int Size { get; }
        int Height { get; }

        bool Contains(TEl element);
        bool TryFind(SearchPredicate<TEl> predicate, out TEl element);
        bool TryFind(SearchPredicator<TEl> predicator, out TEl element);

        void Insert(TEl element, Func<TEl, bool> overwriteFunc = null);
        bool Delete(TEl element);
        void DeleteMin();
        void DeleteMax();

        bool TryGetSuccessor(TEl element, out TEl succ);
        bool TryGetPredecessor(TEl element, out TEl pred);
    }
}
