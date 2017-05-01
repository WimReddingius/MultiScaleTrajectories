using System;

namespace MultiScaleTrajectories.AlgoUtil.DataStructures.Search.RedBlack
{
    class RedBlackNode<TEl, TNode> where TNode : RedBlackNode<TEl, TNode>
    {
        public event Action LeftNodeUpdated;
        public event Action RightNodeUpdated;
        public event Action ElementReplaced;

        private TEl element;
        private TNode left, right;

        public bool Color;
        public int N;
        public TEl Min { get; private set; }
        public TEl Max { get; private set; }
        public TNode Successor { get; set; }
        public TNode Predecessor { get; set; }

        public TEl Element
        {
            get { return element; }
            set
            {
                if (!value.Equals(element))
                {
                    element = value;
                    if (left == null)
                        Min = Element;
                    if (right == null)
                        Max = Element;

                    ElementReplaced?.Invoke();
                }
            }
        }
        public TNode Left
        {
            get { return left; }
            set
            {
                left = value;
                Min = left != null ? left.Min : Element;
                LeftNodeUpdated?.Invoke();
            }
        }
        public TNode Right
        {
            get { return right; }
            set {
                right = value;
                Max = right != null ? right.Max : Element;
                RightNodeUpdated?.Invoke();
            }
        }

        public RedBlackNode(TEl element, TNode succ, TNode pred)
        {
            Element = element;
            Color = RedBlackBST<TEl, TNode>.RED;
            N = 1;
            Successor = succ;
            Predecessor = pred;
        }

    }
}
