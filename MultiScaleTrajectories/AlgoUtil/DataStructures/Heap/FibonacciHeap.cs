using System;
using System.Collections.Generic;
using AlgoKit.Collections.Heaps;

namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Heap
{
    /// <summary>
    /// Fibonacci Heap realization. Uses generic type T for data storage and TKey as a key type.
    /// </summary>
    /// <typeparam name="TValue">Type of the stored objects.</typeparam>
    /// <typeparam name="TKey">Type of the object key. Should implement IComparable.</typeparam>
    public class FibonacciHeap<TKey, TValue> : BaseHeap<TKey, TValue, FibonacciHeapNode<TKey, TValue>, FibonacciHeap<TKey, TValue>> where TKey : IComparable
    {
        /// <summary>
        /// Maximum nodes quantity in the heap.
        /// </summary>
        private static readonly double OneOverLogPhi = 1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0);

        /// <summary>
        /// Minimum (statring) node of the heap.
        /// </summary>
        private FibonacciHeapNode<TKey, TValue> minNode;

        public override int Count => numNodes;

        /// <summary>
        /// The nodes quantity.
        /// </summary>
        private int numNodes;

        private readonly TKey minKeyValue;

        /// <summary>
        /// Initializes the new instance of the Heap.
        /// </summary>
        /// <param name="minKeyValue">Minimum value of the key - to be used for comparing.</param>
        /// <param name="comparer"></param>
        public FibonacciHeap(TKey minKeyValue, Comparer<TKey> comparer)
        {
            this.minKeyValue = minKeyValue;
            Comparer = comparer;
        }

        /// <summary>
        /// Removes all the elements from the heap.
        /// </summary>
        public void Clear()
        {
            minNode = null;
            numNodes = 0;
        }

        /// <summary>
        /// Decreases the key of a node.
        /// O(1) amortized.
        /// </summary>
        public override void Update(FibonacciHeapNode<TKey, TValue> x, TKey k)
        {
            if (k.CompareTo(x.Key) > 0)
            {
                throw new ArgumentException("decreaseKey() got larger key value");
            }

            x.Key = k;

            FibonacciHeapNode<TKey, TValue> y = x.Parent;

            if ((y != null) && (x.Key.CompareTo(y.Key) < 0))
            {
                Cut(x, y);
                CascadingCut(y);
            }

            if (x.Key.CompareTo(minNode.Key) < 0)
            {
                minNode = x;
            }
        }

        /// <summary>
        /// Deletes a node from the heap.
        /// O(log n)
        /// </summary>
        public override TValue Remove(FibonacciHeapNode<TKey, TValue> x)
        {
            // make newParent as small as possible
            Update(x, minKeyValue);

            // remove the smallest, which decreases n also
            return Pop().Value;
        }

        /// <summary>
        /// Inserts a new node with its key.
        /// O(1)
        /// </summary>
        public override FibonacciHeapNode<TKey, TValue> Add(TKey key, TValue value)
        {
            var node = new FibonacciHeapNode<TKey, TValue>(key, value);

            // concatenate node into min list
            if (minNode != null)
            {
                node.Left = minNode;
                node.Right = minNode.Right;
                minNode.Right = node;
                node.Right.Left = node;

                if (node.Key.CompareTo(minNode.Key) < 0)
                {
                    minNode = node;
                }
            }
            else
            {
                minNode = node;
            }

            numNodes++;
            return node;
        }

        /// <summary>
        /// Returns the smalles node of the heap.
        /// O(1)
        /// </summary>
        /// <returns></returns>
        public override FibonacciHeapNode<TKey, TValue> Peek()
        {
            return minNode;
        }

        /// <summary>
        /// Removes the smalles node of the heap.
        /// O(log n) amortized
        /// </summary>
        /// <returns></returns>
        public override FibonacciHeapNode<TKey, TValue> Pop()
        {
            var oldMin = minNode;

            if (minNode != null)
            {
                var numKids = minNode.Degree;
                var oldMinChild = minNode.Child;

                // for each child of minNode do...
                while (numKids > 0)
                {
                    var tempRight = oldMinChild.Right;

                    // remove oldMinChild from child list
                    oldMinChild.Left.Right = oldMinChild.Right;
                    oldMinChild.Right.Left = oldMinChild.Left;

                    // add oldMinChild to root list of heap
                    oldMinChild.Left = minNode;
                    oldMinChild.Right = minNode.Right;
                    minNode.Right = oldMinChild;
                    oldMinChild.Right.Left = oldMinChild;

                    // set parent[oldMinChild] to null
                    oldMinChild.Parent = null;
                    oldMinChild = tempRight;
                    numKids--;
                }

                // remove minNode from root list of heap
                minNode.Left.Right = minNode.Right;
                minNode.Right.Left = minNode.Left;

                if (minNode == minNode.Right)
                {
                    minNode = null;
                }
                else
                {
                    minNode = minNode.Right;
                    Consolidate();
                }

                // decrement size of heap
                numNodes--;
            }

            return oldMin;
        }

        /// <summary>
        /// Joins this heap with another. O(1)
        /// </summary>
        /// <param name="otherHeap"></param>
        /// <returns></returns>
        public override void Merge(FibonacciHeap<TKey, TValue> otherHeap)
        {
            if (otherHeap != null)
            {
                if (minNode != null)
                {
                    if (otherHeap.minNode != null)
                    {
                        minNode.Right.Left = otherHeap.minNode.Left;
                        otherHeap.minNode.Left.Right = minNode.Right;
                        minNode.Right = otherHeap.minNode;
                        otherHeap.minNode.Left = minNode;

                        if (otherHeap.minNode.Key.CompareTo(minNode.Key) < 0)
                        {
                            minNode = otherHeap.minNode;
                        }
                    }
                }
                else
                {
                    minNode = otherHeap.minNode;
                }

                numNodes = numNodes + otherHeap.numNodes;
            }
        }

        /// <summary>
        /// Performs a cascading cut operation. This cuts newChild from its parent and then
        /// does the same for its parent, and so on up the tree.
        /// </summary>
        protected void CascadingCut(FibonacciHeapNode<TKey, TValue> y)
        {
            FibonacciHeapNode<TKey, TValue> z = y.Parent;

            // if there's a parent...
            if (z != null)
            {
                // if newChild is unmarked, set it marked
                if (!y.Mark)
                {
                    y.Mark = true;
                }
                else
                {
                    // it's marked, cut it from parent
                    Cut(y, z);

                    // cut its parent as well
                    CascadingCut(z);
                }
            }
        }

        protected void Consolidate()
        {
            int arraySize = ((int) Math.Floor(Math.Log(numNodes) * OneOverLogPhi)) + 1;

            var array = new List<FibonacciHeapNode<TKey, TValue>>(arraySize);

            // Initialize degree array
            for (var i = 0; i < arraySize; i++)
            {
                array.Add(null);
            }

            // Find the number of root nodes.
            var numRoots = 0;
            FibonacciHeapNode<TKey, TValue> x = minNode;

            if (x != null)
            {
                numRoots++;
                x = x.Right;

                while (x != minNode)
                {
                    numRoots++;
                    x = x.Right;
                }
            }

            // For each node in root list do...
            while (numRoots > 0)
            {
                // Access this node's degree..
                int d = x.Degree;
                FibonacciHeapNode<TKey, TValue> next = x.Right;

                // ..and see if there's another of the same degree.
                for (;;)
                {
                    FibonacciHeapNode<TKey, TValue> y = array[d];
                    if (y == null)
                    {
                        // Nope.
                        break;
                    }

                    // There is, make one of the nodes a child of the other.
                    // Do this based on the key value.
                    if (x.Key.CompareTo(y.Key) > 0)
                    {
                        FibonacciHeapNode<TKey, TValue> temp = y;
                        y = x;
                        x = temp;
                    }

                    // FibonacciHeapNode<T> newChild disappears from root list.
                    Link(y, x);

                    // We've handled this degree, go to next one.
                    array[d] = null;
                    d++;
                }

                // Save this node for later when we might encounter another
                // of the same degree.
                array[d] = x;

                // Move forward through list.
                x = next;
                numRoots--;
            }

            // Set min to null (effectively losing the root list) and
            // reconstruct the root list from the array entries in array[].
            minNode = null;

            for (var i = 0; i < arraySize; i++)
            {
                FibonacciHeapNode<TKey, TValue> y = array[i];
                if (y == null)
                {
                    continue;
                }

                // We've got a live one, add it to root list.
                if (minNode != null)
                {
                    // First remove node from root list.
                    y.Left.Right = y.Right;
                    y.Right.Left = y.Left;

                    // Now add to root list, again.
                    y.Left = minNode;
                    y.Right = minNode.Right;
                    minNode.Right = y;
                    y.Right.Left = y;

                    // Check if this is a new min.
                    if (y.Key.CompareTo(minNode.Key) < 0)
                    {
                        minNode = y;
                    }
                }
                else
                {
                    minNode = y;
                }
            }
        }

        /// <summary>
        /// The reverse of the link operation: removes newParent from the child list of newChild.
        /// This method assumes that min is non-null.
        /// Running time: O(1)
        /// </summary>
        protected void Cut(FibonacciHeapNode<TKey, TValue> x, FibonacciHeapNode<TKey, TValue> y)
        {
            // remove newParent from childlist of newChild and decrement degree[newChild]
            x.Left.Right = x.Right;
            x.Right.Left = x.Left;
            y.Degree--;

            // reset newChild.child if necessary
            if (y.Child == x)
            {
                y.Child = x.Right;
            }

            if (y.Degree == 0)
            {
                y.Child = null;
            }

            // add newParent to root list of heap
            x.Left = minNode;
            x.Right = minNode.Right;
            minNode.Right = x;
            x.Right.Left = x;

            // set parent[newParent] to nil
            x.Parent = null;

            // set mark[newParent] to false
            x.Mark = false;
        }

        /// <summary>
        /// Makes newChild a child of Node newParent.
        /// O(1)
        /// </summary>
        protected void Link(FibonacciHeapNode<TKey, TValue> newChild, FibonacciHeapNode<TKey, TValue> newParent)
        {
            // remove newChild from root list of heap
            newChild.Left.Right = newChild.Right;
            newChild.Right.Left = newChild.Left;

            // make newChild a child of newParent
            newChild.Parent = newParent;

            if (newParent.Child == null)
            {
                newParent.Child = newChild;
                newChild.Right = newChild;
                newChild.Left = newChild;
            }
            else
            {
                newChild.Left = newParent.Child;
                newChild.Right = newParent.Child.Right;
                newParent.Child.Right = newChild;
                newChild.Right.Left = newChild;
            }

            // increase degree[newParent]
            newParent.Degree++;

            // set mark[newChild] false
            newChild.Mark = false;
        }

        public override IEnumerator<IHeapNode<TKey, TValue>> GetEnumerator()
        {
            return minNode.Traverse().GetEnumerator();
        }
    }
}