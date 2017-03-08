using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Exploration.Stats
{

    class StatTable<TIn, TOut> : DataView<AlgorithmRun<TIn, TOut>[]> where TIn : Input, new() where TOut : Output, new()
    {

        private readonly NonGenericStatTable table;

        public StatTable()
        {
            table = new NonGenericStatTable();
            FormsUtil.FillContainer(this, table);
        }

        public override void LoadData(AlgorithmRun<TIn, TOut>[] runs)
        {
            table.LoadData(runs);
        }

    }
}
