using System;
using MultiScaleTrajectories.Algorithm.DataStructures.Search.RedBlack;

namespace MultiScaleTrajectories.Algorithm.DataStructures.Search.Range.RedBlack
{
    class RedBlackRangeNode<TEl, TNode, TRange> : RedBlackNode<TEl, TNode> where TNode : RedBlackRangeNode<TEl, TNode, TRange>
    {
        public event Action RangeDataUpdated;

        public RangeConsolidator<TRange> RangeConsolidator;

        private TRange subtreeData;
        public TRange SubtreeData
        {
            get { return subtreeData; }
            set
            {
                if (!value.Equals(subtreeData))
                {
                    subtreeData = value;
                    RangeDataUpdated?.Invoke();
                }
            }
        }
        public TRange NodeData { get; private set; }


        public RedBlackRangeNode(TEl element, TNode succ, TNode pred, RangeConsolidator<TRange> rangeConsolidator, 
            RangeInitializer<TEl, TRange> rangeInitializer) : base(element, succ, pred)
        {
            RangeConsolidator = rangeConsolidator;

            NodeData = rangeInitializer(element);
            SubtreeData = NodeData;

            ElementReplaced += () =>
            {
                NodeData = rangeInitializer(element);
                ConsolidateRangeData();
            };

            LeftNodeUpdated += () =>
            {
                if (Left != null)
                    Left.RangeDataUpdated = ConsolidateRangeData;

                ConsolidateRangeData();
            };

            RightNodeUpdated += () =>
            {
                if (Right != null)
                    Right.RangeDataUpdated = ConsolidateRangeData;

                ConsolidateRangeData();
            };
        }

        private void ConsolidateRangeData()
        {
            var consolidation = NodeData;
            if (Left != null)
                consolidation = RangeConsolidator(consolidation, Left.SubtreeData);

            if (Right != null)
                consolidation = RangeConsolidator(consolidation, Right.SubtreeData);

            SubtreeData = consolidation;
        }

    }
}
