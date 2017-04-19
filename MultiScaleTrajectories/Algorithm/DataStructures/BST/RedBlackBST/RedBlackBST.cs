using System;
using System.Collections;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST
{
    //left leaning red black binary search tree with no duplicates
    class RedBlackBST<TEl, TNode> : IBinarySearchTree<TEl>, IEnumerable<TEl> 
        where TEl : class 
        where TNode : RedBlackNode<TEl, TNode>, new()
    {
        private const bool RED = true;
        private const bool BLACK = false;

        public TEl Min => Root.Min;
        public TEl Max => Root.Max;
        public int Height => TreeHeight(Root);
        public int Size => TreeSize(Root);
        public bool IsEmpty => Root == null;

        protected TNode Root;
        private readonly Comparer<TEl> Comparer;
        private readonly Dictionary<TEl, TEl> successors;
        private readonly Dictionary<TEl, TEl> predecessors;


        public RedBlackBST(Comparer<TEl> comparer)
        {
            Comparer = comparer;
            successors = new Dictionary<TEl, TEl>();
            predecessors = new Dictionary<TEl, TEl>();
        }

        public RedBlackBST(Comparison<TEl> comparison) : this(Comparer<TEl>.Create(comparison))
        {
        }

        /*************************************************************************
         *  RedBlackNode<T> helper methods
         *************************************************************************/

        // is node x red; false if x is null ?
        private bool IsRed(TNode x)
        {
            return x?.Color == RED;
        }

        // number of node in subtree rooted at x; 0 if x is null
        private int TreeSize(TNode x)
        {
            return x?.N ?? 0;
        }

        /*************************************************************************
         *  Standard BST search
         *************************************************************************/

        // value associated with the given element; null if no such element
        public TEl Find(TEl element)
        {
            return Find(new BSTPredicator<TEl>(e => Comparer.Compare(element, e)));
        }

        // value associated with the given predicator; null if no such element
        public TEl Find(BSTPredicator<TEl> comparer)
        {
            return ElementAt(FindNode(Root, comparer));
        }

        protected TNode FindNode(TNode x, BSTPredicator<TEl> predicator)
        {
            while (x != null)
            {
                var cmp = predicator.Compare(x.Element);
                if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
                else return x;
            }
            return null;
        }

        // is there a element-value pair with the given element?
        public bool Contains(TEl element)
        {
            return Find(element) != null;
        }

        protected TEl ElementAt(TNode t)
        {
            return t?.Element;
        }

        protected List<TEl> AllElementsRootedAt(TNode t)
        {
            var result = new List<TEl>();

            if (t.Left != null)
                result.AddRange(AllElementsRootedAt(t.Left));

            result.Add(t.Element);

            if (t.Right != null)
                result.AddRange(AllElementsRootedAt(t.Right));

            return result;
        }

        /*************************************************************************
         *  Red-black insertion
         *************************************************************************/

        // insert the element-value pair; overwrite the old value with the new value
        // if the element is already present
        //O(logn)
        public void Insert(TEl element, Func<TEl, TEl, bool> overwriteFunc = null)
        {
            Root = Insert(Root, element, overwriteFunc);
            Root.Color = BLACK;
            // assert Check();
        }

        // insert the element-value pair in the subtree rooted at t
        private TNode Insert(TNode t, TEl element, Func<TEl, TEl, bool> overwriteFunc, TEl bestSucc = null, TEl bestPred = null)
        {
            if (t == null)
            {
                successors[element] = bestSucc;

                if (bestPred != null)
                {
                    predecessors[element] = bestPred;
                    successors[bestPred] = element;
                }
                if (bestSucc != null)
                {
                    successors[element] = bestSucc;
                    predecessors[bestSucc] = element;
                }

                return CreateNode(element, RED, 1);
            }

            var cmp = Comparer.Compare(element, t.Element);
            if (cmp < 0)
            {
                t.Left = Insert(t.Left, element, overwriteFunc, t.Element, bestPred);
            }
            else if (cmp > 0)
            {
                t.Right = Insert(t.Right, element, overwriteFunc, bestSucc, t.Element);
            }
            else
            {
                if (overwriteFunc == null || overwriteFunc(element, t.Element))
                    t.Element = element;
            }

            // fix-up any right-leaning links
            if (IsRed(t.Right) && !IsRed(t.Left))
                t = RotateLeft(t);
            if (IsRed(t.Left) && IsRed(t.Left.Left))
                t = RotateRight(t);
            if (IsRed(t.Left) && IsRed(t.Right))
                FlipColors(t);

            t.N = TreeSize(t.Left) + TreeSize(t.Right) + 1;
            return t;
        }

        protected virtual TNode CreateNode(TEl element, bool color, int N)
        {
            var node = new TNode
            {
                Element = element,
                Color = color,
                N = N
            };
            return node;
        }

        /*************************************************************************
         *  Red-black deletion
         *************************************************************************/

        // delete the element-value pair with the minimum element
        public virtual void DeleteMin()
        {
            if (IsEmpty) throw new InvalidOperationException("BST underflow");

            predecessors[GetSuccessor(Min)] = null;

            // if both children of root are black, set root to red
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = RED;

            Root = DeleteMin(Root);
            if (!IsEmpty) Root.Color = BLACK;
            // assert Check();
        }

        // delete the element-value pair with the minimum element rooted at h
        private TNode DeleteMin(TNode t)
        {
            if (t.Left == null)
                return null;

            if (!IsRed(t.Left) && !IsRed(t.Left.Left))
                t = MoveRedLeft(t);

            t.Left = DeleteMin(t.Left);
            return Balance(t);
        }

        // delete the element-value pair with the maximum element
        public void DeleteMax()
        {
            if (IsEmpty) throw new InvalidOperationException("BST underflow");

            successors[GetPredecessor(Max)] = null;

            // if both children of root are black, set root to red
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = RED;

            Root = DeleteMax(Root);
            if (!IsEmpty) Root.Color = BLACK;
            // assert Check();

            //Max = GetPredecessor(Max);
        }

        // delete the element-value pair with the maximum element rooted at h
        private TNode DeleteMax(TNode t)
        {
            if (IsRed(t.Left))
                t = RotateRight(t);

            if (t.Right == null)
                return null;

            if (!IsRed(t.Right) && !IsRed(t.Right.Left))
                t = MoveRedRight(t);

            t.Right = DeleteMax(t.Right);

            return Balance(t);
        }

        //O(log n), not safe
        public bool Delete(TEl element)
        {
            return Delete(element, false);
        }

        // delete the element-value pair with the given element
        //O(log n) if safe = false
        //O(2log n) if safe = true
        public bool Delete(TEl element, bool safe)
        {
            if (safe)
            {
                //O(logn)
                if (!Contains(element))
                    return false;
            }

            // if both children of root are black, set root to red
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = RED;

            //O(logn)
            Root = Delete(Root, element);
            if (!IsEmpty) Root.Color = BLACK;
            // assert Check();

            //update predecessors/successors
            if (GetSuccessor(element) != null)
                predecessors[GetSuccessor(element)] = GetPredecessor(element);

            if (GetPredecessor(element) != null)
                successors[GetPredecessor(element)] = GetSuccessor(element);

            return true;
        }

        // delete the element-value pair with the given element rooted at h
        private TNode Delete(TNode t, TEl element)
        {
            if (t == null)
                throw new InvalidOperationException("Trying to delete element that is not present");

            if (Comparer.Compare(element, t.Element) < 0)
            {
                if (!IsRed(t.Left) && !IsRed(t.Left.Left))
                    t = MoveRedLeft(t);
                t.Left = Delete(t.Left, element);
            }
            else
            {
                if (IsRed(t.Left))
                    t = RotateRight(t);

                if (Comparer.Compare(element, t.Element) == 0 && t.Right == null)
                    return null;

                if (!IsRed(t.Right) && !IsRed(t.Right.Left))
                    t = MoveRedRight(t);

                if (Comparer.Compare(element, t.Element) == 0)
                {
                    //TNode x = FindMinNode(t.Right);
                    //TNode x = t.Right.Min;
                    t.Element = t.Right.Min;
                    // t.val = get(t.right, FindMinNode(t.right).element);
                    // t.element = FindMinNode(t.right).element;
                    t.Right = DeleteMin(t.Right);
                }
                else
                    t.Right = Delete(t.Right, element);
            }
            return Balance(t);
        }

        /*************************************************************************
         *  Red-black tree helper functions
         *************************************************************************/

        // make a left-leaning link lean to the right
        private TNode RotateRight(TNode t)
        {
            // assert (h != null) && IsRed(t.left);
            TNode x = t.Left;
            t.Left = x.Right;
            x.Right = t;
            x.Color = x.Right.Color;
            x.Right.Color = RED;
            x.N = t.N;
            t.N = TreeSize(t.Left) + TreeSize(t.Right) + 1;
            return x;
        }

        // make a right-leaning link lean to the left
        private TNode RotateLeft(TNode t)
        {
            // assert (h != null) && IsRed(t.right);
            TNode x = t.Right;
            t.Right = x.Left;
            x.Left = t;
            x.Color = x.Left.Color;
            x.Left.Color = RED;
            x.N = t.N;
            t.N = TreeSize(t.Left) + TreeSize(t.Right) + 1;
            return x;
        }

        // flip the colors of a node and its two children
        private void FlipColors(TNode t)
        {
            // t must have opposite color of its two children
            // assert (h != null) && (t.left != null) && (t.right != null);
            // assert (!IsRed(t) &&  IsRed(t.left) &&  IsRed(t.right))
            //    || (IsRed(t)  && !IsRed(t.left) && !IsRed(t.right));
            t.Color = !t.Color;
            t.Left.Color = !t.Left.Color;
            t.Right.Color = !t.Right.Color;
        }

        // Assuming that t is red and both t.left and t.left.left
        // are black, make t.left or one of its children red.
        private TNode MoveRedLeft(TNode t)
        {
            // assert (h != null);
            // assert IsRed(t) && !IsRed(t.left) && !IsRed(t.left.left);

            FlipColors(t);
            if (IsRed(t.Right.Left))
            {
                t.Right = RotateRight(t.Right);
                t = RotateLeft(t);
                FlipColors(t);
            }
            return t;
        }

        // Assuming that t is red and both t.right and t.right.left
        // are black, make t.right or one of its children red.
        private TNode MoveRedRight(TNode t)
        {
            // assert (h != null);
            // assert IsRed(t) && !IsRed(t.right) && !IsRed(t.right.left);
            FlipColors(t);
            if (IsRed(t.Left.Left))
            {
                t = RotateRight(t);
                FlipColors(t);
            }
            return t;
        }

        // restore red-black tree invariant
        private TNode Balance(TNode t)
        {
            // assert (h != null);

            if (IsRed(t.Right))
                t = RotateLeft(t);
            if (IsRed(t.Left) && IsRed(t.Left.Left))
                t = RotateRight(t);
            if (IsRed(t.Left) && IsRed(t.Right))
                FlipColors(t);

            t.N = TreeSize(t.Left) + TreeSize(t.Right) + 1;
            return t;
        }

        /*************************************************************************
         *  Utility functions
         *************************************************************************/

        private int TreeHeight(TNode x)
        {
            if (x == null) return -1;
            return 1 + Math.Max(TreeHeight(x.Left), TreeHeight(x.Right));
        }

        /*************************************************************************
         *  Ordered symbol table methods.
         *************************************************************************/

        public TEl GetSuccessor(TEl element)
        {
            TEl succ;
            successors.TryGetValue(element, out succ);
            return succ;
        }

        public TEl GetPredecessor(TEl element)
        {
            TEl pred;
            predecessors.TryGetValue(element, out pred);
            return pred;
        }

        private TEl FindSuccessor(TEl element)
        {
            return ElementAt(FindSuccessorNode(Root, element));
        }

        // the largest element in the subtree rooted at x less than or equal to the given element
        private TNode FindSuccessorNode(TNode x, TEl element)
        {
            if (x == null) return null;
            var cmp = Comparer.Compare(element, x.Element);
            if (cmp >= 0) return FindSuccessorNode(x.Right, element);

            var t = FindSuccessorNode(x.Left, element);
            return t ?? x;
        }

        private TEl FindPredecessor(TEl element)
        {
            return ElementAt(FindPredecessorNode(Root, element));
        }

        // the largest element in the subtree rooted at x less than or equal to the given element
        private TNode FindPredecessorNode(TNode x, TEl element)
        {
            if (x == null) return null;
            var cmp = Comparer.Compare(element, x.Element);
            if (cmp <= 0) return FindPredecessorNode(x.Left, element);

            var t = FindPredecessorNode(x.Right, element);
            return t ?? x;
        }

        // the smallest element; null if no such element
        private TEl FindMin()
        {
            return IsEmpty ? null : FindMinNode(Root).Element;
        }

        // the smallest element in subtree rooted at x; null if no such element
        private TNode FindMinNode(TNode x)
        {
            // assert x != null;
            return x.Left == null ? x : FindMinNode(x.Left);
        }

        // the largest element; null if no such element
        private TEl FindMax()
        {
            return IsEmpty ? null : FindMaxNode(Root).Element;
        }

        // the largest element in the subtree rooted at x; null if no such element
        private TNode FindMaxNode(TNode x)
        {
            // assert x != null;
            return x.Right == null ? x : FindMaxNode(x.Right);
        }

        // the largest element less than or equal to the given element
        public TEl Floor(TEl element)
        {
            var x = Floor(Root, element);
            return x?.Element;
        }

        // the largest element in the subtree rooted at x less than or equal to the given element
        private TNode Floor(TNode x, TEl element)
        {
            if (x == null) return null;
            int cmp = Comparer.Compare(element, x.Element);
            if (cmp == 0) return x;
            if (cmp < 0) return Floor(x.Left, element);

            var t = Floor(x.Right, element);
            return t ?? x;
        }

        // the smallest element greater than or equal to the given element
        public TEl Ceil(TEl element)
        {
            var x = Ceil(Root, element);
            return x?.Element;
        }

        // the smallest element in the subtree rooted at x greater than or equal to the given element
        private TNode Ceil(TNode x, TEl element)
        {
            if (x == null) return null;
            int cmp = Comparer.Compare(element, x.Element);
            if (cmp == 0) return x;
            if (cmp > 0) return Ceil(x.Right, element);

            var t = Ceil(x.Left, element);
            return t ?? x;
        }

        // the element of rank k
        public TEl Select(int k)
        {
            if (k < 0 || k >= Size)
                return null;
            TNode x = Select(Root, k);
            return x.Element;
        }

        // the element of rank k in the subtree rooted at x
        private TNode Select(TNode x, int k)
        {
            // assert x != null;
            // assert k >= 0 && k < TreeSize(x);
            int t = TreeSize(x.Left);
            if (t > k)
                return Select(x.Left, k);
            if (t < k)
                return Select(x.Right, k - t - 1);
            return x;
        }

        // number of elements less than element
        public int Rank(TEl element)
        {
            return Rank(element, Root);
        }

        // number of elements less than element in the subtree rooted at x
        private int Rank(TEl element, TNode x)
        {
            if (x == null) return 0;
            var cmp = Comparer.Compare(element, x.Element);

            if (cmp < 0) return Rank(element, x.Left);
            if (cmp > 0) return 1 + TreeSize(x.Left) + Rank(element, x.Right);
            return TreeSize(x.Left);
        }

        /***********************************************************************
         *  Range count and range search.
         ***********************************************************************/

        // the elements between lo and hi, as an Iterable
        public IEnumerable<TEl> GetAll(TEl lo, TEl hi)
        {
            Queue<TEl> queue = new Queue<TEl>();
            // if (IsEmpty() || lo.CompareTo(hi) > 0) return queue;
            Elements(Root, queue, lo, hi);
            return queue;
        }

        // add the elements between lo and hi in the subtree rooted at x
        // to the queue
        private void Elements(TNode x, Queue<TEl> queue, TEl lo, TEl hi)
        {
            if (x == null) return;
            int cmplo = Comparer.Compare(lo, x.Element);
            int cmphi = Comparer.Compare(hi, x.Element);
            if (cmplo < 0) Elements(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0) queue.Enqueue(x.Element);
            if (cmphi > 0) Elements(x.Right, queue, lo, hi);
        }

        // number elements between lo and hi
        public int NumElementsInRange(TEl lo, TEl hi)
        {
            if (Comparer.Compare(lo, hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;

            return Rank(hi) - Rank(lo);
        }

        /*************************************************************************
         *  Check integrity of red-black BST data structure
         *************************************************************************/

        private bool Check()
        {
            return IsBST() && IsSizeConsistent() && IsRankConsistent() && Is23() && IsBalanced();
        }

        // does this binary tree satisfy symmetric order?
        // Note: this test also ensures that data structure is a binary tree since order is strict
        private bool IsBST()
        {
            return IsBST(Root, null, null);
        }

        // is the tree rooted at x a BST with all elements strictly between Min and max
        // (if min or max is null, treat as empty constraint)
        // Credit: Bob Dondero's elegant solution
        private bool IsBST(TNode x, TEl min, TEl max)
        {
            if (x == null) return true;
            if (min != null && Comparer.Compare(x.Element, min) <= 0) return false;
            if (max != null && Comparer.Compare(x.Element, max) >= 0) return false;
            return IsBST(x.Left, min, x.Element) && IsBST(x.Right, x.Element, max);
        }

        // are the size fields correct?
        private bool IsSizeConsistent()
        {
            return IsSizeConsistent(Root);
        }

        private bool IsSizeConsistent(TNode x)
        {
            if (x == null) return true;
            if (x.N != TreeSize(x.Left) + TreeSize(x.Right) + 1) return false;
            return IsSizeConsistent(x.Left) && IsSizeConsistent(x.Right);
        }

        // check that ranks are consistent
        private bool IsRankConsistent()
        {
            for (var i = 0; i < Size; i++)
                if (i != Rank(Select(i))) return false;

            foreach (var element in this)
            {
                if (Comparer.Compare(element, Select(Rank(element))) != 0) return false;
            }
            return true;
        }

        // Does the tree have no red right links, and at most one (left)
        // red links in a row on any path?
        private bool Is23()
        {
            return Is23(Root);
        }

        private bool Is23(TNode x)
        {
            if (x == null) return true;
            if (IsRed(x.Right)) return false;
            if (x != Root && IsRed(x) && IsRed(x.Left))
                return false;
            return Is23(x.Left) && Is23(x.Right);
        }

        // do all paths from root to leaf have same number of black edges?
        private bool IsBalanced()
        {
            var black = 0; // number of black links on path from root to min
            var x = Root;
            while (x != null)
            {
                if (!IsRed(x)) black++;
                x = x.Left;
            }
            return IsBalanced(Root, black);
        }

        // does every path from the root to a leaf have the given number of black links?
        private bool IsBalanced(TNode x, int black)
        {
            if (x == null) return black == 0;
            if (!IsRed(x)) black--;
            return IsBalanced(x.Left, black) && IsBalanced(x.Right, black);
        }

        public override string ToString()
        {
            var str = "";
            foreach (var TEl in this)
            {
                str += TEl + " ";
            }

            return str;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TEl> GetEnumerator()
        {
            return GetAll(Min, Max).GetEnumerator();
        }
    }
}
