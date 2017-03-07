using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Exploration.Stats
{

    class StatTable<TIn, TOut> : DataView<AlgorithmRun<TIn, TOut>[]> where TIn : Input, new() where TOut : Output, new()
    {

        private readonly GenericStatTable table;

        public StatTable()
        {
            table = new GenericStatTable();
            FormsUtil.FillContainer(this, table);
        }

        public override void LoadData(AlgorithmRun<TIn, TOut>[] runs)
        {
            table.LoadData(runs);
        }

    }
}
