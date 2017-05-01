using System.Collections.Generic;
using AlgoKit.Collections.Heaps;

namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Heap
{
    /// <summary>
    /// Represents the one node in the Fibonacci Heap.
    /// </summary>
    /// <typeparam name="TValue">Type of the object to be stored.</typeparam>
    /// <typeparam name="TKey">Type of the key to be used for the stored object. 
    /// Has to implement the <see cref="System.IComparable"/> interface.</typeparam>
    public class FibonacciHeapNode<TKey, TValue> : IHeapNode<TKey, TValue> where TKey : System.IComparable
    {
        public FibonacciHeapNode(TKey key, TValue value)
        {
            Right = this;
            Left = this;
            Value = value;
            Key = key;
        }

        /// <summary>
        /// Gets or sets the node data object.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Gets or sets the reference to the first child node.
        /// </summary>
        public FibonacciHeapNode<TKey, TValue> Child { get; set; }

        /// <summary>
        /// Gets or sets the reference to the left node neighbour.
        /// </summary>
        public FibonacciHeapNode<TKey, TValue> Left { get; set; }

        /// <summary>
        /// Gets or sets the reference to the node parent.
        /// </summary>
        public FibonacciHeapNode<TKey, TValue> Parent { get; set; }

        /// <summary>
        /// Gets or sets the reference to the right node neighbour.
        /// </summary>
        public FibonacciHeapNode<TKey, TValue> Right { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whatever node is marked (visited).
        /// </summary>
        public bool Mark { get; set; }

        /// <summary>
        /// Gets or sets the value of the node key.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the value of the node degree.
        /// </summary>
        public int Degree { get; set; }

        public IEnumerable<FibonacciHeapNode<TKey, TValue>> Traverse()
        {
            yield return this;

            for (var child = Left; child != null; child = child.Right)
                foreach (var node in child.Traverse())
                    yield return node;
        }
    }
}
