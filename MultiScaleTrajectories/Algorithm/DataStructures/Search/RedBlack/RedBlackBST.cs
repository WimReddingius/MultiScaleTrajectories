using System;
using System.Collections;
using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack
{
    //left leaning red black binary search tree with no duplicates
    abstract class RedBlackBST<TEl, TNode> : ISearchTree<TEl>, IEnumerable<TEl> where TNode : RedBlackNode<TEl, TNode>
    {
        public const bool RED = true;
        public const bool BLACK = false;

        public TEl Min => Root.Min;
        public TEl Max => Root.Max;
        public int Height => TreeHeight(Root);
        public int Size => TreeSize(Root);
        public bool IsEmpty => Root == null;

        protected TNode Root;
        private readonly Comparer<TEl> Comparer;
        private readonly Dictionary<TEl, TNode> nodes;


        protected RedBlackBST(Comparer<TEl> comparer)
        {
            Comparer = comparer;
            nodes = new Dictionary<TEl, TNode>();
        }

        protected RedBlackBST(Comparison<TEl> comparison) : this(Comparer<TEl>.Create(comparison))
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

        public bool TryFind(SearchPredicate<TEl> predicate, out TEl element)
        {
            return TryFind(new SearchPredicator<TEl>(predicate), out element);
        }

        // value associated with the given predicator; null if no such element
        public bool TryFind(SearchPredicator<TEl> predicator, out TEl element)
        {
            var node = FindNode(Root, predicator);
            if (node != null)
            {
                element = node.Element;
                return true;
            }

            element = default(TEl);
            return false;
        }

        protected TNode FindNode(TNode x, SearchPredicator<TEl> predicator)
        {
            while (x != null)
            {
                var cmp = predicator.ElementCompliance(x.Element);
                if (cmp < 0)
                    x = x.Left;
                else if (cmp > 0)
                    x = x.Right;
                else
                    return x;
            }
            return x;
        }

        // is there a element-value pair with the given element?
        public bool Contains(TEl element)
        {
            var someElement = default(TEl);
            return TryFind(el => Comparer.Compare(el, element), out someElement);
        }

        protected TEl ElementAt(TNode t)
        {
            return t.Element;
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
        public void Insert(TEl element, Func<TEl, bool> overwriteFunc = null)
        {
            Root = Insert(Root, element, overwriteFunc);
            Root.Color = BLACK;
            // assert Check();
        }

        // insert the element-value pair in the subtree rooted at t
        private TNode Insert(TNode t, TEl element, Func<TEl, bool> overwriteFunc, TNode bestSucc = null, TNode bestPred = null)
        {
            if (t == null)
            {
                var node = CreateNode(element, bestSucc, bestPred);

                if (bestPred != null)
                {
                    bestPred.Successor = node;
                }
                if (bestSucc != null)
                {
                    bestSucc.Predecessor = node;
                }

                nodes[element] = node;
                return node;
            }

            var cmp = Comparer.Compare(element, t.Element);
            if (cmp < 0)
            {
                t.Left = Insert(t.Left, element, overwriteFunc, t, bestPred);
            }
            else if (cmp > 0)
            {
                t.Right = Insert(t.Right, element, overwriteFunc, bestSucc, t);
            }
            else
            {
                if (overwriteFunc == null || overwriteFunc(t.Element))
                {
                    t.Element = element;
                    nodes.Remove(t.Element);
                    nodes[element] = t;
                }
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

        protected abstract TNode CreateNode(TEl element, TNode succ, TNode pred);

        /*************************************************************************
         *  Red-black deletion
         *************************************************************************/

        // delete the element-value pair with the minimum element
        public virtual void DeleteMin()
        {
            if (IsEmpty) throw new InvalidOperationException("BST underflow");

            //nodes[Min].Successor.Predecessor = null;
            //nodes.Remove(Min);

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
            {
                DeleteNodeAndConnectNeighbors(t);
                return null;
            }

            if (!IsRed(t.Left) && !IsRed(t.Left.Left))
                t = MoveRedLeft(t);

            t.Left = DeleteMin(t.Left);
            return Balance(t);
        }

        // delete the element-value pair with the maximum element
        public void DeleteMax()
        {
            if (IsEmpty) throw new InvalidOperationException("BST underflow");

            //nodes[Max].Predecessor.Successor = null;
            //nodes.Remove(Max);

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
            {
                DeleteNodeAndConnectNeighbors(t);
                return null;
            }

            if (!IsRed(t.Right) && !IsRed(t.Right.Left))
                t = MoveRedRight(t);

            t.Right = DeleteMax(t.Right);

            return Balance(t);
        }

        // delete the element-value pair with the given element
        public bool Delete(TEl element)
        {
            if (!nodes.ContainsKey(element))
                return false;

            // if both children of root are black, set root to red
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = RED;

            //O(logn)
            Root = Delete(Root, element);
            if (!IsEmpty) Root.Color = BLACK;
            // assert Check();

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
                {
                    DeleteNodeAndConnectNeighbors(t);
                    return null;
                }

                if (!IsRed(t.Right) && !IsRed(t.Right.Left))
                    t = MoveRedRight(t);

                if (Comparer.Compare(element, t.Element) == 0)
                {
                    t.Element = t.Right.Min;
                    t.Right = DeleteMin(t.Right);

                    nodes.Remove(element);
                    nodes[t.Element] = t;
                }
                else
                    t.Right = Delete(t.Right, element);
            }
            return Balance(t);
        }

        private void DeleteNodeAndConnectNeighbors(TNode t)
        {
            if (t.Successor != null)
                t.Successor.Predecessor = t.Predecessor;

            if (t.Predecessor != null)
                t.Predecessor.Successor = t.Successor;

            nodes.Remove(t.Element);
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

        public TNode GetNode(TEl element)
        {
            return nodes[element];
        }

        public bool TryGetSuccessor(TEl element, out TEl succ)
        {
            if (nodes[element].Successor != null)
            {
                succ = nodes[element].Successor.Element;
                return true;
            }

            succ = default(TEl);
            return false;
        }

        public bool TryGetPredecessor(TEl element, out TEl pred)
        {
            if (nodes[element].Predecessor != null)
            {
                pred = nodes[element].Predecessor.Element;
                return true;
            }

            pred = default(TEl);
            return false;
        }

        /*************************************************************************
         *  Ordered symbol table methods.
         *************************************************************************/

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
