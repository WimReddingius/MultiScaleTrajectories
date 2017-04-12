using System;

namespace MultiScaleTrajectories.Algorithm.DataStructures.BST.RedBlackBST
{
    class RedBlackNode<TEl, TNode> where TNode : RedBlackNode<TEl, TNode>
    {
        public event Action LeftNodeChanged;
        public event Action RightNodeChanged;
        public event Action ElementReplaced;

        private TEl element;
        private TNode left, right;

        public bool Color;
        public int N;
        public TEl Min, Max;
        public TEl Element
        {
            get { return element; }
            set
            {
                element = value;
                if (left == null)
                    Min = element;
                if (right == null)
                    Max = element;

                ElementReplaced?.Invoke();
            }
        }
        public TNode Left
        {
            get { return left; }
            set
            {
                left = value;
                Min = left != null ? left.Min : Element;
                LeftNodeChanged?.Invoke();
            }
        }
        public TNode Right
        {
            get { return right; }
            set {
                right = value;
                Max = right != null ? right.Max : Element;
                RightNodeChanged?.Invoke();
            }
        }

    }
}
